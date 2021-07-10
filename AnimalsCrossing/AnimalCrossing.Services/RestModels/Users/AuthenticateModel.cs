using System.ComponentModel.DataAnnotations;

namespace AnimalCrossing.Services.RestModels.Users
{
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
