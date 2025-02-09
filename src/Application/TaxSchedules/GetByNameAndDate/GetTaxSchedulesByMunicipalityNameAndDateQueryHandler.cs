using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.TaxSchedules.GetByNameAndDate;

using Domain.Municipalities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SharedKernel;

namespace Application.Municipalities.GetByName;

internal sealed class GetTaxSchedulesByMunicipalityNameAndDateQueryHandler(IApplicationDbContext context, ILogger<GetTaxSchedulesByMunicipalityNameAndDateQueryHandler> logger) : IQueryHandler<GetTaxSchedulesByMunicipalityNameAndDateQuery, TaxScheduleResponse>
{
    public async Task<Result<TaxScheduleResponse>> Handle(GetTaxSchedulesByMunicipalityNameAndDateQuery query, CancellationToken cancellationToken)
    {
        TaxScheduleResponse response = null;

        try
        {
            var municipality = await context.Municipalities.AsNoTracking()
                .Include(u => u.TaxSchedules)
                .Where(u => u.Name == query.MunicipalityName)
                .SingleOrDefaultAsync(cancellationToken);

            if (municipality is null)
            {
                logger.LogInformation("Municipality not found by name: {MunicipalityName}", query.MunicipalityName);
                return Result.Failure<TaxScheduleResponse>(TaxScheduleErrors.NotFoundByMunicipalityName(query.MunicipalityName));
            }

            var taxSchedules = municipality.TaxSchedules
                                            .Where(t => t.StartDateUtc <= query.DateUtc && t.EndDateUtc >= query.DateUtc)
                                            .ToList();

            response = new TaxScheduleResponse
            {
                MunicipalityId = municipality.Id,
                MunicipalityName = municipality.Name,
                TaxSchedules = taxSchedules
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting municipality by name: {MunicipalityName}", query.MunicipalityName);
            return Result.Failure<TaxScheduleResponse>(Error.Failure("Municipalities.Failue", "Error getting tax schedules"));
        }

        return Result.Success(response);
    }
}
