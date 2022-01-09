using Library.DAL.Entities;
using Library.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _applicationContext;
        private BookRepository _bookRepository;
        private CountryRepository _countryRepository;
        private PersonRepository _personRepository;
        private PublishingHouseReposiroty _publishingHouseRepository;

        public EFUnitOfWork(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public IRepository<Book> BookRepository
        {
            get
            {
                if (_bookRepository == null)
                {
                    _bookRepository = new BookRepository(_applicationContext);
                }

                return _bookRepository;
            }
        }

        public IRepository<Country> CountryRepository
        {
            get
            {
                if (_countryRepository == null)
                {
                    _countryRepository = new CountryRepository(_applicationContext);
                }

                return _countryRepository;
            }
        }

        public IRepository<Person> PersonRepository
        {
            get
            {
                if (_personRepository == null)
                {
                    _personRepository = new PersonRepository(_applicationContext);
                }

                return _personRepository;
            }
        }

        public IRepository<PublishingHouse> PublishingHouseRepository
        {
            get
            {
                if (_publishingHouseRepository == null)
                {
                    _publishingHouseRepository = new PublishingHouseRepository(_applicationContext);
                }

                return _publishingHouseRepository;
            }
        }

        public async void SaveAsync()
        {
            await _applicationContext.SaveChangesAsync();
        }

        public void RollBack()
        {
            _applicationContext.Dispose();
        }

        //To add BeginTransactionMethod, _applicationContext.ROllback()
    }
}
