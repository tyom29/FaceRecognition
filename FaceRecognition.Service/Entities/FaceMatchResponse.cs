namespace FaceRecognition.Service.Api.Entities
{
    public class FaceMatchResponse
    {
        public FaceMatchResponse(bool match, float? similarity, string fileName)
        {
            Match = match;
            Similarity = similarity;
            DrawnImage = fileName;
        }

        public bool Match { get; private set; }
        public float? Similarity { get; private set; }
        public string DrawnImage { get; private set; }
    }
}