using System.ComponentModel.DataAnnotations;

namespace CarvillaApp.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [MaxLength(40)]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MaxLength(40)]
        [MinLength(3)]
        public string Surname { get; set; }
        [Required]
        [MaxLength(40)]
        [MinLength(3)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password) , Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
