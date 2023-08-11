using Models.Dto.Order;
using Models;

namespace HiddenVilla_Client.Services.Contracts
{
    public interface IStripePaymentService
    {
        public Task<SuccessModel> CheckOut(StripePaymentDTO model);
    }
}
