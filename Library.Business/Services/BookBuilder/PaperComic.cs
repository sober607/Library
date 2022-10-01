using Library.Business.Services.BookBuilder.Interfaces;

namespace Library.Business.Services.BookBuilder
{
    public class PaperComic : IAbstractPrintedProduct
    {
        public string ShipmentMethod { get; set; } = "Regular Mail";
        public string Cover { get; set; } = "Paper cover";
        public string Extras { get; set; } = string.Empty;
        public string ProductName { get; set; }

        public PaperComic(string comicName)
        {
            ProductName = comicName;
        }
    }
}
