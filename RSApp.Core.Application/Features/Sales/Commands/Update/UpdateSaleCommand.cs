using AutoMapper;
using MediatR;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Repositories;

namespace RSApp.Core.Application.Features.Sales.Commands.Update;

public class UpdateSaleCommand : IRequest<UpdateSaleResponse> {
  public int Id { get; set; }
  public string Name { get; set; } = null!;
  public string? Description { get; set; } = null!;
}

public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResponse> {
  private readonly ISaleRepository _saleRepository;
  private readonly IMapper _mapper;

  public UpdateSaleCommandHandler(ISaleRepository saleRepository, IMapper mapper) {
    _saleRepository = saleRepository;
    _mapper = mapper;
  }

  public async Task<UpdateSaleResponse> Handle(UpdateSaleCommand request, CancellationToken cancellationToken) {
    var sale = await _saleRepository.GetEntity(request.Id) ?? throw new Exception("Sale not found");

    sale = _mapper.Map<Sale>(request);

    await _saleRepository.Update(sale);

    return _mapper.Map<UpdateSaleResponse>(sale);
  }
}