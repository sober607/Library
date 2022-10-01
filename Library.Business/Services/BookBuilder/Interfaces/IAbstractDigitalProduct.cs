namespace Library.Business.Services.BookBuilder.Interfaces
{
    public interface IAbstractDigitalProduct
    {
        public string ShipmentMethod { get; set; }

        public string ProductName { get; set; }
    }
}
