using AutoMapper;
using CommunityLibrary.Application.DTO;
using CommunityLibrary.Domain;

namespace CommunityLibrary.Application.MappingSetup
{
    public class MappingProfile :Profile
    {
        public MappingProfile ()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<AuthorDto, Author>().ReverseMap();
            CreateMap<BookCategoryDto, BookCategory>().ReverseMap();
            CreateMap<BookDto, Book>().ReverseMap();
            CreateMap<ClientDto, Client>().ReverseMap();
            CreateMap<BookRentalDto, BookRental>().ReverseMap();
        }
    }
}
