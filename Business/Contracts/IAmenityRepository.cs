using Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IAmenityRepository
    {
        public Task<HotelAmenityDTO> Create(HotelAmenityDTO hotelAmenity);
        public Task<HotelAmenityDTO> Update(int amenityId, HotelAmenityDTO hotelAmenity);
        public Task<int> Delete(int amenityId, string userId);
        public Task<IEnumerable<HotelAmenityDTO>> GetAll();
        public Task<HotelAmenityDTO> GetById(int amenityId);
        public Task<HotelAmenityDTO> AlreadyExists(string name);
    }
}
