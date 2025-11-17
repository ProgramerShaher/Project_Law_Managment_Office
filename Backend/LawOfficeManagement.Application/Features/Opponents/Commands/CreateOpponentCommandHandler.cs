// LawOfficeManagement.Application.Features.Opponents.Commands
using AutoMapper;
using LawOfficeManagement.Application.Features.Opponents.DTOs;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.Opponents.Commands
{
    // Create Opponent
    public class CreateOpponentCommand : IRequest<int>
    {
        public CreateOpponentDto CreateDto { get; set; }
    }

    public class CreateOpponentCommandHandler : IRequestHandler<CreateOpponentCommand, int>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateOpponentCommandHandler> _logger;

        public CreateOpponentCommandHandler(IUnitOfWork uow, IMapper mapper, ILogger<CreateOpponentCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CreateOpponentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء إنشاء خصم جديد: {OpponentName}", request.CreateDto.OpponentName);

            // التحقق من عدم وجود خصم بنفس الاسم ورقم الجوال
            var opponentExists = await _uow.Repository<Opponent>()
                .ExistsAsync(o => o.OpponentName == request.CreateDto.OpponentName &&
                                 o.OpponentMobile == request.CreateDto.OpponentMobile &&
                                 !o.IsDeleted);

            if (opponentExists)
            {
                _logger.LogWarning("محاولة إنشاء خصم موجود مسبقًا: {OpponentName} - {Mobile}",
                    request.CreateDto.OpponentName, request.CreateDto.OpponentMobile);
                throw new InvalidOperationException($"الخصم '{request.CreateDto.OpponentName}' بنفس رقم الجوال موجود مسبقًا");
            }

            var opponent = _mapper.Map<Opponent>(request.CreateDto);

            await _uow.Repository<Opponent>().AddAsync(opponent);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم إنشاء الخصم بنجاح بالمعرف: {OpponentId}", opponent.Id);

            return opponent.Id;
        }
    }

    
 
}