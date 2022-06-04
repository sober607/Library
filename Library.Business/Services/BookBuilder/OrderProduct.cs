using Library.Business.DTO.Book;
using Library.Business.DTO.Client;
using Library.Business.Services.BookBuilder.Interfaces;

namespace Library.Business.Services.BookBuilder
{
    public class OrderProduct
    {
        public void MakeDigitalOrder(BookDto bookDto, ClientDto clientDto)
        {
            string orderDetails;

            switch (bookDto.PublishingHouseId)
            {
                case 1:
                    orderDetails = CreatDigitalProduct(new Marvel(), (clientDto.FirstName + " " + clientDto.LastName), clientDto.Email, bookDto.Title);
                    break;
                case 2:
                    orderDetails = CreatDigitalProduct(new Promin(), (clientDto.FirstName + " " + clientDto.LastName), clientDto.Email, bookDto.Title);
                    break;
                default:
                    orderDetails = "Publishing House is not available for order";
                    break;
            }
        }

        public string CreatDigitalProduct(IAbstractPublishingHouse publishingHouse, string clientName, string email, string productName)
        {
            var digitalProduct = publishingHouse.CreateDigitalProduct(productName);

            var toReturn = $"Product name: {digitalProduct.ProductName}\n" +
                $"Shipment method: {digitalProduct.ShipmentMethod}\n" +
                $"Receiver: {clientName}\n" +
                $"Address: {email}\n";

            return toReturn;
        }

        public string CreatePaperProduct(IAbstractPublishingHouse publishingHouse, string clientName, string address, string productName)
        {
            var paperProduct = publishingHouse.CreatePrintedProduct(productName);

            var toReturn = $"Product name: {productName}\n" +
                $"Product name: {productName}\n" +
                $"Shipment method: {paperProduct.ShipmentMethod}\n" +
                $"Receiver: {clientName}\n" +
                $"Delivery address: {address}\n";

            return toReturn;
        }
    }
}
