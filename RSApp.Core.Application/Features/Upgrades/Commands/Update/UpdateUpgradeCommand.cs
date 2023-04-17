using AutoMapper;
using MediatR;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Repositories;

namespace RSApp.Core.Application.Features.Upgrades.Commands.Update;

public class UpdateUpgradeCommand : IRequest<UpdateUpgradeResponse> {
  public int Id { get; set; }
  public string Name { get; set; } = null!;
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
