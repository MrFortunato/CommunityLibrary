using CommunityLibrary.Application.Interfaces;
using CommunityLibrary.Application.MappingSetup;
using CommunityLibrary.Application.Security;
using CommunityLibrary.Application.Services;
using CommunityLibrary.Domain;
using CommunityLibrary.Domain.Repositories;
using CommunityLibrary.Infra.Data;
using CommunityLibrary.Infra.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;


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
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped<IUserRepository, UserRepository>(); 
            services.AddScoped(typeof(IGenericRepository<Client>), typeof(ClientRepository));
            services.AddScoped(typeof(IGenericRepository<BookRental>), typeof(BookRentalRepository));
            services.AddScoped(typeof(IGenericRepository<Author>), typeof(AuthorRepository));

            services.AddScoped(typeof(IBookService), typeof(BookService));
            services.AddScoped(typeof(IAuthorService), typeof(AuthorService));
            services.AddScoped(typeof(IClientService), typeof(ClientService));
            services.AddScoped(typeof(IUserService), typeof(UserService));
            services.AddScoped(typeof(IBookCategoryService), typeof(BookCategoryService));

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            return services;
        }


        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JwtSettings:Audience"],
                    ValidateLifetime = true,
                    //ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }


    }
}
