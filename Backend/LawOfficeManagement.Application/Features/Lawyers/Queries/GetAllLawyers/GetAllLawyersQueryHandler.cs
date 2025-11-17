using AutoMapper;
using LawOfficeManagement.Application.Features.Lawyers.DTOs;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Lawyers.Queries.GetAllLawyers
{
    public class GetAllLawyersQueryHandler : IRequestHandler<GetAllLawyersQuery, IReadOnlyList<LawyerDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllLawyersQueryHandler> _logger;

        public GetAllLawyersQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetAllLawyersQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IReadOnlyList<LawyerDto>> Handle(GetAllLawyersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Ã·» Ã„Ì⁄ «·„Õ«„Ì‰.");

            var lawyers = await _uow.Repository<Lawyer>().GetAllAsync(l => !l.IsDeleted);
            return _mapper.Map<IReadOnlyList<LawyerDto>>(lawyers);
        }
    }
}
