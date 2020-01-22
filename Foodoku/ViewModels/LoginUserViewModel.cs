﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Foodoku.ViewModels
{
    public class LoginUserViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }

        public LoginUserViewModel()
        {
        }
    }
}
