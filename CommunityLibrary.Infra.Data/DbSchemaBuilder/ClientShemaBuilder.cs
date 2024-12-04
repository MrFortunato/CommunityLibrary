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
                .HasDefaultValueSql("UUID_TO_BIN(UUID())")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.CreatedDate)
                .HasColumnType("datetime")
                 .HasDefaultValueSql("CURRENT_TIMESTAMP")
                 .IsRequired();

            builder.Property(x => x.RegisteredByUserId)
                .HasColumnType("BINARY(16)")
                .HasDefaultValueSql("UUID_TO_BIN(UUID())")
                .IsRequired();

            builder.Property(x => x.Status)
                .HasColumnType("TINYINT(1)")
                .IsRequired();

            //Relationships
            builder.HasOne(x => x.RegisteredUser)
                    .WithMany()
                    .HasForeignKey(x => x.RegisteredByUserId);

            builder.HasMany(x => x.BookRentals)
                .WithOne(x => x.Client)
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User)               
                .WithOne(x => x.Client)                
                .HasForeignKey<Client>(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);   

        }
    }
}
