﻿using System.ComponentModel.DataAnnotations;

namespace AsyncInn.Models.DTOs
{
    public class LoginData
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
