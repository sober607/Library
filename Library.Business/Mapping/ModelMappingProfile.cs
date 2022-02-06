using AutoMapper;
using Library.Business.DTO.Book;
using Library.Business.DTO.Country;
using Library.Business.DTO.Person;
using Library.Business.DTO.PublishingHouse;
using Library.DAL.Entities;
using System.Linq;

namespace Library.Business.Mapping
{
    public class ModelMappingProfile : Profile
    {
        public ModelMappingProfile()
        {
            // To review and edit
            CreateMap<Book, BookDto>().ForMember(destination => destination.AuthorIds, conf => conf.MapFrom(x => x.BookAuthors.Select(y => y.AuthorId).ToList())); // Проверитьб нужен ли ToLLIst
            CreateMap<BookDto, Book>();

            CreateMap<CreateBookDto, Book>();

            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();

            CreateMap<Person, PersonDto>();
            CreateMap<PersonDto, Person>();

            CreateMap<PublishingHouse, PublishingHouseDto>();
            CreateMap<PublishingHouseDto, PublishingHouseDto>();
        }
    }
}
