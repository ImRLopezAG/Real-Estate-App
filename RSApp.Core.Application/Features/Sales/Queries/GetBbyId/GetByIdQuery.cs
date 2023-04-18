using AutoMapper;
using MediatR;
using RSApp.Core.Services.Repositories;
using RSApp.Core.Services.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace RSApp.Core.Application.Features.Sales.Queries.GetBbyId;

public class GetByIdQuery : IRequest<SaleVm> {
  [SwaggerParameter(Description = "Colocar el ID de la venta")]
  public int Id { get; set; }
}

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, SaleVm> {
  private readonly ISaleRepository _saleRepository;
  private readonly IMapper _mapper;

  public GetByIdQueryHandler(ISaleRepository saleRepository, IMapper mapper) {
    _saleRepository = saleRepository;
    _mapper = mapper;
  }

  public async Task<SaleVm> Handle(GetByIdQuery request, CancellationToken cancellationToken) {
    var sale = await _saleRepository.GetEntity(request.Id) ?? throw new Exception("Sale not found");
    return _mapper.Map<SaleVm>(sale);
  }
}