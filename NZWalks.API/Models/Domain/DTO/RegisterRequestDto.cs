using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.Domain.DTO
{
    public class RegisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public List<string> Roles { get; set; }

    }
}
