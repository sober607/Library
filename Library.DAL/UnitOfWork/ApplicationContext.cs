using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Library.DAL.Entities;
using Library.DAL.EntitiesConfiguration;

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
            modelBuilder.ApplyConfiguration(new BookAuthorEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BookEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CountryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PersonEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PublishingHouseEntityConfiguration());
        }
    }
}
