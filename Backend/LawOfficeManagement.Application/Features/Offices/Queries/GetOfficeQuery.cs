using LawOfficeManagement.Core.Entities;
using MediatR;

namespace LawOfficeManagement.Application.Features.Offices.Queries
{
    public class GetOfficeQuery : IRequest<Office>
    {
    }
}
