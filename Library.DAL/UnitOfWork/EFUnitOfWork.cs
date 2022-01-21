using System.Threading.Tasks;
using Library.DAL.Entities;
using Library.DAL.Repositories;

namespace Library.DAL.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _applicationContext;
        private BookRepository _bookRepository;
        private CountryRepository _countryRepository;
        private PersonRepository _personRepository;
        private PublishingHouseRepository _publishingHouseRepository;

        public EFUnitOfWork(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public IRepository<Book> Books
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

        public IRepository<Country> Countries
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

        public IRepository<Person> Persons
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

        public IRepository<PublishingHouse> PublishingHouses
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

        public async Task SaveAsync()
        {
            await _applicationContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _applicationContext.Dispose();
        }

        //To add BeginTransactionMethod, _applicationContext.ROllback()
    }
}
