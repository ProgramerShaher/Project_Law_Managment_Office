using LawOfficeManagement.Application.Features.Cases.Commands.Dtos;
using MediatR;

namespace LawOfficeManagement.Application.Features.Cases.Commands.CreateCaseSession
{
    public class CreateCaseSessionCommand : IRequest<int>
    {
        public CreateCaseSessionDto CreateCaseSessionDto { get; set; } = new();
    }
}