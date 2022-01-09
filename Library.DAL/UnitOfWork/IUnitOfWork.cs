using System;
using Library.DAL.Entities;
using Library.DAL.Repository;

namespace Library.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Book> Books { get; }

        IRepository<Country> Coutries { get; }

        IRepository<Person> Persons { get; }

        IRepository<PublishingHouse> PublishingHouses { get; }

        void Save();
    }
}
