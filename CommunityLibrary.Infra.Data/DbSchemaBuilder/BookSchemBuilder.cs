using CommunityLibrary.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommunityLibrary.Infra.Data.DbSchemaBuilder
{
    public class BookSchemBuilder : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnType("BINARY(16)")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.AuthorId)
                .HasColumnType("BINARY(16)")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.BookCategoryId)
                .HasColumnType("BINARY(16)")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.RegisteredByUserId)
                .HasColumnType("BINARY(16)")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();


            builder.Property(x => x.Status)
                .HasColumnType("TINYINT(1)")
                .IsRequired();

            builder.HasOne(x => x.Author)
                    .WithMany(a => a.Books)
                .HasForeignKey(x => x.AuthorId);

            builder.HasOne(x => x.BookCategory)
                .WithMany(a => a.Books)
                .HasForeignKey(x => x.BookCategoryId);

            builder.HasOne(x => x.RegisteredByUser)
                    .WithMany(a => a.RegisteredBooks)
                .HasForeignKey(x => x.RegisteredByUserId);

        }
    }
}
