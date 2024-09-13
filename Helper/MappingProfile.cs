using AutoMapper;
using LibraryManagementSystem.Dtos;
using LibraryManagementSystem.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Author, AuthorDto>();
        CreateMap<Book, BookDto>();
        CreateMap<AuthorDto, Author>();
        CreateMap<BookDto, Book>();
    }
}
