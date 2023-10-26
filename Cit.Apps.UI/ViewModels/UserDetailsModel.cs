using Cit.Apps.Licensing.Application.ReadModels;

namespace Cit.Apps.Licensing.UI.ViewModels
{
    public class UserDetailsModel
    {
        public string UserName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }

    }
}
