using CommunityLibrary.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommunityLibrary.Infra.Data.DbSchemaBuilder
{
    public class AuthorSchemaBuilder : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            //Table properties
            builder.Property(x => x.Id)
                .HasColumnType("BINARY(16)")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
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
                .WithOne(x => x.Author)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.RegisteredUser)
                .WithOne()
                .HasForeignKey<Author>(x => x.RegisteredUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
