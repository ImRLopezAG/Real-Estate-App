using AutoMapper;
using MediatR;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace RSApp.Core.Application.Features.Sales.Commands.Update;

/// <example>
/// Parameters for updating a sale
/// </example>
public class UpdateSaleCommand : IRequest<UpdateSaleResponse> {

  /// <example> 1 </example>
  [SwaggerParameter(Description = "new Id of sale")]
  public int Id { get; set; }
  /// <example> Rent </example>
  [SwaggerParameter(Description = "new Name of sale")]
  public string Name { get; set; } = null!;
  /// <example> Example description </example>
  [SwaggerParameter(Description = "new Description of sale")]
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