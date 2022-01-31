using System.ComponentModel.DataAnnotations;

namespace Imi.Project.Common.Dtos.Users
{
    public class UserRequestDto : BaseDto
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(100, MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(100, MinimumLength = 3)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(100, MinimumLength = 3)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid e-mail address")]
        public string EmailAddress { get; set; }
    }
}
