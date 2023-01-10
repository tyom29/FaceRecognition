using System;
using System.Net;
using System.Threading.Tasks;
using FaceRecognition.Service.Api.Domain;
using FaceRecognition.Service.Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FaceRecognition.Service.Api.Controllers;

[ApiController]
[Route("api/v1/Detection")]
public class FacesController : ControllerBase
{
    private readonly IServiceCompareFaces _serviceCompareFaces;
    private readonly IServiceDetectFaces _serviceDetectFaces;

    public FacesController(
        IServiceDetectFaces serviceDetectFaces,
        IServiceCompareFaces serviceCompareFaces)
    {
        _serviceDetectFaces = serviceDetectFaces;
        _serviceCompareFaces = serviceCompareFaces;
    }


    [HttpGet]
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

    [HttpPost]
    public async Task<IActionResult> GetFaceMatches([FromBody] FaceMatchRequest a)
    {
        try
        {
            var result = await _serviceCompareFaces.CompareFacesAsync(a.SourceImage, a.TargetImage);
            return StatusCode(HttpStatusCode.OK.GetHashCode(), result);
        }
        catch (Exception ex)
        {
            return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
        }
    }
}