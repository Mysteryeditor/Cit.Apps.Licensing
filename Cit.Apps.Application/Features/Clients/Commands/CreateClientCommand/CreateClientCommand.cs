using AutoMapper;
using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.Domain.Common;
using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Licensing.Shared.Password;
using Cit.Apps.Shared.Result;
using MediatR;

namespace Cit.Apps.Licensing.Application.Features.Clients.Commands.CreateClientCommand
{
    public class CreateClientCommand:BaseEntity,IRequest<ResultModel<string>>
    {
        public string Name { get; set; }
        public long ContactNumber { get; set; }
        public string ContactPerson { get; set; }
        public string SubscriptionKey { get; set; }
    }

    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ResultModel<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateClientCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IPasswordService passwordService)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultModel<string>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {

            var client = new Client()
            {
                Name = request.Name,
                ContactNumber = request.ContactNumber,
                ContactPerson = request.ContactPerson,
                SubscriptionKey = request.SubscriptionKey,
                CreatedAt = DateTime.Now,
                CreatedBy=request.CreatedBy,
                ModifiedBy=request.ModifiedBy
            };
            try
            {
                await _unitOfWork.Repository<Client>().AddAsync(client);
                await _unitOfWork.Save(cancellationToken);
            }
            catch (Exception ex)
            {
                return await Result<string>.ReturnResult(500, ex.Message, null);
            }


            return await Result<string>.ReturnResult(200, "Created SuccessFully", null);
        }
    }
}
