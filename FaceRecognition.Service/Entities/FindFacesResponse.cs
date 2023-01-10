namespace FaceRecognition.Service.Api.Entities;

public class FindFacesResponse
{
    public FindFacesResponse(string fileName)
    {
        DrawnImage = fileName;
    }

    public string DrawnImage { get; }
}