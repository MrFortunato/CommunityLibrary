using CommunityLibrary.Domain;
using Microsoft.EntityFrameworkCore;

namespace CommunityLibrary.Infra.Data.AppDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<BookRental> BookRentals { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Client>()
                .Property(c => c.Id)
                .HasColumnType("CHAR(36)") // Usando CHAR(36) para armazenar o GUID
                .IsRequired();

            modelBuilder.Entity<Book>()
                .Property(c => c.Id)
                .HasColumnType("CHAR(36)") // Usando CHAR(36) para armazenar o GUID
                .IsRequired();

            modelBuilder.Entity<Book>()
          .Property(c => c.AuthorId)
          .HasColumnType("CHAR(36)") // Usando CHAR(36) para armazenar o GUID
          .IsRequired();

            modelBuilder.Entity<Book>()
     .Property(c => c.RegisteredByUserId)
     .HasColumnType("CHAR(36)") // Usando CHAR(36) para armazenar o GUID
     .IsRequired();

            modelBuilder.Entity<Book>()
.Property(c => c.BookCategoryId)
.HasColumnType("CHAR(36)") // Usando CHAR(36) para armazenar o GUID
.IsRequired();

            modelBuilder.Entity<User>()
           .Property(c => c.Id)
           .HasColumnType("CHAR(36)") // Usando CHAR(36) para armazenar o GUID
           .IsRequired();

            modelBuilder.Entity<Book>()
                .HasOne(br => br.RegisteredUser)
                .WithMany(c => c.RegisteredBooks)
                .HasForeignKey(br => br.RegisteredByUserId);
            // Relacionamento um-para-um entre Client e Use
            modelBuilder.Entity<Client>()
                .HasOne(c => c.User) 
                .WithOne(c => c.Client) 
                .HasForeignKey<Client>(c => c.UserId) 
                .OnDelete(DeleteBehavior.Restrict);



             modelBuilder.Entity<Client>()
                .HasOne(c => c.User) 
                .WithMany(u => u.RegisteredClients) 
                .HasForeignKey(c => c.RegisteredByUserId) 
                .OnDelete(DeleteBehavior.Restrict); 

            // Relacionamento um-para-muitos entre Client e BookRental
            modelBuilder.Entity<BookRental>()
                .HasOne(br => br.Client) 
                .WithMany(c => c.BookRentals) 
                .HasForeignKey(br => br.ClientId); 

            modelBuilder.Entity<BookRental>()
                .HasOne(br => br.User)
                .WithMany(c => c.BookRentals) 
                .HasForeignKey(br => br.UserId); 

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author) 
                .WithMany(a => a.Books) 
                .HasForeignKey(b => b.AuthorId) 
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Book>()
                .HasOne(b => b.BookCategory) 
                .WithMany(a => a.Books) 
                .HasForeignKey(b => b.BookCategoryId) 
                .OnDelete(DeleteBehavior.Restrict);

        
            modelBuilder.Entity<BookRental>()
                .HasOne(br => br.Book) 
                .WithMany(b => b.BookRentals) 
                .HasForeignKey(br => br.BookId) 
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookRental>()
                .HasOne(br => br.User) 
                .WithMany(u => u.BookRentals) 
                .HasForeignKey(br => br.UserId) 
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<BookRental>()
               .HasOne(br => br.Client) 
               .WithMany(u => u.BookRentals)
               .HasForeignKey(br => br.ClientId) 
               .OnDelete(DeleteBehavior.Restrict); 

            base.OnModelCreating(modelBuilder);
        }
    }
}
