using AutoMapper;
using MediatR;
using RSApp.Core.Services.Repositories;
using RSApp.Core.Services.ViewModels;

namespace RSApp.Core.Application.Features.Upgrades.Queries.GetAll;

public class GetAllUpgradeQuery : IRequest<IEnumerable<UpgradeVm>> {

}

public class GetAllUpgradeQueryHandler : IRequestHandler<GetAllUpgradeQuery, IEnumerable<UpgradeVm>> {
  private readonly IUpgradeRepository _upgradeRepository;
  private readonly IMapper _mapper;

  public GetAllUpgradeQueryHandler(IUpgradeRepository upgradeRepository, IMapper mapper) {
    _upgradeRepository = upgradeRepository;
    _mapper = mapper;
  }

  public async Task<IEnumerable<UpgradeVm>> Handle(GetAllUpgradeQuery request, CancellationToken cancellationToken) {
    var upgrades = await _upgradeRepository.GetAll();
    return _mapper.Map<IEnumerable<UpgradeVm>>(upgrades);
  }
}