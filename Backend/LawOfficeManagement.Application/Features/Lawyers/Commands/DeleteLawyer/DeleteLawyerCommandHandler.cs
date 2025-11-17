using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Lawyers.Commands.DeleteLawyer
{
    public class DeleteLawyerCommandHandler : IRequestHandler<DeleteLawyerCommand, bool>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DeleteLawyerCommandHandler> _logger;

        public DeleteLawyerCommandHandler(IUnitOfWork uow, ILogger<DeleteLawyerCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteLawyerCommand request, CancellationToken cancellationToken)
        {
            var lawyer = await _uow.Repository<Lawyer>().GetByIdAsync(request.Id);
            if (lawyer == null || lawyer.IsDeleted)
            {
                _logger.LogWarning("„Õ«Ê·… Õ–› „Õ«„Ì €Ì— „ÊÃÊœ »«·„⁄—› {LawyerId}", request.Id);
                return false;
            }

            lawyer.IsDeleted = true;
            await _uow.Repository<Lawyer>().UpdateAsync(lawyer);
            await _uow.SaveChangesAsync(cancellationToken);
        

            _logger.LogInformation(" „ Õ–› «·„Õ«„Ì »«·„⁄—› {LawyerId}", request.Id);
            return true;
        }
    }
}
