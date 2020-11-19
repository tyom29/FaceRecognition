using System.Linq;
using Amazon.Rekognition;
using System.Threading.Tasks;
using Amazon.Rekognition.Model;
using FaceRecognition.Service.Api.Domain;
using FaceRecognition.Service.Api.Entities;
using Amazon;

namespace FaceRecognition.Service.Api.Services
{
    public class ServiceCompareFaces : IServiceCompareFaces
    {
        private readonly IServiceUtils _serviceUtils;
        private readonly AmazonRekognitionClient _rekognitionClient;

        public ServiceCompareFaces(IServiceUtils serviceUtils)
        {
            _serviceUtils = serviceUtils;
            _rekognitionClient = new AmazonRekognitionClient("Public Code", "Private Code Amazon Token", RegionEndpoint.USEast2);
        }

        public async Task<FaceMatchResponse> CompareFacesAsync(string sourceImage, string targetImage)
        {
            var imageSource = new Amazon.Rekognition.Model.Image();
            imageSource.Bytes = _serviceUtils.ConvertImageToMemoryStream(sourceImage);

            var imageTarget = new Amazon.Rekognition.Model.Image();
            imageTarget.Bytes = _serviceUtils.ConvertImageToMemoryStream(targetImage);

            var request = new CompareFacesRequest
            {
                SourceImage = imageSource,
                TargetImage = imageTarget,
                SimilarityThreshold = 80f
            };

         
            var response = await _rekognitionClient.CompareFacesAsync(request);
            var hasMatch = response.FaceMatches.Any();

            if (!hasMatch)
            {
                return new FaceMatchResponse(hasMatch, null, string.Empty);
            }

            //var fileName = _serviceUtils.Drawing(imageSource.Bytes, response.SourceImageFace);
            var fileName = "filename";
            var similarity = response.FaceMatches.FirstOrDefault().Similarity;

            return new FaceMatchResponse(hasMatch, similarity, fileName);
        }
    }
}