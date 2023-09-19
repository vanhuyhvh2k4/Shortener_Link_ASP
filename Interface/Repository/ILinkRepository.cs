using Shortener_Link.Models;
using System.Security.Principal;

namespace Shortener_Link.Interface.Repository
{
    public interface ILinkRepository
    {
        Link CreateLink(Link link);
    }
}
