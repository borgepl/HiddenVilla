using Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IHotelRoomRepository
    {
        public Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoomDTO);
        public Task<HotelRoomDTO> Update(int roomId, HotelRoomDTO hotelRoomDTO);
        public Task<HotelRoomDTO> GetById(int roomId);
        public Task<IEnumerable<HotelRoomDTO>> GetAll();
        public Task<HotelRoomDTO> IsRoomUnique(string name);
        public Task<int> Delete(int roomId);
    }
}
