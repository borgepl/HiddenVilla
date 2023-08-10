﻿using System.ComponentModel.DataAnnotations;

namespace HiddenVilla_Client.ViewModels
{
    public class HotelAmenityVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter amenity name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter amenity timming")]
        public string Timming { get; set; }
        [Required(ErrorMessage = "Please enter amenity description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter amenity icon from font awesome")]
        public string IconStyle { get; set; }
    }
}
