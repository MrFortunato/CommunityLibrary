using CommunityLibrary.Application.DTO;
using CommunityLibrary.Application.Interfaces;
using CommunityLibrary.Application.MappingSetup;
using CommunityLibrary.Application.Services;
using CommunityLibrary.Domain;
using CommunityLibrary.Infra.Data;
using CommunityLibrary.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CommunityLibrary.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Database connection string is not configured.");
            }
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(connectionString,
                    new MySqlServerVersion(new Version(5, 5, 37)));
            });
            return services;    
        } 
        public static IServiceCollection ConfigureRepositoryDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<BookCategory>), typeof(BookCategoryRepository));
            services.AddScoped(typeof(IGenericRepository<Book>), typeof(BookRepository));
            services.AddScoped(typeof(IGenericRepository<User>), typeof(UserRepository));
            services.AddScoped(typeof(IGenericRepository<Client>), typeof(ClientRepository));
            services.AddScoped(typeof(IGenericRepository<BookRental>), typeof(BookRentalRepository));

            //services.AddScoped(typeof(IGenerecService<BookCategoryDto>),typeof(BookCategoryService));
            services.AddScoped(typeof(IGenerecService<UserDto>), typeof(UserService));

            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }

       
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            // Adiciona os serviços necessários para a API
     

            return services;
        }

    }
}
