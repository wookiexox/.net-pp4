using System.Net;
using EShop.Application.Service;
using EShop.Domain.Exceptions.CreditCard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders.Physical;

namespace EShopService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        protected ICreditCardService _creditCardService;

        public CreditCardController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }


        [HttpGet]
        public IActionResult Get(string cardNumber)
        {
            try
            {
                _creditCardService.ValidateCardNumber(cardNumber);
                return Ok(new { cardProvider = _creditCardService.GetCardType(cardNumber) });
            }
            catch(CardNumberTooLongException)
            {
                return StatusCode((int)HttpStatusCode.RequestUriTooLong, new { error = "The card number is too long", code = (int)HttpStatusCode.RequestUriTooLong });
            }
            catch(CardNumberTooShortException)
            {
                return BadRequest(new { error = "The card number is too short", code = (int)HttpStatusCode.BadRequest });
            }
            catch(CardNumberInvalidException)
            {
                return BadRequest(new { error = "Invalid card number", code = (int)HttpStatusCode.BadRequest });
            }
        }
    }
}
