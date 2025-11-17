using LawOfficeManagement.Application.Features.PowerOfAttorneys.DTOs;
using MediatR;
using System.Collections.Generic;

namespace LawOfficeManagement.Application.Features.PowerOfAttorneys.Queries.GetAllPowerOfAttorney
{
    public class GetAllPowerOfAttorneyQuery : IRequest<IEnumerable<PowerOfAttorneyDto>> { }
}
