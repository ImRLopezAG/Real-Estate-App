using AutoMapper;
using MediatR;
using RSApp.Core.Application.Wrappers;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace RSApp.Core.Application.Features.Upgrades.Commands.Create;

/// <summary>
/// Parameters for creating a sale
/// </summary>
public class CreateUpgradeCommand : IRequest<Response<int>>
{
  /// <example> Rent </example>
  [SwaggerParameter(Description = "Name of sale")]
  public string Name { get; set; } = null!;
  /// <example> Example description </example>
  [SwaggerParameter(Description = "Description of sale")]
  public string? Description { get; set; } = null!;
}

public class CreateUpgradeCommandHandler : IRequestHandler<CreateUpgradeCommand, Response<int>>
{
  private readonly IUpgradeRepository _upgradeRepository;
  private readonly IMapper _mapper;

  public CreateUpgradeCommandHandler(IUpgradeRepository upgradeRepository, IMapper mapper)
  {
    _upgradeRepository = upgradeRepository;
    _mapper = mapper;
  }

  public async Task<Response<int>> Handle(CreateUpgradeCommand request, CancellationToken cancellationToken)
  {
    var upgrade = _mapper.Map<Upgrade>(request);
    upgrade = await _upgradeRepository.Save(upgrade);
    return new Response<int>(upgrade.Id);
  }
}