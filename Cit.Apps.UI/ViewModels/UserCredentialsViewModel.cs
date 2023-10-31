using System.ComponentModel.DataAnnotations;

namespace Cit.Apps.Licensing.UI.ViewModels
{
    public class UserCredentialsViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
