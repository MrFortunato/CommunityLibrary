using AutoMapper;
using CommunityLibrary.Application.Request;
using CommunityLibrary.Domain;

namespace CommunityLibrary.Application.MappingSetup
{
    public class MappingProfile :Profile
    {
        public MappingProfile ()
        {
            ConfigureNamingConventions();
            CreateUserMapping();
            CreateAuthorMapping();
            CreateClientMapping();
            CreateBookCategoryMapping();
            CreateBookMapping();

        }
        private void ConfigureNamingConventions()
        {
            SourceMemberNamingConvention = LowerUnderscoreNamingConvention.Instance;
            DestinationMemberNamingConvention = PascalCaseNamingConvention.Instance;
        }
        private void CreateUserMapping()
        {
            CreateMap<UserDetailsRequest, User>().ReverseMap();
            CreateMap<UserCreateRequest, User>().ReverseMap();
            CreateMap<UserUpdateRequest, User>().ReverseMap();
        }

        private void CreateAuthorMapping()
        {
            CreateMap<AuthorDetailsRequest, Author>().ReverseMap()
                .ForMember(a => a.RegisteredByUserName, opt => opt.MapFrom(b => b.RegisteredByUser.Name));
            CreateMap<AuthorCreateRequest, Author>().ReverseMap()
               .ForMember(a => a.RegisteredByUserId, opt => opt.MapFrom(b => b.RegisteredByUser.Id));
            CreateMap<AuthorUpdateRequest, Author>().ReverseMap();
        }
        private void CreateClientMapping()
        {
            CreateMap<ClientCreateRequest, Client>().ReverseMap();
            CreateMap<ClientUpdateRequest, Client>().ReverseMap();
            CreateMap<ClientDetailsRequest, Client>().ReverseMap()
               .ForMember(c => c.Name, opt => opt.MapFrom(u => u.User.Name))
               .ForMember(c => c.Email, opt => opt.MapFrom(u => u.User.Email));
        }
        private void CreateBookCategoryMapping()
        {

            CreateMap<BookCategoryDetailsRequest, BookCategory>().ReverseMap()
               .ForMember(a => a.RegisteredByUserName, opt => opt.MapFrom(b => b.RegisteredByUser.Name));
            CreateMap<BookCategoryCreateRequest, BookCategory>().ReverseMap()
               .ForMember(a => a.RegisteredByUserId, opt => opt.MapFrom(b => b.RegisteredByUser.Id));
            CreateMap<BookCategoryUpdateRequest, BookCategory>().ReverseMap();
        }
        private void CreateBookMapping()
        {
            CreateMap<BookCreateRequest, Book>().ReverseMap()
               .ForMember(a => a.RegisteredByUserId, opt => opt.MapFrom(b => b.RegisteredByUser.Id))
               .ForMember(a => a.BookCategoryId, opt => opt.MapFrom(b => b.BookCategory.Id));
            CreateMap<BookUpdateRequest, Book>().ReverseMap()
               .ForMember(a => a.BookCategoryId, opt => opt.MapFrom(b => b.BookCategory.Id));
            CreateMap<BookDetailsRequest, Book>().ReverseMap()
               .ForMember(d => d.BookCategory, opt => opt.MapFrom(b => b.BookCategory.Description))
               .ForMember(a => a.AuthorName, opt => opt.MapFrom(b => b.Author.Name))
               .ForMember(a => a.RegisteredByUserName, opt => opt.MapFrom(b => b.RegisteredByUser.Name)); ;
        }   
        private void CreatePaginationMapping()
        {
            CreateMap<PaginatedResponse<User>, PaginatedResultService<UserDetailsRequest>>().ReverseMap();
            CreateMap<PaginatedResponse<Client>, PaginatedResultService<ClientDetailsRequest>>().ReverseMap();
            CreateMap<PaginatedResponse<Author>, PaginatedResultService<AuthorDetailsRequest>>().ReverseMap();
            CreateMap<PaginatedResponse<BookCategory>, PaginatedResultService<BookCategoryDetailsRequest>>().ReverseMap();
            CreateMap<PaginatedResponse<Book>, PaginatedResultService<BookDetailsRequest>>().ReverseMap();
        }

    }
}
