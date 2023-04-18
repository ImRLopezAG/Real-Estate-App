using AutoMapper;
using MediatR;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace RSApp.Core.Application.Features.PropTypes.Commands.Update;

/// <summary>
/// Parameters for updating a sale
/// </summary>
public class UpdatePropTypeCommand : IRequest<UpdatePropTypeResponse> {
  /// <example> 1 </example>
  [SwaggerParameter(Description = "Id of sale")]
  public int Id { get; set; }
  /// <example> Rent </example>
  [SwaggerParameter(Description = "Name of sale")]
  public string Name { get; set; } = null!;
  /// <example> Example description </example>
  [SwaggerParameter(Description = "Description of sale")]
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