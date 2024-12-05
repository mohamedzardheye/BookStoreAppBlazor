using AutoMapper;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.Models.Author;

namespace BookStoreApp.Api.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        { 
            CreateMap<AuthorCreateDto, Author>().ReverseMap();
            CreateMap<AuthorReadOnlyDto, Author>().ReverseMap();
            CreateMap<AuthorUpdateDto, Author>().ReverseMap();
        }
    }
}
