using AutoMapper;
using MediatR;
using RSApp.Core.Application.Wrappers;
using RSApp.Core.Services.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace RSApp.Core.Application.Features.Upgrades.Commands.Delete;

/// <summary>
/// Parameters for deleting a sale
/// </summary>
public class DeleteUpgradeCommand : IRequest<Response<int>>
{
  /// <example> 1 </example>
  [SwaggerParameter(Description = "Id of sale")]
  public int Id { get; set; }
}

public class DeleteUpgradeCommandHandler : IRequestHandler<DeleteUpgradeCommand, Response<int>>
{
  private readonly IUpgradeRepository _upgradeRepository;
  private readonly IMapper _mapper;

  public DeleteUpgradeCommandHandler(IUpgradeRepository upgradeRepository, IMapper mapper)
  {
    _upgradeRepository = upgradeRepository;
    _mapper = mapper;
  }

  public async Task<Response<int>> Handle(DeleteUpgradeCommand request, CancellationToken cancellationToken)
  {
    var upgrade = await _upgradeRepository.GetEntity(request.Id) ?? throw new Exception("Upgrade not found");
    await _upgradeRepository.Delete(upgrade);
    return new Response<int>(upgrade.Id);
  }
}