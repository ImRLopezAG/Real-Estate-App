using AutoMapper;
using RSApp.Core.Domain.Entities;
using RSApp.Core.Services.Core;
using RSApp.Core.Services.Repositories;
using RSApp.Core.Services.Services;
using RSApp.Core.Services.ViewModels;
using RSApp.Core.Services.ViewModels.SaveVm;

namespace RSApp.Core.Application.Services;

public class SaleService : GenericService<SaleVm, SaveSaleVm, Sale>, ISaleService {
  private readonly ISaleRepository _saleRepository;
  private readonly IMapper _mapper;

  public SaleService(ISaleRepository saleRepository, IMapper mapper) : base(saleRepository, mapper) {
    _saleRepository = saleRepository;
    _mapper = mapper;
  }
}