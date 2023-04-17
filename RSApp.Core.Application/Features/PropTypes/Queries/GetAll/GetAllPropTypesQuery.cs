using AutoMapper;
using MediatR;
using RSApp.Core.Services.Repositories;
using RSApp.Core.Services.ViewModels;

namespace RSApp.Core.Application.Features.PropTypes.Queries.GetAll;

public class GetAllPropTypesQuery : IRequest<IEnumerable<PropTypeVm>> {

}

public class GetAllPropTypesQueryHandler : IRequestHandler<GetAllPropTypesQuery, IEnumerable<PropTypeVm>> {
  private readonly IPropTypeRepository _propTypeRepository;
  private readonly IMapper _mapper;

  public GetAllPropTypesQueryHandler(IPropTypeRepository propTypeRepository, IMapper mapper) {
    _propTypeRepository = propTypeRepository;
    _mapper = mapper;
  }

  public async Task<IEnumerable<PropTypeVm>> Handle(GetAllPropTypesQuery request, CancellationToken cancellationToken) {
    var propTypes = await _propTypeRepository.GetAll();
    return _mapper.Map<IEnumerable<PropTypeVm>>(propTypes);
  }
}