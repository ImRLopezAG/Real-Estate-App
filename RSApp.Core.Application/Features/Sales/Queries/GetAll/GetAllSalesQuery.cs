using AutoMapper;
using MediatR;
using RSApp.Core.Services.Repositories;
using RSApp.Core.Services.ViewModels;

namespace RSApp.Core.Application.Features.Sales.Queries.GetAll;

public class GetAllSalesQuery : IRequest<IEnumerable<SaleVm>> {

}

public class GetAllSalesQueryHandler : IRequestHandler<GetAllSalesQuery, IEnumerable<SaleVm>> {
  private readonly ISaleRepository _saleRepository;
  private readonly IMapper _mapper;

  public GetAllSalesQueryHandler(ISaleRepository saleRepository, IMapper mapper) {
    _saleRepository = saleRepository;
    _mapper = mapper;
  }

  public async Task<IEnumerable<SaleVm>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken) {
    var sales = await _saleRepository.GetAll();
    return _mapper.Map<IEnumerable<SaleVm>>(sales);
  }
}