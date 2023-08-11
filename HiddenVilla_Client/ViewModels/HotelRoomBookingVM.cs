using Models.Dto.Order;
using System.ComponentModel.DataAnnotations;

namespace HiddenVilla_Client.ViewModels
{
    public class HotelRoomBookingVM
    {
        [ValidateComplexType]
        public RoomOrderDetailsDTO? OrderDetails { get; set; }
    }
}
