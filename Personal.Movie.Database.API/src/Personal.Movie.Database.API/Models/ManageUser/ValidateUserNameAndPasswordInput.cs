using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Personal.Movie.Database.API.Models.ManageUser
{
    public class ValidateUserNameAndPasswordInput
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string userPasswordHash { get; set; }
    }
}
