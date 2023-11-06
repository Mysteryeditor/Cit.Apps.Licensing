using AutoMapper;
using Cit.Apps.Licensing.Application.Features.Users.Commands.CreateUserCommand;
using Cit.Apps.Licensing.Application.ReadModels;
using Cit.Apps.Licensing.UI.ViewModels;

namespace Cit.Apps.Licensing.UI.Mappings
{
    public class CommonMappings : Profile
    {
        public CommonMappings()
        {
            //user mappings
            CreateMap<UserReadModel, UserDetailsModel>();
            CreateMap<UserDetailsModel, UserReadModel>();
            CreateMap<CreateUserViewModel, CreateUserCommand>();

            //Application mapping
            CreateMap<ApplicationReadModel,ApplicationViewModel>();

            //Client mapping
            CreateMap<ClientReadModel, ClientViewModel>();
            CreateMap<ClientViewModel, ClientReadModel>();
            CreateMap<ClientReadModel, CreateClientViewModel>();
        }
    }
}
