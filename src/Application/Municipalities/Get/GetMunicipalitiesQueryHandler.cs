using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;

using Domain.Municipalities;

using MediatR;

using Microsoft.EntityFrameworkCore;

using SharedKernel;

namespace Application.Municipalities.Get;

internal sealed class GetMunicipalitiesQueryHandler(IApplicationDbContext context) : IQueryHandler<GetMunicipalitiesQuery, List<MunicipalityResponse>>
{
    public async Task<Result<List<MunicipalityResponse>>> Handle(GetMunicipalitiesQuery query, CancellationToken cancellationToken)
    {
        var municipalities = await context.Municipalities
                                            .Include(m => m.TaxSchedules)
                                            .ToListAsync(cancellationToken);
        var allmunicipalities = municipalities.Select(u => new MunicipalityResponse()
        {
            Id = u.Id,
            Name = u.Name,
            TaxSchedules = u.TaxSchedules,
        }).ToList();

        return allmunicipalities.ToList();
    }
}
