using Library.Business.Services.BookBuilder.Interfaces;

namespace Library.Business.Services.BookBuilder
{
    public class Marvel : IAbstractPublishingHouse
    {
        public string PublishingHouseName { get; set; } = "Marvel";

        public IAbstractDigitalProduct CreateDigitalProduct(string productName)
        {
            return new DigitalComic(productName);
        }

        public IAbstractPrintedProduct CreatePrintedProduct(string productName)
        {
            return new PaperComic(productName);
        }
    }
}
