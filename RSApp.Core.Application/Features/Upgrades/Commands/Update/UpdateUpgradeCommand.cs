using AutoMapper;
using MediatR;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace RSApp.Core.Application.Features.Upgrades.Commands.Update;

/// <summary>
/// Parameters for updating a sale
/// </summary>

public class UpdateUpgradeCommand : IRequest<UpdateUpgradeResponse> {
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

public class UpdateUpgradeCommandHandler : IRequestHandler<UpdateUpgradeCommand, UpdateUpgradeResponse> {
  private readonly IUpgradeRepository _upgradeRepository;
  private readonly IMapper _mapper;

  public UpdateUpgradeCommandHandler(IUpgradeRepository upgradeRepository, IMapper mapper) {
    _upgradeRepository = upgradeRepository;
    _mapper = mapper;
  }

  public async Task<UpdateUpgradeResponse> Handle(UpdateUpgradeCommand request, CancellationToken cancellationToken) {
    var upgrade = await _upgradeRepository.GetEntity(request.Id) ?? throw new Exception("Upgrade not found");

    upgrade = _mapper.Map<Upgrade>(request);

    await _upgradeRepository.Update(upgrade);

    return _mapper.Map<UpdateUpgradeResponse>(upgrade);
  }
}
