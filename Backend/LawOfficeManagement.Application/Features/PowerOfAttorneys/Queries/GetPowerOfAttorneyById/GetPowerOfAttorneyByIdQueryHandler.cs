using AutoMapper;
using LawOfficeManagement.Application.Features.PowerOfAttorneys.DTOs;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.PowerOfAttorneys.Queries.GetPowerOfAttorneyById
{
    public class GetPowerOfAttorneyByIdHandler : IRequestHandler<GetPowerOfAttorneyByIdQuery, PowerOfAttorneyDto?>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPowerOfAttorneyByIdHandler> _logger;

        public GetPowerOfAttorneyByIdHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetPowerOfAttorneyByIdHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PowerOfAttorneyDto?> Handle(GetPowerOfAttorneyByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جاري جلب الوكالة ذات المعرف {Id}", request.Id);

            var entity = await _uow.Repository<PowerOfAttorney>()
          .FirstOrDefaultAsync(
           p => p.Id == request.Id,
           includeProperties: "Client,Office,Lawyer"
      );

            if (entity == null || entity.IsDeleted)
            {
                _logger.LogWarning("لم يتم العثور على الوكالة ذات المعرف {Id}", request.Id);
                return null;
            }

            return _mapper.Map<PowerOfAttorneyDto>(entity);
        }
    }
}
