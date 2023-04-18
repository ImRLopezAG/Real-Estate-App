using AutoMapper;
using MediatR;
using RSApp.Core.Application.Wrappers;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace RSApp.Core.Application.Features.PropTypes.Commands.Create;

/// <summary>
/// Parameters for creating a sale
/// </summary>
public class CreatePropTypeCommand : IRequest<Response<int>>
{
  /// <example> Rent </example>
  [SwaggerParameter(Description = "Name of sale")]
  public string Name { get; set; } = null!;
  /// <example> Example description </example>
  [SwaggerParameter(Description = "Description of sale")]
  public string Description { get; set; } = null!;
}

public class CreatePropTypeCommandHandler : IRequestHandler<CreatePropTypeCommand, Response<int>>
{
  private readonly IPropTypeRepository _propTypeRepository;
  private readonly IMapper _mapper;

  public CreatePropTypeCommandHandler(IPropTypeRepository propTypeRepository, IMapper mapper)
  {
    _propTypeRepository = propTypeRepository;
    _mapper = mapper;
  }

  public async Task<Response<int>> Handle(CreatePropTypeCommand request, CancellationToken cancellationToken)
  {
    var propType = _mapper.Map<PropType>(request);
    await _propTypeRepository.Save(propType);
    return new Response<int>(propType.Id);
  }
}