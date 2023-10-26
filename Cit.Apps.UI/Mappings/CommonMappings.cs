using Cit.Apps.Licensing.Application.ReadModels;
using Cit.Apps.Licensing.UI.ViewModels;
using AutoMapper;
using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Shared.Result;

namespace Cit.Apps.Licensing.UI.Mappings
{
    public class CommonMappings : Profile
    {
        public CommonMappings()
        {
            CreateMap<User, UserReadModel>();
            CreateMap<UserReadModel, UserDetailsModel>();


        }
    }
}
