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
    public class HotelRoomRepository : IHotelRoomRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public HotelRoomRepository( ApplicationDbContext context, IMapper mapper)
        {
           _db = context;
           _mapper = mapper;
        }
        public async Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoomDTO)
        {
            var addedHotelRoom = await _db.HotelRooms.AddAsync(_mapper.Map<HotelRoom>(hotelRoomDTO));

            await _db.SaveChangesAsync();

            return _mapper.Map<HotelRoomDTO>(addedHotelRoom.Entity);
        }

        public async Task<bool> IsRoomBooked(int roomId, string checkInDateStr, string checkOutDateStr)
        {
            try
            {
                if(!string.IsNullOrEmpty(checkOutDateStr) && !string.IsNullOrEmpty(checkInDateStr))
                {
                    DateTime checkInDate = DateTime.ParseExact(checkInDateStr, "dd/MM/yyyy", null);
                    DateTime checkOutDate = DateTime.ParseExact(checkOutDateStr, "dd/MM/yyyy", null);

                    var existingBooking = await _db.RoomOrderDetails.Where(x => x.RoomId == roomId && x.IsPaymentSuccessful &&
                                        ((checkInDate < x.CheckOutDate && checkInDate.Date >= x.CheckInDate)
                                                ||
                                        (checkOutDate.Date > x.CheckInDate.Date && checkInDate.Date <= x.CheckInDate.Date)
                                        )).FirstOrDefaultAsync();

                    if (existingBooking != null)
                    {
                        return true;
                    }
                    return false;
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<HotelRoomDTO> IsRoomUnique(string name, int roomId = 0)
        {
            try
            {
                if (roomId == 0)
                {
                    HotelRoom hotelRoom = await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());

                    return _mapper.Map<HotelRoomDTO>(hotelRoom);
                }
                else
                {
                    HotelRoom hotelRoom = await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()
                                                && x.Id != roomId);

                    return _mapper.Map<HotelRoomDTO>(hotelRoom);
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<IEnumerable<HotelRoomDTO>> GetAll()
        {
            try
            {
                IEnumerable<HotelRoom> hotelRooms =  await _db.HotelRooms.Include(x=>x.HotelRoomImages).ToListAsync();
                IEnumerable<HotelRoomDTO> hotelRoomDTOs = _mapper.Map<IEnumerable<HotelRoomDTO>>(hotelRooms);
                return hotelRoomDTOs;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<HotelRoomDTO> GetById(int roomId)
        {
            try
            {
                HotelRoom hotelRoom = await _db.HotelRooms.Include(x=>x.HotelRoomImages).FirstOrDefaultAsync(x => x.Id == roomId);

                return _mapper.Map<HotelRoomDTO>(hotelRoom);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<HotelRoomDTO> Update(int roomId, HotelRoomDTO hotelRoomDTO)
        {
            try
            {
                if (roomId == hotelRoomDTO.Id)
                {
                    // valid
                    HotelRoom roomDetails = await _db.HotelRooms.FindAsync(roomId);
                    HotelRoom room = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO, roomDetails);
                    var updatedRoom = _db.HotelRooms.Update(room);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<HotelRoomDTO>(updatedRoom.Entity);
                }
                else
                {
                    // invalid
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        public async Task<int> Delete(int roomId)
        {
            var roomDetails = await _db.HotelRooms.FindAsync(roomId);
            if (roomDetails != null)
            {
                var allimages = await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync();

                _db.HotelRoomImages.RemoveRange(allimages);
                _db.HotelRooms.Remove(roomDetails);
                return await _db.SaveChangesAsync();
            }
            return 0;

        }

        public async Task<IEnumerable<HotelRoomDTO>> GetAll(string checkInDateStr, string checkOutDateStr)
        {
            try
            {
                IEnumerable<HotelRoom> hotelRooms = await _db.HotelRooms.Include(x => x.HotelRoomImages).ToListAsync();
                IEnumerable<HotelRoomDTO> hotelRoomDTOs = _mapper.Map<IEnumerable<HotelRoomDTO>>(hotelRooms);

                if (!string.IsNullOrEmpty(checkInDateStr) && !string.IsNullOrEmpty(checkOutDateStr))
                {
                    foreach (var hotelRoomDTO in hotelRoomDTOs)
                    {
                        hotelRoomDTO.IsBooked = await IsRoomBooked(hotelRoomDTO.Id, checkInDateStr, checkOutDateStr);
                    }
                   
                }

                return hotelRoomDTOs;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<HotelRoomDTO> GetByIdAndDates(int roomId, string checkInDateStr, string checkOutDateStr)
        {
            try
            {
                HotelRoom hotelRoom = await _db.HotelRooms.Include(x => x.HotelRoomImages).FirstOrDefaultAsync(x => x.Id == roomId);

                if ( !string.IsNullOrEmpty(checkInDateStr) && !string.IsNullOrEmpty(checkOutDateStr) )
                {
                    HotelRoomDTO hotelRoomDTO = _mapper.Map<HotelRoomDTO>(hotelRoom);
                    hotelRoomDTO.IsBooked = await IsRoomBooked(roomId, checkInDateStr, checkOutDateStr);
                    return hotelRoomDTO;
                }
                return _mapper.Map<HotelRoomDTO>(hotelRoom);
                
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
