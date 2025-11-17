using AutoMapper;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Clients.Commands.CreateClient
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateClientCommandHandler> _logger;
        private readonly IUnitOfWork _uow;

        public CreateClientCommandHandler(
            IMapper mapper,
            ILogger<CreateClientCommandHandler> logger,
            IUnitOfWork uow)
        {
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
        }

        public async Task<int> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء عملية إنشاء عميل جديد: {ClientName}", request.FullName);

            // Email uniqueness
            if (!string.IsNullOrEmpty(request.Email))
            {
                var emailExists = await _uow.Repository<Client>().ExistsAsync(c => !c.IsDeleted && c.Email == request.Email);
                if (emailExists)
                {
                    _logger.LogWarning("فشلت محاولة إنشاء عميل ببريد إلكتروني موجود بالفعل: {Email}", request.Email);
                    throw new InvalidOperationException($"البريد الإلكتروني '{request.Email}' مستخدم بالفعل.");
                }
            }

            // Phone uniqueness
            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                var phoneExists = await _uow.Repository<Client>().ExistsAsync(c => !c.IsDeleted && c.PhoneNumber == request.PhoneNumber);
                if (phoneExists)
                {
                    _logger.LogWarning("فشلت محاولة إنشاء عميل برقم هاتف موجود بالفعل: {Phone}", request.PhoneNumber);
                    throw new InvalidOperationException($"رقم الهاتف '{request.PhoneNumber}' مستخدم بالفعل.");
                }
            }

            // Map to entity
            var clientEntity = _mapper.Map<Client>(request);

            // Handle national id image          

            await _uow.Repository<Client>().AddAsync(clientEntity);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم إنشاء العميل {ClientName} بنجاح بالمعرف: {ClientId}", clientEntity.FullName, clientEntity.Id);
            return clientEntity.Id;
        }
    }
}
