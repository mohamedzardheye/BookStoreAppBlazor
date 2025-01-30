using AutoMapper;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.Models;
using BookStoreApp.Api.Models.Author;
using BookStoreApp.Api.Models.Book;
using BookStoreApp.Api.Models.booking;
using BookStoreApp.Api.Models.User;
using Microsoft.AspNetCore.Authorization;
using Sahal_Projects.Areas.APIs.Model;

namespace BookStoreApp.Api.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        { 
            CreateMap<AuthorCreateDto, Author>().ReverseMap();
            CreateMap<AuthorReadOnlyDto, Author>().ReverseMap();
            CreateMap<AuthorUpdateDto, Author>().ReverseMap();
            CreateMap<AuthorReadOnlyDto, PaginatedResponseDto<AuthorReadOnlyDto>>().ReverseMap();

            CreateMap<BookUpdateDto, Book>().ReverseMap();
            CreateMap<BookCreateDto, Book>().ReverseMap();

            CreateMap<Book, BookReadOnlyDto>()
                .ForMember(q=> q.AuthorName, d => d.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}"))
                .ReverseMap(); 
            
            CreateMap<Book, BookDetailDto>()
                .ForMember(q=> q.AuthorName, d => d.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}"))
                .ReverseMap();



            CreateMap<ApiUser, UserDto>().ReverseMap();





            CreateMap<booking_registration, booking_registration_read_only>().ReverseMap();

        }
    }
}
