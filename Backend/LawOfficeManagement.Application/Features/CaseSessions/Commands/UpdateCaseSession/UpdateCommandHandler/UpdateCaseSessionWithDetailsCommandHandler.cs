using AutoMapper;
using LawOfficeManagement.Application.Exceptions;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseSessions.Commands
{
    public class UpdateCaseSessionWithDetailsCommandHandler : IRequestHandler<UpdateCaseSessionWithDetailsCommand>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCaseSessionWithDetailsCommandHandler> _logger;
        private readonly IUnitOfWork _uow;

        public UpdateCaseSessionWithDetailsCommandHandler(
            IMapper mapper,
            ILogger<UpdateCaseSessionWithDetailsCommandHandler> logger,
            IUnitOfWork uow)
        {
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
        }

        public async Task Handle(UpdateCaseSessionWithDetailsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating case session with details for ID: {SessionId}", request.Id);

                // الحصول على الجلسة
                var caseSession = await _uow.Repository<CaseSession>()
                    .GetAll()
                    .FirstOrDefaultAsync(cs => cs.Id == request.Id && !cs.IsDeleted, cancellationToken);

                if (caseSession == null)
                {
                    throw new NotFoundException($"Case session with ID {request.Id} not found");
                }

                // 🔹 استخدام AutoMapper لتحديث الجلسة
                _mapper.Map(request.UpdateCaseSessionDto, caseSession);
                caseSession.LastModifiedAt = DateTime.UtcNow;
                caseSession.LastModifiedBy = "System";

                // تحديث الجلسة
                await _uow.Repository<CaseSession>().UpdateAsync(caseSession);
                await _uow.SaveChangesAsync(cancellationToken);

                // معالجة الأدلة
                await ProcessEvidences(caseSession, request.Evidences, cancellationToken);

                // معالجة الشهود
                await ProcessWitnesses(caseSession, request.Witnesses, cancellationToken);

                _logger.LogInformation("Successfully updated case session with details for ID: {SessionId}", request.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating case session with details for ID: {SessionId}", request.Id);
                throw;
            }
        }

        private async Task ProcessEvidences(CaseSession caseSession, List<UpdateCaseEvidenceDto> evidenceDtos, CancellationToken cancellationToken)
        {
            foreach (var evidenceDto in evidenceDtos)
            {
                if (evidenceDto.IsDeleted && evidenceDto.Id > 0)
                {
                    // حذف الدليل الموجود
                    var existingEvidence = await _uow.Repository<CaseEvidence>()
                        .GetByIdAsync(evidenceDto.Id);

                    if (existingEvidence != null && existingEvidence.CaseSessionId == caseSession.Id)
                    {
                        existingEvidence.IsDeleted = true;
                        existingEvidence.LastModifiedAt = DateTime.UtcNow;
                        existingEvidence.LastModifiedBy = "System";
                        await _uow.Repository<CaseEvidence>().UpdateAsync(existingEvidence);
                    }
                }
                else if (evidenceDto.Id == 0 && !evidenceDto.IsDeleted)
                {
                    // 🔹 استخدام AutoMapper لإنشاء دليل جديد
                    var newEvidence = _mapper.Map<CaseEvidence>(evidenceDto);
                    newEvidence.CaseId = caseSession.CaseId ?? 0;
                    newEvidence.CaseSessionId = caseSession.Id;
                    newEvidence.CreatedBy = "System";
                    newEvidence.CreatedAt = DateTime.UtcNow;

                    await _uow.Repository<CaseEvidence>().AddAsync(newEvidence);
                }
                else if (evidenceDto.Id > 0 && !evidenceDto.IsDeleted)
                {
                    // تحديث دليل موجود
                    var existingEvidence = await _uow.Repository<CaseEvidence>()
                        .GetByIdAsync(evidenceDto.Id);

                    if (existingEvidence != null && existingEvidence.CaseSessionId == caseSession.Id)
                    {
                        // 🔹 استخدام AutoMapper لتحديث الدليل
                        _mapper.Map(evidenceDto, existingEvidence);
                        existingEvidence.LastModifiedAt = DateTime.UtcNow;
                        existingEvidence.LastModifiedBy = "System";

                        await _uow.Repository<CaseEvidence>().UpdateAsync(existingEvidence);
                    }
                }
            }

            await _uow.SaveChangesAsync(cancellationToken);
        }

        private async Task ProcessWitnesses(CaseSession caseSession, List<UpdateCaseWitnessDto> witnessDtos, CancellationToken cancellationToken)
        {
            foreach (var witnessDto in witnessDtos)
            {
                if (witnessDto.IsDeleted && witnessDto.Id > 0)
                {
                    // حذف الشاهد الموجود
                    var existingWitness = await _uow.Repository<CaseWitness>()
                        .GetByIdAsync(witnessDto.Id);

                    if (existingWitness != null && existingWitness.CaseSessionId == caseSession.Id)
                    {
                        existingWitness.IsDeleted = true;
                        existingWitness.LastModifiedAt = DateTime.UtcNow;
                        existingWitness.LastModifiedBy = "System";
                        await _uow.Repository<CaseWitness>().UpdateAsync(existingWitness);
                    }
                }
                else if (witnessDto.Id == 0 && !witnessDto.IsDeleted)
                {
                    // 🔹 استخدام AutoMapper لإنشاء شاهد جديد
                    var newWitness = _mapper.Map<CaseWitness>(witnessDto);
                    newWitness.CaseId = caseSession.CaseId ?? 0;
                    newWitness.CaseSessionId = caseSession.Id;
                    newWitness.CreatedBy = "System";
                    newWitness.CreatedAt = DateTime.UtcNow;

                    await _uow.Repository<CaseWitness>().AddAsync(newWitness);
                }
                else if (witnessDto.Id > 0 && !witnessDto.IsDeleted)
                {
                    // تحديث شاهد موجود
                    var existingWitness = await _uow.Repository<CaseWitness>()
                        .GetByIdAsync(witnessDto.Id);

                    if (existingWitness != null && existingWitness.CaseSessionId == caseSession.Id)
                    {
                        // 🔹 استخدام AutoMapper لتحديث الشاهد
                        _mapper.Map(witnessDto, existingWitness);
                        existingWitness.LastModifiedAt = DateTime.UtcNow;
                        existingWitness.LastModifiedBy = "System";

                        await _uow.Repository<CaseWitness>().UpdateAsync(existingWitness);
                    }
                }
            }

            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}