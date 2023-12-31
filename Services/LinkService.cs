﻿using AutoMapper;
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
        private readonly IUrlUtilities _urlUtilities;
        private readonly IMapper _mapper;
        private readonly string DOMAIN_NAME;
        private readonly int NUMBER_OF_ENDPOINT;

        public LinkService(ILinkRepository linkRepository, IEndpointUtilities endpointUtilities, IUrlUtilities urlUtilities, IMapper mapper, IConfiguration configuration)
        {
            _linkRepository = linkRepository;
            _endpointUtilities = endpointUtilities;
            _urlUtilities = urlUtilities;
            _mapper = mapper;
            DOMAIN_NAME = configuration.GetSection("DOMAIN_NAME").Value!;
            NUMBER_OF_ENDPOINT = int.Parse(configuration.GetSection("NUMBER_OF_ENDPOINT").Value!);
        }

        public ResponseDTO<GetLinkDTO> CreateShortLink(CreateLinkDTO createLink)
        {
            try
            {
                 bool isValidUrl = _urlUtilities.CheckUrlActive(createLink.OriginalLink);

                if (!isValidUrl)
                {
                    return new ResponseDTO<GetLinkDTO>
                    {
                        Status = 400,
                        Message = "The url you entered is not working, please try again"
                    };
                }
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
                            Message = "The endpoint you entered already exitst. Please try another endpoint or use random endpoint."
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

        public ResponseDTO<string> RedirectLink(string endpoint)
        {
            try
            {
                Link link = _linkRepository.GetLinkByEndpoint(endpoint);

                if (link == null)
                {
                    return new ResponseDTO<string>
                    {
                        Status = 404,
                        Message = "Not found link"
                    };
                }

                return new ResponseDTO<string>
                {
                    Status = 200,
                    Message = "Success",
                    Data = link.OriginalLink
                };
            } catch (Exception ex)
            {
                return new ResponseDTO<string>
                {
                    Status = 500,
                    Message = ex.Message
                };
            }
        }
    }
}
