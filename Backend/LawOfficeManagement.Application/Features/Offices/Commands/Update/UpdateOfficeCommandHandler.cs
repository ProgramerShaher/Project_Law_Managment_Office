using AutoMapper;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Offices.Commands.Update
{
    public class UpdateOfficeCommandHandler : IRequestHandler<UpdateOfficeCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOfficeCommandHandler> _logger;
        private readonly IUnitOfWork _uow;

        public UpdateOfficeCommandHandler(
            IMapper mapper,
            ILogger<UpdateOfficeCommandHandler> logger,
            IUnitOfWork uow)
        {
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
        }

        public async Task<bool> Handle(UpdateOfficeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء عملية تعديل بيانات المكتب");

            var office = await _uow.Repository<Office>().FirstOrDefaultAsync(o => true);

            if (office == null)
            {
                _logger.LogWarning("لا يوجد مكتب موجود للتعديل");
                throw new InvalidOperationException("المكتب غير موجود!");
            }

            // 🔹 تحديث القيم
            office.OfficeName = request.OfficeName;
            office.ManagerName = request.ManagerName;
            office.Address = request.Address;
            office.WebSitUrl = request.WebSitUrl;
            office.PhoneNumber = request.PhoneNumber;
            office.Email = request.Email;
            office.LicenseNumber = request.LicenseNumber;

            await _uow.Repository<Office>().UpdateAsync(office);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم تعديل بيانات المكتب بنجاح");
            return true;
        }
    }
}
