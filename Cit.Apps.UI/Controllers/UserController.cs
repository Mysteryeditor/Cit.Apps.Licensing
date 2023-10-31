using AutoMapper;
using Cit.Apps.Licensing.Application.Features.Users.Commands.CreateUserCommand;
using Cit.Apps.Licensing.Application.Features.Users.Commands.DeleteUserCommand;
using Cit.Apps.Licensing.Application.Features.Users.Commands.UpdateUserCommand;
using Cit.Apps.Licensing.Application.Features.Users.Queries.GetUserByIdQuery;
using Cit.Apps.Licensing.Application.Features.Users.Queries.GetUsers;
using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.Application.ReadModels;
using Cit.Apps.Licensing.Shared.Password;
using Cit.Apps.Licensing.UI.ViewModels;
using Cit.Apps.Shared.Result;
using Cit.Apps.UI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Cit.Apps.Licensing.UI.Controllers
{
    //[Route("")]
    public class UserController : Controller
    {
        private ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly IToastNotification _toastNotification;
        private readonly IService _service;

        public UserController(ILogger<HomeController> logger, IMediator mediator, IMapper mapper, IPasswordService passwordService,IToastNotification toastNotification,IService service)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _passwordService = passwordService;
            _toastNotification = toastNotification;
            _service = service;
        }
        public async Task<IActionResult> AllUsers()
        {
            var item = await _mediator.Send(new GetAllUsersQuery());
            return View(_mapper.Map<List<UserDetailsModel>>(item.Data));
        }



        public IActionResult CreateUser()
        {

            return View(new CreateUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ResultModel<string> result = await _mediator.Send(_mapper.Map<CreateUserCommand>(model));
                _toastNotification.AddSuccessToastMessage("User Created Successfully");
                return RedirectToAction("AllUsers");

            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateUser(int id)
        {
            var currentUser = await _mediator.Send(new GetUserByIdQuery(id));
            var item = _mapper.Map<UserDetailsModel>(currentUser.Data);

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserDetailsModel updatedData)
        {
            if (ModelState.IsValid)
            {
                var item = await _mediator.Send(new UpdateUserCommand()
                {
                    Id = updatedData.Id,
                    FirstName = updatedData.FirstName,
                    LastName = updatedData.LastName,
                    UserName = updatedData.UserName
                });

                if (item.Statuscode == 200)
                {
                    _toastNotification.AddSuccessToastMessage("Updated Successfully");
                    return RedirectToAction("AllUsers");
                }

                else
                {
                    //toastr
                    return RedirectToAction("AllUsers");

                }
            }
            return View(updatedData);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleteUser = await _mediator.Send(new GetUserByIdQuery(id));
            var item = _mapper.Map<UserDetailsModel>(deleteUser.Data);
            return View(item);
        }

        public async Task<IActionResult> ConfirmDeleteUser(int id)
        {
            var result = await _mediator.Send(new DeleteUserCommand() { Id = id });
            return RedirectToAction("AllUsers");
        }
    }
}
