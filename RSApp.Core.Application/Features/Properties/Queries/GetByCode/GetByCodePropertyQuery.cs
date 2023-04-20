using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RSApp.Core.Services.Services;
using RSApp.Core.Services.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace RSApp.Core.Application.Features.Properties.Queries.GetByCode
{
/// <summary>
/// Property by Code
/// </summary>
    public class GetByCodePropertyQuery:IRequest<PropertyVm>
    {
        [SwaggerParameter(Description = "Code for property")]
        public string Code { get; set; } = null!;
    }

        public class GetByCodePropertyQueryHandler : IRequestHandler<GetByCodePropertyQuery, PropertyVm> {
        private readonly IPropertyService _propService;
        private readonly IMapper _mapper;

        public GetByCodePropertyQueryHandler(IPropertyService propService, IMapper mapper) {
            _propService = propService;
            _mapper = mapper;
        }

        public async Task<PropertyVm> Handle(GetByCodePropertyQuery request, CancellationToken cancellationToken) {
            var property = await _propService.GetAll().ContinueWith(r=>r.Result.FirstOrDefault(c=>c.Code == request.Code));
            return property;
        }
}
}