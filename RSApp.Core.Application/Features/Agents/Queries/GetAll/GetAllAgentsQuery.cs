using AutoMapper;
using MediatR;
using RSApp.Core.Services.Contracts;
using RSApp.Core.Services.Dtos.Account;

namespace RSApp.Core.Application.Features.Agents.Queries.GetAll {
    public class GetAllAgentsQuery : IRequest<IEnumerable<AccountDto>> {

    }

    public class GetAllAgentsQueryHandler : IRequestHandler<GetAllAgentsQuery, IEnumerable<AccountDto>> {
        private readonly IUserService _account;
        private readonly IMapper _mapper;
        public GetAllAgentsQueryHandler(IUserService account, IMapper mapper) {
            _account = account;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AccountDto>> Handle(GetAllAgentsQuery request, CancellationToken cancellationToken) {
            var agents = await _account.GetAll().ContinueWith(r => r.Result.Where(i => i.Role == "Agent"));
            return _mapper.Map<IEnumerable<AccountDto>>(agents);
        }
    }
}