using Models.Dto;

namespace HiddenVilla_Client.Services.Contracts
{
    public interface IHotelRoomService
    {
        public Task<IEnumerable<HotelRoomDTO>> GetHotelRooms(string checkInDate, string checkOutDate);
        public Task<HotelRoomDTO> GetHotelRoomDetails(int roomId, string checkInDate, string checkOutDate);
    }
}
