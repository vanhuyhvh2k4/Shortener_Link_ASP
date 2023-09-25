
using Shortener_Link.Data;
using Shortener_Link.Interface.Repository;
using Shortener_Link.Models;

namespace Shortener_Link.Repository
{
    public class LinkRepository : ILinkRepository
    {
        private readonly DataContext _context;

        public LinkRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateLink(Link link)
        {
            var newLink = new Link()
            {
                Id = new Guid(),
                OriginalLink = link.OriginalLink.Trim(),
                Endpoint = link.Endpoint.Trim(),
                ShortedLink = link.ShortedLink.Trim(),
                QRLink = link.QRLink.Trim(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.MinValue
            };

            _context.Links.Add(newLink);

            return Save();
        }

        public Link GetLinkByEndpoint(string endpoint)
        {
            return _context.Links.Where(link => link.Endpoint == endpoint.Trim()).FirstOrDefault();
        }

        public bool IsEndpointExists(string endpoint)
        {
            return _context.Links.Any(link => link.Endpoint.Trim() == endpoint.Trim());
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
