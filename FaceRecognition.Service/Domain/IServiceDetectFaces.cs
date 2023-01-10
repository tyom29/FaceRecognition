using System.Threading.Tasks;
using FaceRecognition.Service.Api.Entities;

namespace FaceRecognition.Service.Api.Domain;

public interface IServiceDetectFaces
{
    Task<FindFacesResponse> DetectFacesAsync(string sourceImage);
}