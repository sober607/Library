using Library.Business.Services.BookBuilder.Interfaces;

namespace Library.Business.Services.BookBuilder
{
    public class PrintedBook : IAbstractPrintedProduct
    {
        public string ShipmentMethod { get; set; } = "By Mail";
        public string Cover { get; set; }
        public string ProductName { get; set; }

        public PrintedBook(string productName)
        {
            ProductName = productName;
        }
    }
}
