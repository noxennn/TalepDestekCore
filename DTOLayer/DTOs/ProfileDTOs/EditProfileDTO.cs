﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.ProfileDTOs
{
    public class EditProfileDTO
    {
        public int Id {  get; set; }   
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }

        public string? ImageURL { get; set; }
        public string Gender { get; set; }
    }
}
