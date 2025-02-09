using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.TaxSchedules.GetByNameAndDate;

using Domain.Municipalities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SharedKernel;

namespace Application.Municipalities.GetByName;

internal sealed class GetMunicipalityByNameQueryHandler(IApplicationDbContext context, ILogger<GetMunicipalityByNameQueryHandler> logger) : IQueryHandler<GetMunicipalityByNameQuery, MunicipalityResponse>
{
    public async Task<Result<MunicipalityResponse>> Handle(GetMunicipalityByNameQuery query, CancellationToken cancellationToken)
    {
        MunicipalityResponse? municipality = null;
        try
        {
            municipality = await context.Municipalities
                .Where(u => u.Name == query.MunicipalityName)
                .Select(u => new MunicipalityResponse
                {
                    Id = u.Id,
                    Name = u.Name,
                    TaxSchedules = u.TaxSchedules,
                })
                .SingleOrDefaultAsync(cancellationToken);

            if (municipality is null)
            {
                logger.LogInformation("Municipality not found by name: {MunicipalityName}", query.MunicipalityName);
                return Result.Failure<MunicipalityResponse>(MunicipalityErrors.NotFoundByName(query.MunicipalityName));
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting municipality by name: {MunicipalityName}", query.MunicipalityName);
            return Result.Failure<MunicipalityResponse>(Error.Failure("Municipalities.Failue", "Error getting tax schedules"));
        }

        return Result.Success(municipality);
    }
}
