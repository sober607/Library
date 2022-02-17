using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Library.DAL.Entities;

namespace Library.DAL.UnitOfWork
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<PublishingHouse> PublishingHouse { get; set; }
        public DbSet<BookAuthor> BookAuthor { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        //Many-To-Many table relationship
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>()
                    .HasOne(d => d.Book)
                    .WithMany(t => t.BookAuthors)
                    .HasForeignKey(f => f.BookId)
                    .HasConstraintName("FK_BooksOfAuthor");

            modelBuilder.Entity<BookAuthor>()
                    .HasOne(d => d.Author)
                    .WithMany(t => t.AuthorBooks)
                    .HasForeignKey(f => f.AuthorId)
                    .HasConstraintName("FK_AuthorsOfBook");

            modelBuilder.Entity<BookAuthor>()
                    .HasIndex(f => new { f.AuthorId, f.BookId })
                    .IsUnique();

            modelBuilder.Entity<BookAuthor>()
                .Property(x => x.BookId)
                .IsRequired();

            modelBuilder.Entity<BookAuthor>()
                .Property(x => x.AuthorId)
                .IsRequired();

            modelBuilder.Entity<Book>()
                .Property(x => x.Title)
                .IsRequired();

            modelBuilder.Entity<Book>()
                .Property(x => x.Title)
                .HasMaxLength(1000);

            modelBuilder.Entity<Country>()
                .Property(x => x.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<Country>()
                .Property(x => x.Name)
                .IsRequired();

            modelBuilder.Entity<Person>()
                .Property(x => x.FirstName)
                .IsRequired();

            modelBuilder.Entity<Person>()
                .Property(x => x.FirstName)
                .HasMaxLength(50);

            modelBuilder.Entity<PublishingHouse>()
                .Property(x => x.Name)
                .HasMaxLength(500);

            modelBuilder.Entity<PublishingHouse>()
                .Property(x => x.Name)
                .IsRequired();
        }
    }
}
