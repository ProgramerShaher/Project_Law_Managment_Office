using AutoMapper;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Lawyers.Commands.CreateLawyer
{
    public class CreateLawyerCommandHandler : IRequestHandler<CreateLawyerCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateLawyerCommandHandler> _logger;
        private readonly IUnitOfWork _uow;

        public CreateLawyerCommandHandler(
            IMapper mapper,
            ILogger<CreateLawyerCommandHandler> logger,
            IUnitOfWork uow)
        {
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
        }

        public async Task<int> Handle(CreateLawyerCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء عملية إنشاء محامي جديد: {LawyerName}", request.FullName);

            // ✅ التأكد من عدم وجود محامي بنفس البريد الإلكتروني
            if (!string.IsNullOrEmpty(request.Email))
            {
                var emailExists = await _uow.Repository<Lawyer>()
                    .ExistsAsync(l => !l.IsDeleted && l.Email == request.Email);
                if (emailExists)
                {
                    _logger.LogWarning("محاولة إنشاء محامي ببريد مستخدم مسبقًا: {Email}", request.Email);
                    throw new InvalidOperationException($"البريد الإلكتروني '{request.Email}' مستخدم بالفعل.");
                }
            }

            // ✅ التأكد من عدم وجود رقم هاتف مكرر
            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                var phoneExists = await _uow.Repository<Lawyer>()
                    .ExistsAsync(l => !l.IsDeleted && l.PhoneNumber == request.PhoneNumber);
                if (phoneExists)
                {
                    _logger.LogWarning("محاولة إنشاء محامي برقم هاتف مستخدم مسبقًا: {Phone}", request.PhoneNumber);
                    throw new InvalidOperationException($"رقم الهاتف '{request.PhoneNumber}' مستخدم بالفعل.");
                }
            }

            // 🧩 التحويل إلى الكيان (Entity)
            var lawyerEntity = _mapper.Map<Lawyer>(request);
     

            // 💾 حفظ في قاعدة البيانات
            await _uow.Repository<Lawyer>().AddAsync(lawyerEntity);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم إنشاء المحامي {LawyerName} بنجاح بالمعرف: {LawyerId}",
                lawyerEntity.FullName, lawyerEntity.Id);

            return lawyerEntity.Id;
        }
    }
}
