using AutoMapper;
using DataAccess.Data.Domain;
using Models.Dto;
using Models.Dto.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HotelRoomDTO, HotelRoom>().ReverseMap();
            CreateMap<HotelRoomImageDTO, HotelRoomImage>().ReverseMap();
            CreateMap<HotelAmenityDTO, HotelAmenity>().ReverseMap();

            CreateMap<RoomOrderDetails, RoomOrderDetailsDTO>()
                .ForMember(x=> x.HotelRoomDTO, opt => opt.MapFrom(c => c.HotelRoom));
            CreateMap<RoomOrderDetailsDTO, RoomOrderDetails>();
        }
    }
}
