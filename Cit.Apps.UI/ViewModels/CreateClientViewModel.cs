using System.ComponentModel.DataAnnotations;

namespace Cit.Apps.Licensing.UI.ViewModels
{
    public class CreateClientViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Contact Number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Contact Number must be a 10-digit number")]
        public long ContactNumber { get; set; }

        [Required(ErrorMessage = "Contact Person is required")]
        public string ContactPerson { get; set; }
        [Required(ErrorMessage = "Subscription Key is required")]
        //public string SubscriptionKey { get; set; }

        public int CreatedBy { get; set; }

        public int ModifiedBy { get; set; }
    }
}
