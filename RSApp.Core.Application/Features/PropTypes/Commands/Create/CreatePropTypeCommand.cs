using AutoMapper;
using MediatR;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Repositories;

namespace RSApp.Core.Application.Features.PropTypes.Commands.Create;

public class CreatePropTypeCommand : IRequest<int> {
  public string Name { get; set; } = null!;
  public string Description { get; set; } = null!;
}

public class CreatePropTypeCommandHandler : IRequestHandler<CreatePropTypeCommand, int> {
  private readonly IPropTypeRepository _propTypeRepository;
  private readonly IMapper _mapper;

  public CreatePropTypeCommandHandler(IPropTypeRepository propTypeRepository, IMapper mapper) {
    _propTypeRepository = propTypeRepository;
    _mapper = mapper;
  }

  public async Task<int> Handle(CreatePropTypeCommand request, CancellationToken cancellationToken) {
    var propType = _mapper.Map<PropType>(request);
    await _propTypeRepository.Save(propType);
    return propType.Id;
  }
}