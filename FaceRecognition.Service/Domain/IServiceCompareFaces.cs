using System.Threading.Tasks;
using FaceRecognition.Service.Api.Entities;

namespace FaceRecognition.Service.Api.Domain
{
    public interface IServiceCompareFaces
    {
        Task<FaceMatchResponse> CompareFacesAsync(string sourceImage, string targetImage);
    }
}