using MediatR;
using RSApp.Core.Application.Wrappers;
using RSApp.Core.Services.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace RSApp.Core.Application.Features.Sales.Commands.Delete;


/// <example>
/// Parameters for deleting a sale
/// </example>
public class DeleteSaleCommand : IRequest<Response<int>>
{
  /// <example> 1 </example>
  [SwaggerParameter(Description = "Id of sale")]
  public int Id { get; set; }
}

public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, Response<int>>
{
  private readonly ISaleRepository _saleRepository;

  public DeleteSaleCommandHandler(ISaleRepository saleRepository)
  {
    _saleRepository = saleRepository;
  }

  public async Task<Response<int>> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
  {
    var sale = await _saleRepository.GetEntity(request.Id) ?? throw new Exception("Sale not found");
    await _saleRepository.Delete(sale);
    return new Response<int>(sale.Id);
  }
}
