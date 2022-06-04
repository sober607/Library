using Library.Business.Services.BookBuilder.Interfaces;

namespace Library.Business.Services.BookBuilder
{
    public class DigitalComic : IAbstractDigitalProduct
    {
        public string ShipmentMethod { get; set; } = "Email";
        public string ProductName { get; set; }

        public DigitalComic(string comicName)
        {
            ProductName = comicName;
        }
    }
}
