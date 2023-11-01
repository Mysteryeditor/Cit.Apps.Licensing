using Cit.Apps.Shared.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cit.Apps.Licensing.Application.Features.Applications.Queries.GetApplicationQuery
{
    public class GetApplicationQuery:IRequest<ResultModel<>>
    {
    }
}
