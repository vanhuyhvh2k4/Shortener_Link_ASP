namespace Shortener_Link.Models
{
    public class Link
    {
        public Guid Id { get; set; }

        public string OriginalLink { get; set; }

        public string Endpoint { get; set; }

        public string ShortedLink { get; set; }

        public string QRLink { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
