using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.TaxSchedules.Create;
using Application.TaxSchedules.GetByNameAndDate;

using Domain.Municipalities;
using Domain.TaxSchedules;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SharedKernel;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Municipalities.GetByName;

internal sealed class CreateTaxScheduleForMunicipalityQueryHandler(IApplicationDbContext context, ILogger<CreateTaxScheduleForMunicipalityQueryHandler> logger) : ICommandHandler<CreateTaxScheduleForMunicipalityCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateTaxScheduleForMunicipalityCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var municipality = await context.Municipalities.AsNoTracking()
                .Include(u => u.TaxSchedules)
                .Where(u => u.Id == command.MunicipalityId)
                .SingleOrDefaultAsync(cancellationToken);

            if (municipality is null)
            {
                logger.LogInformation("Municipality not found by name: {MunicipalityId}", command.MunicipalityId);
                return Result.Failure<Guid>(TaxScheduleErrors.NotFound(command.MunicipalityId));
            }

            var taxSchedule = new TaxSchedule(
                command.MunicipalityId,
                command.TaxRate,
                command.StartDateUtc,
                command.EndDateUtc,
                command.Frequency
            );

            context.TaxSchedules.Add(taxSchedule);
            await context.SaveChangesAsync(cancellationToken);

            return taxSchedule.Id;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting municipality by id: {MunicipalityId}", command.MunicipalityId);
            return Result.Failure<Guid>(Error.Failure("Municipalities.Failue", "Error creating Tax Schedule"));
        }
    }
}
