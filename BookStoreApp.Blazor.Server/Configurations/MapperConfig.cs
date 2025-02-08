using AutoMapper;
using BookStoreApp.Blazor.Server.Services.Base;


namespace BookStoreApp.Blazor.Server.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig() {
        
            CreateMap<AuthorReadOnlyDto,AuthorCreateDto>().ReverseMap();
      




        }
    }
}
