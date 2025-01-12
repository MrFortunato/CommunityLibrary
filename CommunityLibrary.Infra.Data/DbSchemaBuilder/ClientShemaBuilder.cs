using CommunityLibrary.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommunityLibrary.Infra.Data.DbSchemaBuilder
{
    public class ClientShemaBuilder : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            //Table properties
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("BINARY(16)")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.UserId)
                .HasColumnType("BINARY(16)")
                .IsRequired();

            builder.Property(x => x.CreatedDate)
                .HasColumnType("datetime")
                 .HasDefaultValueSql("CURRENT_TIMESTAMP")
                 .IsRequired();


            builder.Property(x => x.Status)
                .HasColumnType("TINYINT(1)")
                .IsRequired();


            builder.HasMany(x => x.BookRentals)
                .WithOne(x => x.Client)
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
