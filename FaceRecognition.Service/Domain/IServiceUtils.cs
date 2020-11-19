using System.IO;
using System.Collections.Generic;
using aws = Amazon.Rekognition.Model;

namespace FaceRecognition.Service.Api.Domain
{
    public interface IServiceUtils
    {
        MemoryStream ConvertImageToMemoryStream(string imageBase64);
        string Drawing(MemoryStream imageSource, aws.ComparedSourceImageFace faceMatch);
        string Drawing(MemoryStream imageSource, List<aws.FaceDetail> faceDetails);
    }
}