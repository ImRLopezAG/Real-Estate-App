using AutoMapper;
using MediatR;
using RSApp.Core.Services.Repositories;
using RSApp.Core.Services.ViewModels;

namespace RSApp.Core.Application.Features.Sales.Queries.GetAll;

public class GetAllSaleQuery : IRequest<IEnumerable<SaleVm>> {

}

public class GetAllSaleQueryHandler : IRequestHandler<GetAllSaleQuery, IEnumerable<SaleVm>> {
  private readonly ISaleRepository _saleRepository;
  private readonly IMapper _mapper;

  public GetAllSaleQueryHandler(ISaleRepository saleRepository, IMapper mapper) {
    _saleRepository = saleRepository;
    _mapper = mapper;
  }

  public async Task<IEnumerable<SaleVm>> Handle(GetAllSaleQuery request, CancellationToken cancellationToken) {
    var sales = await _saleRepository.GetAll();
    return _mapper.Map<IEnumerable<SaleVm>>(sales);
  }
}