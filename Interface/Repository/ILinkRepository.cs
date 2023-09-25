using Shortener_Link.Models;

namespace Shortener_Link.Interface.Repository
{
    public interface ILinkRepository
    {
        bool CreateLink(Link link);

        bool IsEndpointExists(string endpoint);

        Link GetLinkByEndpoint(string endpoint);

        bool Save();
    }
}
