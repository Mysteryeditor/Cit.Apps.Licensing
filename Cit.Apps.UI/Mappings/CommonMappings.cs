using Cit.Apps.Licensing.Application.ReadModels;
using Cit.Apps.Licensing.UI.ViewModels;
using AutoMapper;
using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Shared.Result;
using Cit.Apps.Licensing.Application.Features.Users.Commands.CreateUserCommand;
using Cit.Apps.Licensing.Application.Features.Users.Commands.UpdateUserCommand;

namespace Cit.Apps.Licensing.UI.Mappings
{
    public class CommonMappings : Profile
    {
        public CommonMappings()
        {
            CreateMap<User, UserReadModel>();
            CreateMap<UserReadModel, User>();
            CreateMap<UserReadModel, UserDetailsModel>();
            CreateMap<UserDetailsModel, UserReadModel>();

            //for creating
            CreateMap<CreateUserViewModel, CreateUserCommand>();

            //for updating

            CreateMap<UpdateUserCommand, User>();


        }
    }
}
