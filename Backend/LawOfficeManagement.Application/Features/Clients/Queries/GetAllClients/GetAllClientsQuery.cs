using MediatR;

namespace LawOfficeManagement.Application.Features.Clients.Queries.GetAllClients
{
    // الاستعلام لا يحتاج إلى أي معلمات في هذه الحالة
    public class GetAllClientsQuery : IRequest<List<ClientSummaryDto>>
    {
    }
}
