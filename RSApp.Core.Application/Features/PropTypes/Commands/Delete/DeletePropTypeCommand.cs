using MediatR;
using RSApp.Core.Application.Wrappers;
using RSApp.Core.Services.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace RSApp.Core.Application.Features.PropTypes.Commands.Delete;

/// <summary>
/// Parameters for deleting a sale
/// </summary>
public class DeletePropTypeCommand : IRequest<Response<int>>
{
  /// <example> 1 </example>
  [SwaggerParameter(Description = "Id of sale")]
  public int Id { get; set; }
}

public class DeletePropTypeCommandHandler : IRequestHandler<DeletePropTypeCommand, Response<int>>
{
  private readonly IPropTypeRepository _propTypeRepository;

  public DeletePropTypeCommandHandler(IPropTypeRepository propTypeRepository)
  {
    _propTypeRepository = propTypeRepository;
  }

  public async Task<Response<int>> Handle(DeletePropTypeCommand request, CancellationToken cancellationToken)
  {
    var propType = await _propTypeRepository.GetEntity(request.Id) ?? throw new Exception("PropType not found");
    await _propTypeRepository.Delete(propType);
    return new Response<int>(propType.Id);
  }
}
