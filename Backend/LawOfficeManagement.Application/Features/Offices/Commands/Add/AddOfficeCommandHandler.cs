using AutoMapper;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Offices.Commands.Add
;
    public class AddOfficeCommandHandler : IRequestHandler<AddOfficeCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AddOfficeCommandHandler> _logger;
        private readonly IUnitOfWork _uow;

        public AddOfficeCommandHandler(
            IMapper mapper,
            ILogger<AddOfficeCommandHandler> logger,
            IUnitOfWork uow)
        {
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
        }

        public async Task<int> Handle(AddOfficeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء عملية إنشاء المكتب");

            var existing = await _uow.Repository<Office>().FirstOrDefaultAsync(o => true);
            if (existing != null)
            {
                _logger.LogWarning("المكتب موجود بالفعل، لا يمكن إنشاء أكثر من سجل واحد");
                throw new InvalidOperationException("المكتب موجود بالفعل!");
            }

            //var officeEntity = _mapper.Map<Office>(request);

            Office office = new Office
            {
                OfficeName = request.OfficeName,
                ManagerName = request.ManagerName,
                Address = request.Address,
                WebSitUrl = request.WebSitUrl,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                LicenseNumber = request.LicenseNumber,
            };
            await _uow.Repository<Office>().AddAsync(office);

            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم إنشاء المكتب بنجاح بالمعرف: {OfficeId}", office.Id);
            return office.Id;
        }
    }

