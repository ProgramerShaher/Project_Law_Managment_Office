using AutoMapper;
using LawOfficeManagement.Application.Features.Lawyers.DTOs;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Lawyers.Queries.GetLawyerById
{
    public class GetLawyerByIdQueryHandler : IRequestHandler<GetLawyerByIdQuery, LawyerDto?>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetLawyerByIdQueryHandler> _logger;

        public GetLawyerByIdQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetLawyerByIdQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<LawyerDto?> Handle(GetLawyerByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Ã·» »Ì«‰«  «·„Õ«„Ì »«·„⁄—› {LawyerId}", request.Id);

            var lawyer = await _uow.Repository<Lawyer>().GetByIdAsync(request.Id);
            if (lawyer == null || lawyer.IsDeleted)
            {
                _logger.LogWarning("·„ Ì „ «·⁄ÀÊ— ⁄·Ï «·„Õ«„Ì »«·„⁄—› {LawyerId}", request.Id);
                return null;
            }

            return _mapper.Map<LawyerDto>(lawyer);
        }
    }
}
