using AutoMapper;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Lawyers.Commands.UpdateLawyer
{
    public class UpdateLawyerCommandHandler : IRequestHandler<UpdateLawyerCommand, bool>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateLawyerCommandHandler> _logger;

        public UpdateLawyerCommandHandler(IUnitOfWork uow,  IMapper mapper, ILogger<UpdateLawyerCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdateLawyerCommand request, CancellationToken cancellationToken)
        {
            var lawyerRepo = _uow.Repository<Lawyer>();
            var lawyer = await lawyerRepo.GetByIdAsync(request.Id);

            if (lawyer == null || lawyer.IsDeleted)
            {
                _logger.LogWarning("„Õ«Ê·…  ÕœÌÀ „Õ«„Ì €Ì— „ÊÃÊœ »«·„⁄—› {LawyerId}", request.Id);
                return false;
            }

            //  ÕœÌÀ «·»Ì«‰« 
            _mapper.Map(request, lawyer);         
            await _uow.Repository<Lawyer>().UpdateAsync(lawyer);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(" „  ÕœÌÀ «·„Õ«„Ì »‰Ã«Õ »«·„⁄—› {LawyerId}", lawyer.Id);
            return true;
        }
    }
}
