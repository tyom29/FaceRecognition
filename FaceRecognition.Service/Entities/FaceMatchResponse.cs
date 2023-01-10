namespace FaceRecognition.Service.Api.Entities;

public class FaceMatchResponse
{
    public FaceMatchResponse(bool match, float? similarity, string fileName)
    {
        Match = match;
        Similarity = similarity;
        DrawnImage = fileName;
    }

    public bool Match { get; }
    public float? Similarity { get; }
    public string DrawnImage { get; }
}