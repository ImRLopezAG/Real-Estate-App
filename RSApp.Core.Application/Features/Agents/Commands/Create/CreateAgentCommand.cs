using MediatR;
using RSApp.Core.Application.Wrappers;

namespace RSApp.Core.Application.Features.Agents.Commands.Create {
    public class CreateAgentCommand : IRequest<Response<int>> {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string DNI { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public int Role { get; set; } = 3;
        public string? Image { get; set; } = null!;
    }

    //public class CreateAgentCommandHandler : IRequestHandler<CreateAgentCommand, Response<int>> {
    //    private readonly IAccountService _account;
    //    private readonly IMapper _mapper;

    //    public CreateAgentCommandHandler(IAccountService account, IMapper mapper) {
    //        _account = account;
    //        _mapper = mapper;
    //    }

    //    public async Task<Response<int>> Handle(CreateAgentCommand request, CancellationToken cancellationToken) {
    //        var propType = _mapper.Map<PropType>(request);
    //        var origin = Request.Headers["origin"];
    //        await _account.RegisterUserAsync(propType, origin);
    //        return new Response<string>(propType.Id);
    //    }

    //}
}
