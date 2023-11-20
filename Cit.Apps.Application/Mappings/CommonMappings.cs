using AutoMapper;
using Cit.Apps.Licensing.Application.Features.Plans.Commands.CreatePlanCommand;
using Cit.Apps.Licensing.Application.Features.Users.Commands.UpdateUserCommand;
using Cit.Apps.Licensing.Application.ReadModels;
using Cit.Apps.Licensing.Domain.Entities;

namespace Cit.Apps.Licensing.UI.Mappings
{
    public class CommonMappings : Profile
    {
        public CommonMappings()
        {
            CreateMap<User, UserReadModel>();
            CreateMap<UserReadModel, User>();
            CreateMap<UpdateUserCommand, User>();
            CreateMap<ApplicationData,ApplicationReadModel>();
            CreateMap<Client, ClientReadModel>();
            //subscriptions
            CreateMap<ClientSubscription, ClientSubscriptionReadModel>();
            CreateMap<SubscriptionPlan, SubscriptionPlanReadModel>();

            //plans
            CreateMap<CreatePlanCommand, SubscriptionPlan>();

        }
    }
}
