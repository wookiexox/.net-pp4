using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Service
{
    public interface ICreditCardService
    {
        public Boolean ValidateCardNumber(string cardNumber);

        public string GetCardType(string cardNumber);
    }
}
