using AutoMapper;
using MediatR;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Repositories;

namespace RSApp.Core.Application.Features.Upgrades.Queries.GetBbyId;

public class GetByIdUpgradeQuery : IRequest<Upgrade> {
  public int Id { get; set; }
}

public class GetByIdUpgradeQueryHandler : IRequestHandler<GetByIdUpgradeQuery, Upgrade> {
  private readonly IUpgradeRepository _upgradeRepository;
  private readonly IMapper _mapper;

  public GetByIdUpgradeQueryHandler(IUpgradeRepository upgradeRepository, IMapper mapper) {
    _upgradeRepository = upgradeRepository;
    _mapper = mapper;
  }

  public async Task<Upgrade> Handle(GetByIdUpgradeQuery request, CancellationToken cancellationToken) {
    var upgrade = await _upgradeRepository.GetEntity(request.Id) ?? throw new Exception("Upgrade not found");
    return upgrade;
  }
}