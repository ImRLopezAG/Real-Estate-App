using MediatR;
using RSApp.Core.Services.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace RSApp.Core.Application.Features.Sales.Commands.Delete;


public class DeleteSaleCommand : IRequest<int> {
    [SwaggerParameter(Description = "Id of sale")]
    public int Id { get; set; }
}

public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, int> {
    private readonly ISaleRepository _saleRepository;

    public DeleteSaleCommandHandler(ISaleRepository saleRepository) {
        _saleRepository = saleRepository;
    }

    public async Task<int> Handle(DeleteSaleCommand request, CancellationToken cancellationToken) {
        var sale = await _saleRepository.GetEntity(request.Id) ?? throw new Exception("Sale not found");
        await _saleRepository.Delete(sale);
        return sale.Id;
    }
}
