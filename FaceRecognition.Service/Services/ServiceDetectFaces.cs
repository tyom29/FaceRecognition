using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using FaceRecognition.Service.Api.Domain;
using FaceRecognition.Service.Api.Entities;

namespace FaceRecognition.Service.Api.Services;

public class ServiceDetectFaces : IServiceDetectFaces
{
    private readonly AmazonRekognitionClient _rekognitionClient;
    private readonly IServiceUtils _serviceUtils;

    public ServiceDetectFaces(IServiceUtils serviceUtils)
    {
        _serviceUtils = serviceUtils;
        _rekognitionClient = new AmazonRekognitionClient("Public Token", "Private Token", RegionEndpoint.USEast2);
    }

    public async Task<FindFacesResponse> DetectFacesAsync(string sourceImage)
    {
        var imageSource = new Image();
        imageSource.Bytes = _serviceUtils.ConvertImageToMemoryStream(sourceImage);

        var request = new DetectFacesRequest
        {
            Attributes = new List<string> { "DEFAULT" },
            Image = imageSource
        };

        var response = await _rekognitionClient.DetectFacesAsync(request);
        var fileName = _serviceUtils.Drawing(imageSource.Bytes, response.FaceDetails);

        return new FindFacesResponse(fileName);
    }
}