using AutoMapper;
using LawOfficeManagement.Application.Features.Opponents.DTOs;
using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeManagement.Application.Features.Opponents.Commands
{
    public class UpdateOpponentCommand : IRequest<Unit>
    {
        public UpdateOpponentDto UpdateDto { get; set; }
    }

    public class UpdateOpponentCommandHandler : IRequestHandler<UpdateOpponentCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOpponentCommandHandler> _logger;

        public UpdateOpponentCommandHandler(IUnitOfWork uow, IMapper mapper, ILogger<UpdateOpponentCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateOpponentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء تحديث الخصم: {OpponentId}", request.UpdateDto.Id);

            var opponent = await _uow.Repository<Opponent>()
                .GetByIdAsync(request.UpdateDto.Id);

            if (opponent == null || opponent.IsDeleted)
            {
                _logger.LogWarning("الخصم غير موجود: {OpponentId}", request.UpdateDto.Id);
                throw new KeyNotFoundException($"الخصم بالمعرف {request.UpdateDto.Id} غير موجود");
            }

            // التحقق من عدم تكرار الاسم ورقم الجوال مع خصم آخر
            if (opponent.OpponentName != request.UpdateDto.OpponentName ||
                opponent.OpponentMobile != request.UpdateDto.OpponentMobile)
            {
                var duplicateExists = await _uow.Repository<Opponent>()
                    .ExistsAsync(o => o.OpponentName == request.UpdateDto.OpponentName &&
                                     o.OpponentMobile == request.UpdateDto.OpponentMobile &&
                                     o.Id != request.UpdateDto.Id &&
                                     !o.IsDeleted);

                if (duplicateExists)
                {
                    _logger.LogWarning("محاولة تحديث خصم باسم ورقم جوال موجودين مسبقًا: {Name} - {Mobile}",
                        request.UpdateDto.OpponentName, request.UpdateDto.OpponentMobile);
                    throw new InvalidOperationException($"الخصم '{request.UpdateDto.OpponentName}' بنفس رقم الجوال موجود مسبقًا");
                }
            }

            _mapper.Map(request.UpdateDto, opponent);

            await _uow.Repository<Opponent>().UpdateAsync(opponent);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("تم تحديث الخصم بنجاح: {OpponentId}", opponent.Id);

            return Unit.Value;
        }
    }
}
