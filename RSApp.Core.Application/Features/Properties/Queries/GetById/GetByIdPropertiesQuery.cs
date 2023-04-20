using AutoMapper;
using MediatR;
using RSApp.Core.Services.Repositories;
using RSApp.Core.Services.Services;
using RSApp.Core.Services.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace RSApp.Core.Application.Features.Properties.Queries.GetById {
/// <summary>
/// Property by ID
/// </summary>
    public class GetByIdPropertiesQuery : IRequest<PropertyVm> {
        [SwaggerParameter(Description = "ID for property")]
        public int Id { get; set; }
    }

    public class GetByIdPropertiesQueryHandler : IRequestHandler<GetByIdPropertiesQuery, PropertyVm> {
        private readonly IPropertyService _propService;
        private readonly IMapper _mapper;

        public GetByIdPropertiesQueryHandler(IPropertyService propService, IMapper mapper) {
            _propService = propService;
            _mapper = mapper;
        }

        public async Task<PropertyVm> Handle(GetByIdPropertiesQuery request, CancellationToken cancellationToken) {
            var property = await _propService.GetAll().ContinueWith(r=>r.Result.FirstOrDefault(l=>l.Id==request.Id)) ?? throw new Exception("PropType not found");
            return property;
        }
    }
}
