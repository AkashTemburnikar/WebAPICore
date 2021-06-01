using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Models
{
    public class RegitserUser
    {
        private const string V = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";

        [Required(ErrorMessage = "Email is Must")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Must")]
        [RegularExpression(V,
            ErrorMessage = "Minimum eight characters, at least one letter, one number and one special character")]
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginUser
    {
        [Required(ErrorMessage = "User Name is Must")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is Must")]
        public string Password { get; set; }

    }

    public class ResponseData
    {
        public string responseMessage { get; set; }
    }
}
