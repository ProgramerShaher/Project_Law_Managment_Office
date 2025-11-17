using LawOfficeManagement.Application.Features.Lawyers.DTOs;
using MediatR;

namespace LawOfficeManagement.Application.Features.Lawyers.Queries.GetLawyerById
{
    public class GetLawyerByIdQuery : IRequest<LawyerDto?>
    {
        public int Id { get; set; }
    }
}
