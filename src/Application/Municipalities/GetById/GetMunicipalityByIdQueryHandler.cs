using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Municipalities.GetByName;

using Domain.Municipalities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SharedKernel;

namespace Application.Municipalities.GetById;

internal sealed class GetMunicipalityByIdQueryHandler(IApplicationDbContext context, ILogger<GetMunicipalityByIdQueryHandler> logger) : IQueryHandler<GetMunicipalityByIdQuery, MunicipalityResponse>
{
    public async Task<Result<MunicipalityResponse>> Handle(GetMunicipalityByIdQuery query, CancellationToken cancellationToken)
    {
        MunicipalityResponse? municipality = null;
        try
        {
            municipality = await context.Municipalities
            .Where(u => u.Id == query.MunicipalityId)
            .Select(u => new MunicipalityResponse
            {
                Id = u.Id,
                Name = u.Name,
                TaxSchedules = u.TaxSchedules,
            })
            .SingleOrDefaultAsync(cancellationToken);

            if (municipality is null)
            {
                return Result.Failure<MunicipalityResponse>(MunicipalityErrors.NotFound(query.MunicipalityId));
            }

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting municipality by name: {MunicipalityName}", query.MunicipalityId);
            return Result.Failure<MunicipalityResponse>(Error.Failure("Municipalities.Failue", "Error getting tax schedules"));
        }

        return Result.Success(municipality);
    }
}
