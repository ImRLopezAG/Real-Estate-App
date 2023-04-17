using AutoMapper;
using MediatR;
using RSApp.Core.Services.Repositories;
namespace RSApp.Core.Application.Features.Upgrades.Commands.Delete;

public class DeleteUpgradeCommand : IRequest<int> {
  public int Id { get; set; }
}

public class DeleteUpgradeCommandHandler : IRequestHandler<DeleteUpgradeCommand, int> {
  private readonly IUpgradeRepository _upgradeRepository;
  private readonly IMapper _mapper;

  public DeleteUpgradeCommandHandler(IUpgradeRepository upgradeRepository, IMapper mapper) {
    _upgradeRepository = upgradeRepository;
    _mapper = mapper;
  }

  public async Task<int> Handle(DeleteUpgradeCommand request, CancellationToken cancellationToken) {
    var upgrade = await _upgradeRepository.GetEntity(request.Id) ?? throw new Exception("Upgrade not found");
    await _upgradeRepository.Delete(upgrade);
    return upgrade.Id;
  }
}