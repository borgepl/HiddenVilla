using Models.Dto;

namespace HiddenVilla_Client.Services.Contracts
{
    public interface IHotelAmenityService
    {
        public Task<IEnumerable<HotelAmenityDTO>> GetHotelAmenities();
    }
}
