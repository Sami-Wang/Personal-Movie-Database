using System.ComponentModel.DataAnnotations;

namespace Personal.Movie.Database.API.Models.ManageUser
{
    public class RegisterUserInput
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string userPasswordHash { get; set; }
        [Required]
        public int? userRoleID { get; set; }
        [Required]
        public string userFirstName { get; set; }
        [Required]
        public string userLastName { get; set; }
        [Required]
        public string userEmail { get; set; }
    }
}
