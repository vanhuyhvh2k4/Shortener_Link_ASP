using AutoMapper;
using Shortener_Link.DTO;
using Shortener_Link.Interface.Repository;
using Shortener_Link.Interface.Services;
using Shortener_Link.Interface.Utilities;
using Shortener_Link.Models;

namespace Shortener_Link.Services
{
    public class LinkService : ILinkService
    {
        private readonly ILinkRepository _linkRepository;
        private readonly IEndpointUtilities _endpointUtilities;
        private readonly IMapper _mapper;
        private readonly string DOMAIN_NAME;
        private readonly int NUMBER_OF_ENDPOINT;

        public LinkService(ILinkRepository linkRepository, IEndpointUtilities endpointUtilities, IMapper mapper, IConfiguration configuration)
        {
            _linkRepository = linkRepository;
            _endpointUtilities = endpointUtilities;
            _mapper = mapper;
            DOMAIN_NAME = configuration.GetSection("DOMAIN_NAME").Value!;
            NUMBER_OF_ENDPOINT = int.Parse(configuration.GetSection("NUMBER_OF_ENDPOINT").Value!);
        }

        public ResponseDTO<GetLinkDTO> CreateShortLink(CreateLinkDTO createLink)
        {
            try
            {
                string endpoint = "";

                if (String.IsNullOrEmpty(createLink.Endpoint))
                {
                    while (String.IsNullOrEmpty(endpoint) || _linkRepository.IsEndpointExists(endpoint))
                    {   
                        endpoint = _endpointUtilities.GenerateRandomEndpoint(NUMBER_OF_ENDPOINT);
                    }
                } else
                {
                    if (_linkRepository.IsEndpointExists(createLink.Endpoint.Trim()))
                    {
                        return new ResponseDTO<GetLinkDTO>
                        {
                            Status = 409,
                            Message = "Endpoint already exits"
                        };
                    } else
                    {
                        endpoint = createLink.Endpoint.Trim();
                    }
                }

                var newShortLink = new GetLinkDTO()
                {
                    OriginalLink = createLink.OriginalLink,
                    Endpoint = endpoint,
                    QRLink = "qrlink",
                    ShortedLink = DOMAIN_NAME + endpoint
                };

                var newLinkMapper = _mapper.Map<Link>(newShortLink);

                _linkRepository.CreateLink(newLinkMapper);

                return new ResponseDTO<GetLinkDTO>
                {
                    Status = 200,
                    Message = "Success",
                    Data = newShortLink
                };
            } catch (Exception ex)
            {
                return new ResponseDTO<GetLinkDTO>
                {
                    Status = 500,
                    Message = ex.Message
                };
            }
        }
    }
}
