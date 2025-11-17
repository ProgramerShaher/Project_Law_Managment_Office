using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LawOfficeManagement.Application.Exceptions;
using LawOfficeManagement.Application.Features.Cases.Commands.Dtos;
using LawOfficeManagement.Application.Features.CaseSessions.Dtos;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseSessions.Commands.UpdateCaseSession.Command
{
    public class UpdateCaseSessionCommand : IRequest
    {
        public int Id { get; set; }
        public UpdateCaseSessionDto UpdateCaseSessionDto { get; set; } = new();
    }

    public class UpdateCaseSessionCommandHandler : IRequestHandler<UpdateCaseSessionCommand>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCaseSessionCommandHandler> _logger;
        private readonly IUnitOfWork _uow;

        public UpdateCaseSessionCommandHandler(
            IMapper mapper,
            ILogger<UpdateCaseSessionCommandHandler> logger,
            IUnitOfWork uow)
        {
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
        }

        public async Task Handle(UpdateCaseSessionCommand request, CancellationToken cancellationToken)
        {
            var caseSession = await _uow.Repository<CaseSession>().GetByIdAsync(request.Id);
            if (caseSession == null)
            {
                throw new NotFoundException($"Case session with ID {request.Id} not found");
            }

            _mapper.Map(request.UpdateCaseSessionDto, caseSession);

            await _uow.Repository<CaseSession>().UpdateAsync(caseSession);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Successfully updated case session with ID: {SessionId}", request.Id);
        }
    }
}
