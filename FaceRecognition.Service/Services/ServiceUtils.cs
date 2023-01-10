using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using FaceRecognition.Service.Api.Domain;
using Microsoft.AspNetCore.Http;
using aws = Amazon.Rekognition.Model;

namespace FaceRecognition.Service.Api.Services;

public class ServiceUtils : IServiceUtils
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ServiceUtils(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public MemoryStream ConvertImageToMemoryStream(string imageBase64)
    {
        var bytes = Convert.FromBase64String(imageBase64);
        return new MemoryStream(bytes);
    }

    public string Drawing(MemoryStream imageSource, aws.ComparedSourceImageFace faceMatch)
    {
        var squares = new List<aws.BoundingBox>();

        squares.Add(
            new aws.BoundingBox
            {
                Left = faceMatch.BoundingBox.Left,
                Top = faceMatch.BoundingBox.Top,
                Width = faceMatch.BoundingBox.Width,
                Height = faceMatch.BoundingBox.Height
            }
        );

        return Drawing(imageSource, squares);
    }

    public string Drawing(MemoryStream imageSource, List<aws.FaceDetail> faceDetails)
    {
        var squares = new List<aws.BoundingBox>();

        faceDetails.ForEach(f =>
        {
            squares.Add(
                new aws.BoundingBox
                {
                    Left = f.BoundingBox.Left,
                    Top = f.BoundingBox.Top,
                    Width = f.BoundingBox.Width,
                    Height = f.BoundingBox.Height
                }
            );
        });

        return Drawing(imageSource, squares);
    }

    private string Drawing(MemoryStream imageSource, List<aws.BoundingBox> squares)
    {
        var image = Image.FromStream(imageSource);
        var graphic = Graphics.FromImage(image);
        var pen = new Pen(Brushes.Red, 5f);

        squares.ForEach(b =>
        {
            graphic.DrawRectangle(
                pen,
                b.Left * image.Width,
                b.Top * image.Height,
                b.Width * image.Width,
                b.Height * image.Height
            );
        });

        var fileName = $"{Guid.NewGuid()}.jpg";

        image.Save($"Images/{fileName}", ImageFormat.Jpeg);

        return GetUrlImage(fileName);
    }

    private string GetUrlImage(string fileName)
    {
        var request = _httpContextAccessor.HttpContext.Request;
        var urlImage = $"{request.Scheme}://{request.Host.ToUriComponent()}/images/{fileName}";

        return urlImage;
    }
}