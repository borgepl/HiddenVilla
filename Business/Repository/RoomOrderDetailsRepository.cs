using AutoMapper;
using Business.Contracts;
using Common;
using DataAccess.Data;
using DataAccess.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Models.Dto.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class RoomOrderDetailsRepository : IRoomOrderDetailsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public RoomOrderDetailsRepository(ApplicationDbContext context, IMapper mappe)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<RoomOrderDetailsDTO> Create(RoomOrderDetailsDTO details)
        {
            try
            {
                details.CheckInDate = details.CheckInDate.Date;
                details.CheckOutDate = details.CheckOutDate.Date;
                
                var roomOrder = _mapper.Map<RoomOrderDetailsDTO,RoomOrderDetails>(details);
                roomOrder.Status = SD.Status_Pending;

                var addedRoomOrder = await _context.RoomOrderDetails.AddAsync(roomOrder);
                await _context.SaveChangesAsync();

                return _mapper.Map<RoomOrderDetails, RoomOrderDetailsDTO>(addedRoomOrder.Entity);

            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<IEnumerable<RoomOrderDetailsDTO>> GetAllRoomOrderDetail()
        {
            try
            {
                IEnumerable<RoomOrderDetails> roomOrders = await _context.RoomOrderDetails.Include(u=>u.HotelRoom).ToListAsync();
                return _mapper.Map<IEnumerable<RoomOrderDetailsDTO>>(roomOrders)
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<RoomOrderDetailsDTO> GetRoomOrderDetail(int roomOrderId)
        {
            try
            {
                RoomOrderDetails roomOrder = await _context.RoomOrderDetails
                                                .Include(u=>u.HotelRoom).ThenInclude(x=>x.HotelRoomImages)
                                                .FirstOrDefaultAsync(x=>x.Id == roomOrderId);
                RoomOrderDetailsDTO roomOrderDetailsDTO = _mapper.Map<RoomOrderDetailsDTO>(roomOrder);

                roomOrderDetailsDTO.HotelRoomDTO.TotalDays 
                    = roomOrderDetailsDTO.CheckOutDate.Subtract(roomOrderDetailsDTO.CheckInDate).Days ;

                return roomOrderDetailsDTO;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public Task<bool> IsRoomBooked(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            throw new NotImplementedException();
        }

        public Task<RoomOrderDetailsDTO> MarkPaymentSuccessful(int roomOrderId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOrderStatus(int roomOrderId, string status)
        {
            throw new NotImplementedException();
        }
    }
}
