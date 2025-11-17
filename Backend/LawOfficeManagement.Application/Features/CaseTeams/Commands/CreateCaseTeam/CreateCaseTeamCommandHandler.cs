using AutoMapper;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseTeams.Commands.CreateCaseTeam
{
    public class CreateCaseTeamCommandHandler : IRequestHandler<CreateCaseTeamCommand, int>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCaseTeamCommandHandler> _logger;

        public CreateCaseTeamCommandHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<CreateCaseTeamCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CreateCaseTeamCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء إضافة محامي لفريق القضية {CaseId}", request.CreateDto.CaseId);

            // التحقق من وجود القضية
            var caseExists = await _uow.Repository<Case>()
                .ExistsAsync(c => c.Id == request.CreateDto.CaseId);
            if (!caseExists)
                throw new InvalidOperationException("القضية غير موجودة");

            // التحقق من وجود المحامي
            var lawyerExists = await _uow.Repository<Lawyer>()
                .ExistsAsync(l => l.Id == request.CreateDto.LawyerId && !l.IsDeleted);
            if (!lawyerExists)
                throw new InvalidOperationException("المحامي غير موجود أو محذوف");

            // التحقق من عدم تكرار إضافة المحامي لنفس القضية
            var existingAssignment = await _uow.Repository<CaseTeam>()
                .ExistsAsync(ct => ct.CaseId == request.CreateDto.CaseId &&
                                  ct.LawyerId == request.CreateDto.LawyerId);
            if (existingAssignment)
                throw new InvalidOperationException("المحامي مضاف مسبقاً لفريق هذه القضية");

            var caseTeam = _mapper.Map<CaseTeam>(request.CreateDto);

          
             
            await _uow.Repository<CaseTeam>().AddAsync(caseTeam);

            _logger.LogInformation("تم إضافة المحامي {LawyerId} لفريق القضية {CaseId} بالمعرف {TeamId}",
                request.CreateDto.LawyerId, request.CreateDto.CaseId, caseTeam.Id);

            return caseTeam.Id;
        }

        private async Task<DerivedPowerOfAttorney?> FindDerivedPowerOfAttorneyAsync(int lawyerId, int caseId)
        {
            var derivedPowerOfAttorney = await _uow.Repository<DerivedPowerOfAttorney>()
                .FirstOrDefaultAsync(
                    predicate: poa => poa.LawyerId == lawyerId &&
                                     poa.ParentPowerOfAttorney.ClientId == caseId &&
                                     poa.IsActive,
                    includeProperties: ""
                );

            return derivedPowerOfAttorney;
        }
    }
}