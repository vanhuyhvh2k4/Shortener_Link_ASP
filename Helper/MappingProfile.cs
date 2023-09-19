using AutoMapper;
using Shortener_Link.Models;
using Shortener_Link.DTO;

namespace Shortener_Link.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Link, GetLinkDTO>();
            CreateMap<GetLinkDTO, Link>();

            CreateMap<Link, CreateLinkDTO>();
            CreateMap<CreateLinkDTO, Link>();
        }
    }
}
