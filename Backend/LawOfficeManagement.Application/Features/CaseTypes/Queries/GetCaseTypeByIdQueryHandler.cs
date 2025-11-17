using AutoMapper;
using LawOfficeManagement.Application.Features.CaseTypes.Queries.GetAllCaseTypes;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseTypes.Queries
{
    public class GetCaseTypeByIdQuery : IRequest<CaseTypeDto>
    {
        public int Id { get; set; }
    }
    public class GetCaseTypeByIdQueryHandler : IRequestHandler<GetCaseTypeByIdQuery, CaseTypeDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCaseTypeByIdQueryHandler> _logger;

        public GetCaseTypeByIdQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<GetCaseTypeByIdQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CaseTypeDto> Handle(GetCaseTypeByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء عملية جلب نوع القضية بالمعرف: {CaseTypeId}", request.Id);

            var caseType = await _uow.Repository<CaseType>().GetByIdAsync(request.Id);

            if (caseType == null || caseType.IsDeleted)
            {
                _logger.LogWarning("نوع القضية غير موجود: {CaseTypeId}", request.Id);
                throw new InvalidOperationException($"نوع القضية بالمعرف {request.Id} غير موجود.");
            }

            var result = _mapper.Map<CaseTypeDto>(caseType);

            _logger.LogInformation("تم جلب نوع القضية بنجاح: {CaseTypeName}", caseType.Name);
            return result;
        }
    }
}