using AutoMapper;
using MediatR;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Repositories;
namespace RSApp.Core.Application.Features.PropTypes.Commands.Update;

public class UpdatePropTypeCommand : IRequest<UpdatePropTypeResponse> {
  public int Id { get; set; }
  public string Name { get; set; } = null!;
  public string? Description { get; set; } = null!;
}


public class UpdatePropTypeCommandHandler : IRequestHandler<UpdatePropTypeCommand, UpdatePropTypeResponse> {
  private readonly IPropTypeRepository _propTypeRepository;
  private readonly IMapper _mapper;

  public UpdatePropTypeCommandHandler(IPropTypeRepository propTypeRepository, IMapper mapper) {
    _propTypeRepository = propTypeRepository;
    _mapper = mapper;
  }

  public async Task<UpdatePropTypeResponse> Handle(UpdatePropTypeCommand request, CancellationToken cancellationToken) {
    var propType = await _propTypeRepository.GetEntity(request.Id) ?? throw new Exception("PropType not found");
    propType = _mapper.Map<PropType>(request);
    await _propTypeRepository.Update(propType);
    return _mapper.Map<UpdatePropTypeResponse>(propType);
  }
}