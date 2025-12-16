using Microsoft.EntityFrameworkCore;
using indigoLibrary.Domain.Entities;

namespace indigoLibrary.Infrastructure.Data
{
    public class IndigoLibraryDbContext(DbContextOptions<IndigoLibraryDbContext> options) : DbContext(options)
    {
        public DbSet<Book> Books => Set<Book>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Loan> Loans => Set<Loan>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
            .HasKey(l => l.Isbn);


            modelBuilder.Entity<User>()
            .HasKey(u => u.Id);


            modelBuilder.Entity<Loan>()
            .HasKey(p => p.Id);


            modelBuilder.Entity<Loan>()
            .HasOne<Book>()
            .WithMany()
            .HasForeignKey(p => p.Isbn);


            modelBuilder.Entity<Loan>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.UserId);
        }
    }
}