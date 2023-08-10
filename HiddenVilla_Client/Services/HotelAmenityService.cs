using HiddenVilla_Client.Services.Contracts;
using Models.Dto;
using Newtonsoft.Json;

namespace HiddenVilla_Client.Services
{
    public class HotelAmenityService : IHotelAmenityService
    {
        private readonly HttpClient _client;

        public HotelAmenityService(HttpClient client)
        {
            _client = client;
        }
        public async Task<IEnumerable<HotelAmenityDTO>> GetHotelAmenities()
        {
            var response = await _client.GetAsync("api/amenity");
            var content = await response.Content.ReadAsStringAsync();
            var amenities = JsonConvert.DeserializeObject<IEnumerable<HotelAmenityDTO>>(content);
            return amenities;
        }
    }
}
