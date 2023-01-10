using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using FaceRecognition.Service.Api.Domain;
using FaceRecognition.Service.Api.Entities;

namespace FaceRecognition.Service.Api.Services;

public class ServiceCompareFaces : IServiceCompareFaces
{
    private readonly AmazonRekognitionClient _rekognitionClient;
    private readonly IServiceUtils _serviceUtils;

    public ServiceCompareFaces(IServiceUtils serviceUtils)
    {
        _serviceUtils = serviceUtils;
        _rekognitionClient =
            new AmazonRekognitionClient("Public Code", "Private Code Amazon Token", RegionEndpoint.USEast2);
    }

    public async Task<FaceMatchResponse> CompareFacesAsync(string sourceImage, string targetImage)
    {
        var imageSource = new Image();
        imageSource.Bytes = _serviceUtils.ConvertImageToMemoryStream(sourceImage);

        var imageTarget = new Image();
        imageTarget.Bytes = _serviceUtils.ConvertImageToMemoryStream(targetImage);

        var request = new CompareFacesRequest
        {
            SourceImage = imageSource,
            TargetImage = imageTarget,
            SimilarityThreshold = 80f
        };


        var response = await _rekognitionClient.CompareFacesAsync(request);
        var hasMatch = response.FaceMatches.Any();

        if (!hasMatch) return new FaceMatchResponse(hasMatch, null, string.Empty);

        //var fileName = _serviceUtils.Drawing(imageSource.Bytes, response.SourceImageFace);
        var fileName = "filename";
        var similarity = response.FaceMatches.FirstOrDefault().Similarity;

        return new FaceMatchResponse(hasMatch, similarity, fileName);
    }
}