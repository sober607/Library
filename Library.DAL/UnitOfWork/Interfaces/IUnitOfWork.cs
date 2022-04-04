using System;
using System.Threading.Tasks;
using Library.DAL.Entities;
using Library.DAL.Repositories;
using Library.DAL.Repositories.Interfaces;

namespace Library.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Book> Books { get; }

        ICountryRepository Countries { get; }

        IRepository<Person> Persons { get; }

        IRepository<PublishingHouse> PublishingHouses { get; }

        IBookAuthorRepository BookAuthors { get; }

        Task SaveAsync();
    }
}
