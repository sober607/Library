using Library.Business.Services.BookBuilder.Interfaces;

namespace Library.Business.Services.BookBuilder
{
    public class DigitalBook : IAbstractDigitalProduct
    {
        public string ShipmentMethod { get; set; } = "Email";
        public string ProductName { get; set; }

        public DigitalBook(string productName)
        {
            ProductName = productName;
        }
    }
}
