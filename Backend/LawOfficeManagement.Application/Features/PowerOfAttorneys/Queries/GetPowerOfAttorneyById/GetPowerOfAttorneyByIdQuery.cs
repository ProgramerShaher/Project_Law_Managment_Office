using LawOfficeManagement.Application.Features.PowerOfAttorneys.DTOs;
using MediatR;

namespace LawOfficeManagement.Application.Features.PowerOfAttorneys.Queries.GetPowerOfAttorneyById
{
    public class GetPowerOfAttorneyByIdQuery : IRequest<PowerOfAttorneyDto>
    {
        public int Id { get; set; }
        public GetPowerOfAttorneyByIdQuery(int id) => Id = id;
    }
}
