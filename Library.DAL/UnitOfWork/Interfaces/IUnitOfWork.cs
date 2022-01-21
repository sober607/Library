using System;
using System.Threading.Tasks;
using Library.DAL.Entities;
using Library.DAL.Repositories;

namespace Library.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Book> Books { get; }

        IRepository<Country> Countries { get; }

        IRepository<Person> Persons { get; }

        IRepository<PublishingHouse> PublishingHouses { get; }

        Task SaveAsync();
    }
}
