using Cit.Apps.Licensing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cit.Apps.Licensing.Application.ReadModels
{
    public class SubscriptionPlanReadModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BillingCycleType { get; set; }
        public decimal Cost { get; set; }
        public int ApplicationId { get; set; }
        public string ApplicationName { get; set; }
    }
}
