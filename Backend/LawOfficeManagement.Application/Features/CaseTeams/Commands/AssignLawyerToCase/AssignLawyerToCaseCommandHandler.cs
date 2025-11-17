using AutoMapper;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseTeams.Commands.AssignLawyerToCase
{
    public class AssignLawyerToCaseCommand : IRequest<int>
    {
        public int LawyerId { get; set; }
        public int CaseId { get; set; }
        public string Role { get; set; } = "مساعد";
    }
    public class AssignLawyerToCaseCommandHandler : IRequestHandler<AssignLawyerToCaseCommand, int>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AssignLawyerToCaseCommandHandler> _logger;

        public AssignLawyerToCaseCommandHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<AssignLawyerToCaseCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(AssignLawyerToCaseCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء تعيين المحامي {LawyerId} للقضية {CaseId}",
                request.LawyerId, request.CaseId);

            // التحقق من وجود القضية
            var caseExists = await _uow.Repository<Case>()
                .ExistsAsync(c => c.Id == request.CaseId);
            if (!caseExists)
                throw new InvalidOperationException("القضية غير موجودة");

            // التحقق من وجود المحامي
            var lawyerExists = await _uow.Repository<Lawyer>()
                .ExistsAsync(l => l.Id == request.LawyerId && !l.IsDeleted);
            if (!lawyerExists)
                throw new InvalidOperationException("المحامي غير موجود أو محذوف");

            // التحقق من عدم تكرار إضافة المحامي لنفس القضية
            var existingAssignment = await _uow.Repository<CaseTeam>()
                .ExistsAsync(ct => ct.CaseId == request.CaseId &&
                                  ct.LawyerId == request.LawyerId);
            if (existingAssignment)
                throw new InvalidOperationException("المحامي مضاف مسبقاً لفريق هذه القضية");

            // البحث التلقائي عن الوكالة المشتقة إذا لم يتم توفيرها
         
          

            // إنشاء سجل فريق العمل
            var caseTeam = new CaseTeam
            {
                LawyerId = request.LawyerId,
                CaseId = request.CaseId,
                Role = request.Role,
                StartDate = DateTime.UtcNow,
                IsActive = true
            };

            await _uow.Repository<CaseTeam>().AddAsync(caseTeam);

            _logger.LogInformation("تم تعيين المحامي {LawyerId} للقضية {CaseId} بنجاح بالمعرف {TeamId}",
                request.LawyerId, request.CaseId, caseTeam.Id);

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