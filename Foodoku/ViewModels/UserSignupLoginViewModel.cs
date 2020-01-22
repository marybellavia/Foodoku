using System;
using Foodoku.ViewModels;

namespace Foodoku.ViewModels
{
    public class UserSignupLoginViewModel
    {
        public AddUserViewModel AddUserVM { get; set; }
        public LoginUserViewModel LoginUserVM { get; set; }

        public UserSignupLoginViewModel()
        {
        }
    }
}
