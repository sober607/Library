namespace Library.Business.Services.BookBuilder.Interfaces
{
    public interface IAbstractPrintedProduct
    {
        public string ShipmentMethod { get; set; }

        public string Cover { get; set; }

        public string ProductName { get; set; }
    }
}
