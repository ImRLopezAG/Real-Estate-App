using MediatR;
using RSApp.Core.Services.Repositories;

namespace RSApp.Core.Application.Features.PropTypes.Commands.Delete;

public class DeletePropTypeCommand : IRequest<int> {
  public int Id { get; set; }
}

public class DeletePropTypeCommandHandler : IRequestHandler<DeletePropTypeCommand, int> {
  private readonly IPropTypeRepository _propTypeRepository;

  public DeletePropTypeCommandHandler(IPropTypeRepository propTypeRepository) {
    _propTypeRepository = propTypeRepository;
  }

  public async Task<int> Handle(DeletePropTypeCommand request, CancellationToken cancellationToken) {
    var propType = await _propTypeRepository.GetEntity(request.Id) ?? throw new Exception("PropType not found");
    await _propTypeRepository.Delete(propType);
    return propType.Id;
  }
}
