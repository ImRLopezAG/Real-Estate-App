using AutoMapper;
using MediatR;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Repositories;

namespace RSApp.Core.Application.Features.Upgrades.Commands.Create;

public class CreateUpgradeCommand : IRequest<int> {
  public string Name { get; set; } = null!;
  public string? Description { get; set; } = null!;
}

public class CreateUpgradeCommandHandler : IRequestHandler<CreateUpgradeCommand, int> {
  private readonly IUpgradeRepository _upgradeRepository;
  private readonly IMapper _mapper;

  public CreateUpgradeCommandHandler(IUpgradeRepository upgradeRepository, IMapper mapper) {
    _upgradeRepository = upgradeRepository;
    _mapper = mapper;
  }

  public async Task<int> Handle(CreateUpgradeCommand request, CancellationToken cancellationToken) {
    var upgrade = _mapper.Map<Upgrade>(request);
    upgrade = await _upgradeRepository.Save(upgrade);
    return upgrade.Id;
  }
}