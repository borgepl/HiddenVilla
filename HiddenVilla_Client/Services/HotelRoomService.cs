using HiddenVilla_Client.Services.Contracts;
using Models.Dto;
using Newtonsoft.Json;

namespace HiddenVilla_Client.Services
{
    public class HotelRoomService : IHotelRoomService
    {
        private readonly HttpClient _client;

        public HotelRoomService(HttpClient client)
        {
            _client = client;
        }
        public Task<HotelRoomDTO> GetHotelRoomDetails(int roomId, string checkInDate, string checkOutDate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<HotelRoomDTO>> GetHotelRooms(string checkInDate, string checkOutDate)
        {
            var response = await _client.GetAsync($"api/hotelroom?checkInDate={checkInDate}&checkOutDate={checkOutDate}");
            var content = await response.Content.ReadAsStringAsync();
            var rooms = JsonConvert.DeserializeObject<IEnumerable<HotelRoomDTO>>(content);
            return rooms;
        }
    }
}
