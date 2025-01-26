using CommunityLibrary.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommunityLibrary.Infra.Data.DbSchemaBuilder
{
    public class BookCategorySchemaBuilder : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            //Table properties  
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("BINARY(16)")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Property(x => x.Status)
                .HasColumnType("TINYINT(1)")
                .IsRequired();

            //Relationships 
            builder.HasMany(x => x.Books)
               .WithOne(x => x.BookCategory)
               .HasForeignKey(x => x.BookCategoryId)
               .OnDelete(DeleteBehavior.Restrict); ;

            builder.HasOne(x => x.RegisteredByUser)  
              .WithMany(x => x.RegisteredBookCategories)    
              .HasForeignKey(x => x.RegisteredByUserId)  
              .OnDelete(DeleteBehavior.Restrict);  


        }
    }
}
