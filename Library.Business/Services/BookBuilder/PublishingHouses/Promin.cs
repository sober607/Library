using Library.Business.Services.BookBuilder.Interfaces;

namespace Library.Business.Services.BookBuilder
{
    public class Promin : IAbstractPublishingHouse
    {
        public string PublishingHouseName { get; set; } = "Promin";

        public IAbstractDigitalProduct CreateDigitalProduct(string productName)
        {
            return new DigitalBook(productName);
        }

        public IAbstractPrintedProduct CreatePrintedProduct(string productName)
        {
            return new PrintedBook(productName);
        }
    }
}
