using CommunityLibrary.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommunityLibrary.Infra.Data.DbSchemaBuilder
{
    public class UserSchemaBuider : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("BINARY(16)")
                .HasDefaultValueSql("UUID_TO_BIN(UUID())")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Password)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.ConfirmedPassword)
                  .HasMaxLength(100)
                  .IsRequired();

            builder.Property(x => x.CreatedDate)
                 .HasColumnType("datetime")
                 .HasDefaultValueSql("CURRENT_TIMESTAMP")
                 .IsRequired();

            builder.Property(x => x.LastModifiedDate)
                    .HasColumnType("datetime")
                .IsRequired(false);
  
            builder.Property(x => x.Status)
                .HasColumnType("TINYINT(1)")
                .IsRequired();

            // Relationships  
            builder.HasOne(x => x.Client)
                 .WithOne(a => a.User)
                 .HasForeignKey<Client>(x => x.UserId);

            builder.HasMany(x => x.RegisteredClients)
                 .WithOne(a => a.RegisteredUser)
                 .HasForeignKey(x => x.RegisteredByUserId);
            
            builder.HasMany(x => x.RegisteredBooks)
                .WithOne(a => a.RegisteredUser)
                .HasForeignKey(x => x.RegisteredByUserId);

            builder.HasMany(x => x.RegisteredAuthors)        
                .WithOne(a => a.RegisteredUser)             
                .HasForeignKey(x => x.RegisteredUserId); 


        }
    }
}
