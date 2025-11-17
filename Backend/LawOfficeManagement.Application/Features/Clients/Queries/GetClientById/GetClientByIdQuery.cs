using MediatR;

namespace LawOfficeManagement.Application.Features.Clients.Queries.GetClientById
{
    public class GetClientByIdQuery : IRequest<ClientDto?>
    {
        public int Id { get; set; }
    }
}
