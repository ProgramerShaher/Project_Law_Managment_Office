using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Clients.Commands.Update
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, bool>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<UpdateClientCommandHandler> _logger;

        public UpdateClientCommandHandler(
            IUnitOfWork uow,
            ILogger<UpdateClientCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _uow.Repository<Client>().GetByIdAsync(request.Id);
            if (client == null || client.IsDeleted)
                return false;

            if (!string.IsNullOrEmpty(request.Email))
            {
                var emailExists = await _uow.Repository<Client>().ExistsAsync(c => c.Id != request.Id && !c.IsDeleted && c.Email == request.Email);
                if (emailExists)
                    throw new InvalidOperationException($"البريد الإلكتروني '{request.Email}' مستخدم بالفعل.");
            }

            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                var phoneExists = await _uow.Repository<Client>().ExistsAsync(c => c.Id != request.Id && !c.IsDeleted && c.PhoneNumber == request.PhoneNumber);
                if (phoneExists)
                    throw new InvalidOperationException($"رقم الهاتف '{request.PhoneNumber}' مستخدم بالفعل.");
            }

            client.FullName = request.FullName;
            client.BirthDate = request.BirthDate;
            client.ClientType = request.ClientType;
            client.ClientRoleId = request.ClientRoleId;
            client.Email = request.Email;
            client.PhoneNumber = request.PhoneNumber;
            client.Address = request.Address;
            client.UrlImageNationalId = request.NationalIdImage;

            await _uow.Repository<Client>().UpdateAsync(client);
            await _uow.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("تم تحديث العميل {ClientId}", client.Id);
            return true;
        }
    }
}
