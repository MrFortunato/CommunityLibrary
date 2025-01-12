using AutoMapper;
using CommunityLibrary.Application.DTO;
using CommunityLibrary.Application.Request;
using CommunityLibrary.Domain;

namespace CommunityLibrary.Application.MappingSetup
{
    public class MappingProfile :Profile
    {
        public MappingProfile ()
        {
            SourceMemberNamingConvention = LowerUnderscoreNamingConvention.Instance;
            DestinationMemberNamingConvention = PascalCaseNamingConvention.Instance;

            CreateMap<UserDetailsRequest, User>().ReverseMap();
            CreateMap<UserCreateRequest, User>().ReverseMap();
            CreateMap<UserUpdateRequest, User>().ReverseMap();

            CreateMap<AuthorDeatailsRequest, Author>().ReverseMap();
            CreateMap<AuthorCreateRequest, Author>().ReverseMap();
            CreateMap<AuthorUpdateRequest, Author>().ReverseMap();

            CreateMap<BookCategoryDetailsRequest, BookCategory>().ReverseMap();
            CreateMap<BookCategoryCreateRequest, BookCategory>().ReverseMap();
            CreateMap<BookCategoryUpdateRequest, BookCategory>().ReverseMap();

            CreateMap<BookCreateRequest, Book>().ReverseMap();
            CreateMap<BookUpdateRequest, Book>().ReverseMap();
            CreateMap<BookDetailsRequest, Book>().ReverseMap()
             .ForMember(d => d.BookCategory, opt => opt.MapFrom(b => b.BookCategory.Description))
             .ForMember(a => a.AuthorName, opt => opt.MapFrom(b =>b.Author.Name));
            
            CreateMap<ClientCreateRequest, Client>().ReverseMap();
            CreateMap<ClientUpdateRequest, Client>().ReverseMap();
            CreateMap<ClientDetailsRequest, Client>().ReverseMap()
                .ForMember(c => c.Name, opt => opt.MapFrom(u =>u.User.Name))
                .ForMember(c => c.Email, opt => opt.MapFrom(u => u.User.Email));
      
            CreateMap<BookRentalDto, BookRental>().ReverseMap();
 

        }
    }
}
