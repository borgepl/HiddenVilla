using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Dto.Order;
using Stripe.Checkout;

namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StripePaymentController : ControllerBase
    {
        private readonly IConfiguration _config;

        public StripePaymentController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Create(StripePaymentDTO payment)
        {
            try
            {
                var domain = _config.GetValue<string>("HiddenVilla_Client_Url");

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string>
                    {
                        "card"
                    },
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount = payment.Amount*100,
                                Currency="eur",
                                ProductData= new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = payment.ProductName
                                }
                            },
                            Quantity=1
                        }
                    },
                    Mode = "payment",
                    SuccessUrl = domain + "/success-payment?session_id={{CHECKOUT_SESSION_ID}",
                    CancelUrl = domain + payment.ReturnUrl
                };

                var service = new Stripe.Checkout.SessionService();
                Session session = await service.CreateAsync(options);

                return Ok(new SuccessModel()
                {
                    Data = session.Id
                });

            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
