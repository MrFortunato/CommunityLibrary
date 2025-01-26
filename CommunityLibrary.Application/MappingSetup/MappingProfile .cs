using AutoMapper;
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

            CreateMap<AuthorDetailsRequest, Author>().ReverseMap()
                .ForMember(a => a.RegisteredByUserName, opt => opt.MapFrom(b => b.RegisteredByUser.Name));
            CreateMap<AuthorCreateRequest, Author>().ReverseMap()
                 .ForMember(a => a.RegisteredByUserId, opt => opt.MapFrom(b => b.RegisteredByUser.Id));
            CreateMap<AuthorUpdateRequest, Author>().ReverseMap();
                

            CreateMap<BookCategoryDetailsRequest, BookCategory>().ReverseMap()
                .ForMember(a => a.RegisteredByUserName, opt => opt.MapFrom(b => b.RegisteredByUser.Name)); 
            CreateMap<BookCategoryCreateRequest, BookCategory>().ReverseMap()
                  .ForMember(a => a.RegisteredByUserId, opt => opt.MapFrom(b => b.RegisteredByUser.Id)); 
            CreateMap<BookCategoryUpdateRequest, BookCategory>().ReverseMap();

            CreateMap<BookCreateRequest, Book>().ReverseMap()
                  .ForMember(a => a.RegisteredByUserId, opt => opt.MapFrom(b => b.RegisteredByUser.Id))
                  .ForMember(a => a.BookCategoryId, opt => opt.MapFrom(b => b.BookCategory.Id));
            CreateMap<BookUpdateRequest, Book>().ReverseMap()
                  .ForMember(a => a.BookCategoryId, opt => opt.MapFrom(b => b.BookCategory.Id));
            CreateMap<BookDetailsRequest, Book>().ReverseMap()
             .ForMember(d => d.BookCategory, opt => opt.MapFrom(b => b.BookCategory.Description))
             .ForMember(a => a.AuthorName, opt => opt.MapFrom(b =>b.Author.Name))
              .ForMember(a => a.RegisteredByUserName, opt => opt.MapFrom(b => b.RegisteredByUser.Name)); ;
            
            CreateMap<ClientCreateRequest, Client>().ReverseMap();
            CreateMap<ClientUpdateRequest, Client>().ReverseMap();
            CreateMap<ClientDetailsRequest, Client>().ReverseMap()
                .ForMember(c => c.Name, opt => opt.MapFrom(u =>u.User.Name))
                .ForMember(c => c.Email, opt => opt.MapFrom(u => u.User.Email));
      


        }
    }
}
