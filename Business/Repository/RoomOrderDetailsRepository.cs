using AutoMapper;
using Business.Contracts;
using Common;
using DataAccess.Data;
using DataAccess.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
        public RoomOrderDetailsRepository(ApplicationDbContext context, IMapper mapper)
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
                return _mapper.Map<IEnumerable<RoomOrderDetailsDTO>>(roomOrders);
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
                    = roomOrderDetailsDTO.CheckOutDate.Subtract(roomOrderDetailsDTO.CheckInDate).Days;



                return roomOrderDetailsDTO;
            }
            catch (Exception)
            {

                return null;
            }
        }


        public async Task<RoomOrderDetailsDTO> MarkPaymentSuccessful(int roomOrderId)
        {
            var dataFromDb = await _context.RoomOrderDetails.Where(x=> x.Id == roomOrderId).FirstOrDefaultAsync();
            if (dataFromDb == null)
            {
                return null;
            }
            if (!dataFromDb.IsPaymentSuccessful)
            {
                dataFromDb.IsPaymentSuccessful = true;
                dataFromDb.Status = SD.Status_Booked;
                var paymentSuccessful = _context.RoomOrderDetails.Update(dataFromDb);
                await _context.SaveChangesAsync();
                return _mapper.Map<RoomOrderDetailsDTO>(paymentSuccessful.Entity);
            }
            return new RoomOrderDetailsDTO();
        }

        public async Task<bool> UpdateOrderStatus(int roomOrderId, string status)
        {
            try
            {
                var roomOrder = await _context.RoomOrderDetails.FirstOrDefaultAsync(u => u.Id == roomOrderId);
                if (roomOrder == null)
                {
                    return false;
                }
                roomOrder.Status = status;
                if (status == SD.Status_CheckedIn)
                {
                    roomOrder.ActualCheckInDate = DateTime.Now;
                }
                if (status == SD.Status_CheckedOut_Completed)
                {
                    roomOrder.ActualCheckOutDate = DateTime.Now;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
