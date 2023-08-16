using Business.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Dto.Order;
using Stripe.Checkout;

namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomOrderController : ControllerBase
    {
        private readonly IRoomOrderDetailsRepository _roomOrderDetailsRepository;
        private readonly IEmailSender _emailSender;

        public RoomOrderController(IRoomOrderDetailsRepository roomOrderDetailsRepository, IEmailSender emailSender)
        {
            _roomOrderDetailsRepository = roomOrderDetailsRepository;
            _emailSender = emailSender;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomOrderDetailsDTO details)
        {
            if (ModelState.IsValid)
            {
                var result = await _roomOrderDetailsRepository.Create(details);
                return Ok(result);
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while creating the details for the booking."
                });
            };
        }

        [HttpPost]
        public async Task<IActionResult> PaymentSuccessful([FromBody] RoomOrderDetailsDTO details)
        {
            var service = new SessionService();
            var sessionDetails = service.Get(details.StripeSessionId);
            if (sessionDetails.PaymentStatus == "paid")
            {
                var result = await _roomOrderDetailsRepository.MarkPaymentSuccessful(details.Id);
                if (result == null)
                {
                    return BadRequest(new ErrorModel()
                    {
                        ErrorMessage = "Cannot mark payment as successful. - retrieve error order"
                    });
                }
                //await _emailSender.SendEmailAsync(details.Email, "Booking Confirmed - Hidden Villa",
                //   "Your booking has been confirmed at Hidden Villa with order ID :" + details.Id);
                return Ok(result);
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Cannot mark payment as successful."
                });
            }
        }
    }
}
