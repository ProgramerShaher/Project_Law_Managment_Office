using LawOfficeManagement.Core.Entities.Documents;
using LawOfficeManagement.Core.Enums;
using MediatR;

namespace LawOfficeManagement.Application.Features.Documents.Commands.CreateDocument
{
    public class CreateDocumentCommand : IRequest<int>
    {
        public string DocumentName { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public DocumentType DocumentType { get; set; }
        public int EntityId { get; set; }
        public EntityOwnerType EntityOwnerType { get; set; }
    }
}
