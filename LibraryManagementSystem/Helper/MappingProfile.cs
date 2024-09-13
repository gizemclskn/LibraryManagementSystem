using AutoMapper;
using LibraryManagementSystem.DTO;
using LibraryManagementSystem.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Author, AuthorDto>().ReverseMap();
        CreateMap<Book, BookDto>().ReverseMap();
        
    }
}
