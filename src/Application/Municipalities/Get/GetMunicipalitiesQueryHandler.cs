using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Municipalities.GetById;

using Domain.Municipalities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SharedKernel;

namespace Application.Municipalities.Get;

internal sealed class GetMunicipalitiesQueryHandler(IApplicationDbContext context, ILogger<GetMunicipalitiesQueryHandler> logger) : IQueryHandler<GetMunicipalitiesQuery, List<MunicipalityResponse>>
{
    public async Task<Result<List<MunicipalityResponse>>> Handle(GetMunicipalitiesQuery query, CancellationToken cancellationToken)
    {
        List<MunicipalityResponse> allMunicipalities;

        try
        {
            var municipalities = await context.Municipalities
                                .Include(m => m.TaxSchedules)
                                .ToListAsync(cancellationToken);
            allMunicipalities = municipalities.Select(u => new MunicipalityResponse()
            {
                Id = u.Id,
                Name = u.Name,
                TaxSchedules = u.TaxSchedules,
            }).ToList();

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting municipalities");
            return Result.Failure<List<MunicipalityResponse>>(Error.Failure("Municipalities.Failue", "Error getting municipalities"));
        }

        return Result.Success(allMunicipalities);
    }
}
