using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FaceRecognition.Service.Api.Domain;
using FaceRecognition.Service.Api.Entities;

namespace FaceRecognition.Service.Api.Controllers
{
    [ApiController]
    [Route("api/Detection")]
    public class FacesController : ControllerBase
    {
        private readonly IServiceDetectFaces _serviceDetectFaces;
        private readonly IServiceCompareFaces _serviceCompareFaces;

        public FacesController(
            IServiceDetectFaces serviceDetectFaces,
            IServiceCompareFaces serviceCompareFaces)
        {
            _serviceDetectFaces = serviceDetectFaces;
            _serviceCompareFaces = serviceCompareFaces;
        }

        [HttpPost("FaceMatch")]
        public async Task<IActionResult> GetFaceMatches([FromBody] FaceMatchRequest a)
        {
            try
            {
                var result = await _serviceCompareFaces.CompareFacesAsync(a.SourceImage,a.TargetImage);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), result);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpGet("findfaces")]
        public async Task<IActionResult> GetFaceMatches([FromBody] FindFacesRequest request)
        {
            try
            {
                var response = await _serviceDetectFaces.DetectFacesAsync(
                    request.SourceImage
                );

                return StatusCode(HttpStatusCode.OK.GetHashCode(), response);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
