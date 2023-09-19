namespace Shortener_Link.DTO
{
    public class GetLinkDTO
    {
        public string OriginalLink { get; set; }

        public string Endpoint { get; set; }

        public string ShortedLink { get; set; }

        public string QRLink { get; set; }
    }

    public class CreateLinkDTO
    {
        public string OriginalLink { get; set; }

        public string ?Endpoint { get; set; }
    }
}
