using AutoMapper;
using MediatR;
using RSApp.Core.Application.Features.Properties.Queries.GetAll;
using RSApp.Core.Services.Repositories;
using RSApp.Core.Services.Services;
using RSApp.Core.Services.ViewModels;

namespace RSApp.Core.Application.Features.Properties.Queries.GetAll {
    public class GetAllPropertiesQuery : IRequest<IEnumerable<PropertyVm>> { }

}

public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, IEnumerable<PropertyVm>> {
    private readonly IPropertyService _propService;
    private readonly IMapper _mapper;

    public GetAllPropertiesQueryHandler(IPropertyService propService, IMapper mapper) {
        _propService = propService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PropertyVm>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken) =>
        await _propService.GetAll();
}
