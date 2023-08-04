using Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IHotelImagesRepository
    {
        public Task<int> Create(HotelRoomImageDTO image);
        public Task<int> DeleteByImageId(int imageId);
        public Task<int> DeleteByRoomId(int roomId);
        public Task<int> DeleteByImageUrl(string imageUrl);
        public Task<IEnumerable<HotelRoomImageDTO>> GetByRoomId(int roomId);
    }
}
