using System.ComponentModel.DataAnnotations;

namespace Imi.Project.Common.Dtos.Accounts
{
    public class LoginUserRequestDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string UserNameOrEmail { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Password { get; set; }
    }
}