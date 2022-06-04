using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Business.Services.BookBuilder.Interfaces
{
    public interface IAbstractPublishingHouse
    {
        IAbstractDigitalProduct CreateDigitalProduct(string productName);

        IAbstractPrintedProduct CreatePrintedProduct(string productName);
    }
}
