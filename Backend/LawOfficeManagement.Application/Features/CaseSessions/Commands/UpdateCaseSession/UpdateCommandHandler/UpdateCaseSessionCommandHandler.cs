using AutoMapper;
using LawOfficeManagement.Application.Exceptions;
using LawOfficeManagement.Application.Features.CaseSessions.Commands.UpdateCaseSession.Command;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseSessions.Commands
{
    public class UpdateCaseSessionCommandHandler : IRequestHandler<UpdateCaseSessionCommand>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCaseSessionCommandHandler> _logger;
        private readonly IUnitOfWork _uow;

        public UpdateCaseSessionCommandHandler(
            IMapper mapper,
            ILogger<UpdateCaseSessionCommandHandler> logger,
            IUnitOfWork uow)
        {
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
        }

        public async Task Handle(UpdateCaseSessionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating case session with ID: {SessionId}", request.Id);

                // البحث عن الجلسة
                var caseSession = await _uow.Repository<CaseSession>().GetByIdAsync(request.Id);
                if (caseSession == null)
                {
                    _logger.LogWarning("Case session with ID {SessionId} not found", request.Id);
                    throw new NotFoundException($"Case session with ID {request.Id} not found");
                }

                // تحديث البيانات
                _mapper.Map(request.UpdateCaseSessionDto, caseSession);

                // تحديث تاريخ التعديل
                caseSession.LastModifiedAt = DateTime.UtcNow;
                caseSession.LastModifiedBy = "System"; // يمكنك استبدالها بالمستخدم الحالي

                // الحفظ
                await _uow.Repository<CaseSession>().UpdateAsync(caseSession);
                await _uow.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Successfully updated case session with ID: {SessionId}", request.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating case session with ID: {SessionId}", request.Id);
                throw;
            }
        }
    }
}