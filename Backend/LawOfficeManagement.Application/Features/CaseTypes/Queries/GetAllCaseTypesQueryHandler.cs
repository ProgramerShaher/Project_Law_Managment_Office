using AutoMapper;
using LawOfficeManagement.Application.Features.Courts.Queries.GetAllCourts;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseTypes.Queries.GetAllCaseTypes
{
    public class GetAllCaseTypesQuery : IRequest<List<CaseTypeDto>>
    {
    }
    public class GetAllCaseTypesQueryHandler : IRequestHandler<GetAllCaseTypesQuery, List<CaseTypeDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllCaseTypesQueryHandler> _logger;

        public GetAllCaseTypesQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<GetAllCaseTypesQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CaseTypeDto>> Handle(GetAllCaseTypesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء عملية جلب جميع أنواع القضايا");

            var caseTypes = await _uow.Repository<CaseType>()
                .GetAllAsync();

            var result = _mapper.Map<List<CaseTypeDto>>(caseTypes);

            _logger.LogInformation("تم جلب {Count} نوع قضية بنجاح", result.Count);
            return result;
        }
    }
}