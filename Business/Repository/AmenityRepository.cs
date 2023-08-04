using AutoMapper;
using Business.Contracts;
using DataAccess.Data;
using DataAccess.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class AmenityRepository : IAmenityRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AmenityRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<HotelAmenityDTO> AlreadyExists(string name)
        {
            try
            {
                var amenityDetails =
                    await _context.HotelAmenities.FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
                return _mapper.Map<HotelAmenity, HotelAmenityDTO>(amenityDetails);
            }
            catch (Exception)
            {

            }
            return new HotelAmenityDTO();
        }

        public async Task<HotelAmenityDTO> Create(HotelAmenityDTO hotelAmenity)
        {
            var amenity = _mapper.Map<HotelAmenityDTO, HotelAmenity>(hotelAmenity);
            var addedHotelAmenity = await _context.HotelAmenities.AddAsync(amenity);
            await _context.SaveChangesAsync();
            return _mapper.Map<HotelAmenity, HotelAmenityDTO>(addedHotelAmenity.Entity);

        }

        public async Task<int> Delete(int amenityId, string userId)
        {
            var amenityDetails = await _context.HotelAmenities.FindAsync(amenityId);
            if (amenityDetails != null)
            {
                _context.HotelAmenities.Remove(amenityDetails);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<HotelAmenityDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<HotelAmenity>, IEnumerable<HotelAmenityDTO>>(await _context.HotelAmenities.ToListAsync());

        }

        public async Task<HotelAmenityDTO> GetById(int amenityId)
        {
            var amenityData = await _context.HotelAmenities.FirstOrDefaultAsync(x => x.Id == amenityId);

            if (amenityData == null)
            {
                return null;
            }
            return _mapper.Map<HotelAmenity, HotelAmenityDTO>(amenityData);
        }

        public async Task<HotelAmenityDTO> Update(int amenityId, HotelAmenityDTO hotelAmenity)
        {
            var amenityDetails = await _context.HotelAmenities.FindAsync(amenityId);
            var amenity = _mapper.Map(hotelAmenity, amenityDetails);
           
            var updatedamenity = _context.HotelAmenities.Update(amenity);
            await _context.SaveChangesAsync();
            return _mapper.Map<HotelAmenity, HotelAmenityDTO>(updatedamenity.Entity);

        }
    }
}
