using Cit.Apps.Licensing.Application.ReadModels;
using System.ComponentModel.DataAnnotations;

namespace Cit.Apps.Licensing.UI.ViewModels
{
    public class UserDetailsModel
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }

    }
}
