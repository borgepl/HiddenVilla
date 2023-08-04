using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Domain
{
    public class HotelRoom : BaseEntity
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public int Occupancy { get; set; }

        public double RegularRate { get; set; }
        public string? Details { get; set; }
        public string? SqFt { get; set; }

        public virtual ICollection<HotelRoomImage>? HotelRoomImages { get; set; }

       
    }
}
