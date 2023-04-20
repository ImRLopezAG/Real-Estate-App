using AutoMapper;
using MediatR;
using RSApp.Core.Services.Contracts;
using RSApp.Core.Services.Dtos.Account;

namespace RSApp.Core.Application.Features.Agents.Queries.GetById {
    public class GetByIdAgentQuery : IRequest<AccountDto> {
        public string Id { get; set; } = null;
    }

    public class GetByIdAgentQueryHandler : IRequestHandler<GetByIdAgentQuery, AccountDto> {
        private readonly IUserService _account;
        private readonly IMapper _mapper;

        public GetByIdAgentQueryHandler(IUserService account, IMapper mapper) {
            _account = account;
            _mapper = mapper;
        }

        public async Task<AccountDto> Handle(GetByIdAgentQuery request, CancellationToken cancellationToken) {
            var agent = await _account.GetById(request.Id);
            return _mapper.Map<AccountDto>(agent);
        }
    }
}
