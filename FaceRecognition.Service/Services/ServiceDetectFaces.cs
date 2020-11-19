using Amazon.Rekognition;
using System.Threading.Tasks;
using Amazon.Rekognition.Model;
using System.Collections.Generic;
using FaceRecognition.Service.Api.Domain;
using FaceRecognition.Service.Api.Entities;
using System.Drawing;
using Amazon;

namespace FaceRecognition.Service.Api.Services
{
    public class ServiceDetectFaces : IServiceDetectFaces
    {
        private readonly IServiceUtils _serviceUtils;
        private readonly AmazonRekognitionClient _rekognitionClient;

        public ServiceDetectFaces(IServiceUtils serviceUtils)
        {
            _serviceUtils = serviceUtils;
            _rekognitionClient = new AmazonRekognitionClient("Public Token", "Private Token", RegionEndpoint.USEast2);
        }

        public async Task<FindFacesResponse> DetectFacesAsync(string sourceImage)
        {
            var imageSource = new Amazon.Rekognition.Model.Image();
            imageSource.Bytes = _serviceUtils.ConvertImageToMemoryStream(sourceImage);

            var request = new DetectFacesRequest
            {
                Attributes = new List<string>{ "DEFAULT" },
                Image = imageSource
            };

            var response = await _rekognitionClient.DetectFacesAsync(request);
            var fileName = _serviceUtils.Drawing(imageSource.Bytes, response.FaceDetails);

            return new FindFacesResponse(fileName);
        }
    }
}