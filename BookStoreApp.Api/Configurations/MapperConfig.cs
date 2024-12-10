using AutoMapper;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.Models.Author;
using BookStoreApp.Api.Models.Book;

namespace BookStoreApp.Api.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        { 
            CreateMap<AuthorCreateDto, Author>().ReverseMap();
            CreateMap<AuthorReadOnlyDto, Author>().ReverseMap();
            CreateMap<AuthorUpdateDto, Author>().ReverseMap();


            CreateMap<Book, BookReadOnlyDto>()
                .ForMember(q=> q.AuthorName, d => d.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}"))
                .ReverseMap();



        }
    }
}
