using CommunityLibrary.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommunityLibrary.Infra.Data.DbSchemaBuilder
{
    public class BookRentalSchemaBuider : IEntityTypeConfiguration<BookRental>
    {
        public void Configure(EntityTypeBuilder<BookRental> builder)
        {
            //Table properties

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnType("BINARY(16)")
               .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.BookId)
            .HasColumnType("BINARY(16)")
            .ValueGeneratedOnAdd()
            .IsRequired();

            builder.Property(x => x.RegisteredByUserId)
                .HasColumnType("BINARY(16)")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.ClientId)
                .HasColumnType("BINARY(16)")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.RentalDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Property(x => x.ReturnDate)
                .HasColumnType("datetime")
                .IsRequired(false);

            builder.Property(x => x.Returned)
                .HasColumnType("TINYINT(1)")
                .IsRequired();

            //Relationships 
            builder.HasOne(x => x.Book)
                .WithMany(x => x.BookRentals)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.RegisteredByUser)
                .WithMany(x => x.BookRentals)
                .HasForeignKey(x => x.RegisteredByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Client)
                .WithMany(x => x.BookRentals)
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
