using Shortener_Link.DTO;

namespace Shortener_Link.Interface.Services
{
    public interface ILinkService
    {
        ResponseDTO<GetLinkDTO> CreateShortLink(CreateLinkDTO createLink);

        ResponseDTO<string> RedirectLink(string endpoint);
    }
}
