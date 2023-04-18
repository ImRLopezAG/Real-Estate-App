using AutoMapper;
using MediatR;
using RSApp.Core.Services.Repositories;
using RSApp.Core.Services.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace RSApp.Core.Application.Features.PropTypes.Queries.GetBbyId;

public class GetByIdPropTypeQuery : IRequest<PropTypeVm> {

  [SwaggerParameter(Description = "Id of sale")]
  public int Id { get; set; }
}

public class GetByIdPropTypeQueryHandler : IRequestHandler<GetByIdPropTypeQuery, PropTypeVm> {
  private readonly IPropTypeRepository _propTypeRepository;
  private readonly IMapper _mapper;

  public GetByIdPropTypeQueryHandler(IPropTypeRepository propTypeRepository, IMapper mapper) {
    _propTypeRepository = propTypeRepository;
    _mapper = mapper;
  }

  public async Task<PropTypeVm> Handle(GetByIdPropTypeQuery request, CancellationToken cancellationToken) {
    var propType = await _propTypeRepository.GetEntity(request.Id) ?? throw new Exception("PropType not found");
    return _mapper.Map<PropTypeVm>(propType);
  }
}