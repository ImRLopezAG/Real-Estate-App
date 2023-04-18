using AutoMapper;
using MediatR;
using RSApp.Core.Application.Wrappers;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace RSApp.Core.Application.Features.Sales.Commands.Create;

/// <summary>
/// Parameters for creating a sale
/// </summary>
public class CreateSaleCommand : IRequest<Response<int>>
{
  /// <example> Rent </example>
  [SwaggerParameter(Description = "Name of sale")]
  public string Name { get; set; } = null!;
  /// <example> Example description </example>
  [SwaggerParameter(Description = "Description of sale")]
  public string Description { get; set; } = null!;
}

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Response<int>>
{
  private readonly ISaleRepository _saleRepository;
  private readonly IMapper _mapper;

  public CreateSaleCommandHandler(ISaleRepository saleRepository, IMapper mapper)
  {
    _saleRepository = saleRepository;
    _mapper = mapper;
  }

  public async Task<Response<int>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
  {
    var sale = _mapper.Map<Sale>(request);
    await _saleRepository.Save(sale);
    return new Response<int>(sale.Id);
  }
}
