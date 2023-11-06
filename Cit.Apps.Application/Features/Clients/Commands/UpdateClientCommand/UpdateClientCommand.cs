using AutoMapper;
using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.Domain.Common;
using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Shared.Result;
using MediatR;

namespace Cit.Apps.Licensing.Application.Features.Clients.Commands.UpdateClientCommand
{
    public class UpdateClientCommand : BaseEntity, IRequest<ResultModel<string>>
    {
        public string Name { get; set; }

        public long ContactNumber { get; set; }
        public string ContactPerson { get; set; }

        //subkey
    }

    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, ResultModel<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateClientCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultModel<string>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var existingdata = await _unitOfWork.Repository<Client>().GetByIdAsync(request.Id);
                if (existingdata is not null)
                {
                    existingdata.Name = request.Name;
                    existingdata.ContactNumber = request.ContactNumber;
                    existingdata.ContactPerson = request.ContactPerson;
                    await _unitOfWork.Repository<Client>().UpdateAsync(existingdata);
                    await _unitOfWork.Save(cancellationToken);

                    return await Result<string>.ReturnResult(200, "Updated successfully", null);
                }
                else {
                    return await Result<string>.ReturnResult(404, "Record Doesnot exist", null);


                }



            }
            catch (Exception ex)
            {
                return await Result<string>.ReturnResult(500, ex.Message, null);
            }
        }
    }


}
