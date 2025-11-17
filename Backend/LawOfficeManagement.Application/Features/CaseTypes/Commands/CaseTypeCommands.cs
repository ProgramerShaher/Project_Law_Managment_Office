using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawOfficeManagement.Application.Features.CaseTypes.Commands
{
    // CreateCaseTypeCommand
    public class CreateCaseTypeCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    // UpdateCaseTypeCommand
    public class UpdateCaseTypeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    // DeleteCaseTypeCommand
    public class DeleteCaseTypeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}