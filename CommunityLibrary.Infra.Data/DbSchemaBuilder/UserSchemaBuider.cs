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
                .ValueGeneratedOnAdd()
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
    
            
            builder.HasMany(x => x.RegisteredBooks)
                .WithOne(a => a.RegisteredByUser)
                .HasForeignKey(x => x.RegisteredByUserId);

            builder.HasMany(x => x.RegisteredAuthors)
                .WithOne(a => a.RegisteredByUser)
                .HasForeignKey(x => x.RegisteredByUserId);

            builder.HasMany(x => x.RegisteredBookCategories)
                .WithOne(a => a.RegisteredByUser)
                .HasForeignKey(x => x.RegisteredByUserId);

        }
    }
}
