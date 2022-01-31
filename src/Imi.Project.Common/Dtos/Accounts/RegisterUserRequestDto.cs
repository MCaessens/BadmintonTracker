using System;
using System.ComponentModel.DataAnnotations;

namespace Imi.Project.Common.Dtos.Accounts
{
    public class RegisterUserRequestDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string UserName { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string LastName { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }

}
