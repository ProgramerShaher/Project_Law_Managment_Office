using AutoMapper;
using LawOfficeManagement.Application.Exceptions;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Cases.Commands.CreateCaseSession
{
    public class CreateCaseSessionCommandHandler : IRequestHandler<CreateCaseSessionCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCaseSessionCommandHandler> _logger;
        private readonly IUnitOfWork _uow;

        public CreateCaseSessionCommandHandler(
            IMapper mapper,
            ILogger<CreateCaseSessionCommandHandler> logger,
            IUnitOfWork uow)
        {
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
        }

        public async Task<int> Handle(CreateCaseSessionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating new case session for case ID: {CaseId}",
                    request.CreateCaseSessionDto.CaseId);

                // تحقق من وجود القضية
                if (request.CreateCaseSessionDto.CaseId.HasValue)
                {
                    var caseExists = await _uow.Repository<Case>().GetByIdAsync(
                        request.CreateCaseSessionDto.CaseId.Value);
                    if (caseExists == null)
                    {
                        throw new NotFoundException($"Case with ID {request.CreateCaseSessionDto.CaseId} not found");
                    }
                }

                // إنشاء الجلسة
                var caseSession = _mapper.Map<CaseSession>(request.CreateCaseSessionDto);

                await _uow.Repository<CaseSession>().AddAsync(caseSession);
                await _uow.SaveChangesAsync(cancellationToken);

                // إضافة الأدلة
                foreach (var evidenceDto in request.CreateCaseSessionDto.Evidences)
                {
                    var evidence = _mapper.Map<CaseEvidence>(evidenceDto);
                    evidence.CaseId = caseSession.CaseId ?? 0;
                    evidence.CaseSessionId = caseSession.Id;
                    await _uow.Repository<CaseEvidence>().AddAsync(evidence);
                }

                // إضافة الشهود
                foreach (var witnessDto in request.CreateCaseSessionDto.Witnesses)
                {
                    var witness = _mapper.Map<CaseWitness>(witnessDto);
                    witness.CaseId = caseSession.CaseId ?? 0;
                    witness.CaseSessionId = caseSession.Id;
                    await _uow.Repository<CaseWitness>().AddAsync(witness);
                }

                await _uow.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Successfully created case session with ID: {SessionId}", caseSession.Id);

                return caseSession.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating case session");
                throw;
            }
        }
    }

}