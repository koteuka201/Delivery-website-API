﻿using System.ComponentModel.DataAnnotations;

namespace DeliveryAppBack.Models.User
{
    public class LoginModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
