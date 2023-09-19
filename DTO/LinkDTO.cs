namespace Shortener_Link.DTO
{
    public class LinkDTO
    {
        public Guid Id { get; set; }

        public string OriginalLink { get; set; }

        public string Endpoint { get; set; }

        public string ShortedLink { get; set; }

        public string QRLink { get; set; }
    }
}
