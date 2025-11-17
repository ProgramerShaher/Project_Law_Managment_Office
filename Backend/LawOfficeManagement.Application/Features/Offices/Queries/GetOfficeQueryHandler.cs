using LawOfficeManagement.Core.Entities ;
using LawOfficeManagement.Core.Interfaces;
using MediatR;
using AutoMapper;
using Microsoft.Extensions.Logging;
using LawOfficeManagement.Application.Features.Offices.Queries;

namespace LawOfficeManagement.Application.Features.Offices.Queries
{
    public class GetOfficeQueryHandler : IRequestHandler<GetOfficeQuery, Office>
    {
        private readonly IUnitOfWork _uow;

        public GetOfficeQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Office> Handle(GetOfficeQuery request, CancellationToken cancellationToken)
        {
            var office = await _uow.Repository<Office>().FirstOrDefaultAsync(o => true);
            if (office == null)
                throw new InvalidOperationException("المكتب غير موجود!");

            return office;
        }
    }
}
