using Cit.Apps.Licensing.Domain.Common;

namespace Cit.Apps.Licensing.Domain.Entities
{
    public class ApplicationData : BaseEntity
    {
      
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
