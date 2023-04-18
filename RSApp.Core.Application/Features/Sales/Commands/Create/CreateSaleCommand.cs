using AutoMapper;
using MediatR;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace RSApp.Core.Application.Features.Sales.Commands.Create;


public class CreateSaleCommand : IRequest<int> {
    /// <example>
    /// Alquiler
    /// </example>
    [SwaggerParameter(Description = "Name of sale")]
    public string Name { get; set; } = null!;

    /// <example>
    /// Tipo de venta para propiedades
    /// </example>
    [SwaggerParameter(Description = "Description of sale")]
    public string Description { get; set; } = null!;
}

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, int> {
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public CreateSaleCommandHandler(ISaleRepository saleRepository, IMapper mapper) {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateSaleCommand request, CancellationToken cancellationToken) {
        var sale = _mapper.Map<Sale>(request);
        await _saleRepository.Save(sale);
        return sale.Id;
    }
}
