
using BlazorWebAssemblyIdentityDemo.Domain.Repositories.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Application.Pipelines.Queries.User.Queries.GetUserById
{
    public record GetUserByIdQuery(int id) : IRequest<BlazorWebAssemblyIdentityDemo.Domain.Entities.User>
    {
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, BlazorWebAssemblyIdentityDemo.Domain.Entities.User>
    {
        private readonly IUserQueryRepository _userQueryRepository;

        public GetUserByIdQueryHandler(IUserQueryRepository userQueryRepository)
        {
            this._userQueryRepository = userQueryRepository;
        }

        public async Task<Domain.Entities.User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userQueryRepository.GetById(request.id, cancellationToken);

            return user;
        }
    }
}
