using System.ComponentModel.DataAnnotations;

namespace Concierto.Common.Requests
{
    public class UserRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string PasswordConfirm { get; set; }
    }
}
