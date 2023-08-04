using AutoMapper;
using Business.Contracts;
using DataAccess.Data;
using DataAccess.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Models.Dto;
using static System.Net.Mime.MediaTypeNames;

namespace Business.Repository
{
    public class HotelImagesRepository : IHotelImagesRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public HotelImagesRepository(ApplicationDbContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        public async Task<int> Create(HotelRoomImageDTO imageDTO)
        {
            var image = _mapper.Map<HotelRoomImage>(imageDTO);
            await _db.HotelRoomImages.AddAsync(image);
            return await _db.SaveChangesAsync();

        }

        public async Task<int> DeleteByImageId(int imageId)
        {
            var image = await _db.HotelRoomImages.FindAsync(imageId);
            _db.HotelRoomImages.Remove(image);
            return await _db.SaveChangesAsync();

        }

        public async Task<int> DeleteByImageUrl(string imageUrl)
        {
            var image = await _db.HotelRoomImages.Where(x => x.RoomImageUrl.ToLower() == imageUrl.ToLower()).FirstOrDefaultAsync();
            if (image == null)
            {
                return 0;
            }
            _db.HotelRoomImages.Remove(image);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteByRoomId(int roomId)
        {
            var images = await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync();
            _db.HotelRoomImages.RemoveRange(images);
            return await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<HotelRoomImageDTO>> GetByRoomId(int roomId)
        {
            return _mapper.Map<IEnumerable<HotelRoomImageDTO>>(
            await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync());
        }
    }
}
