using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Municipalities.CreateMunicipality;

using Domain.Municipalities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SharedKernel;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Municipalities.GetByName;

internal sealed class CreateMunicipalityCommandHandler(IApplicationDbContext context, ILogger<CreateMunicipalityCommand> logger) : ICommandHandler<CreateMunicipalityCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateMunicipalityCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var municipality = await context.Municipalities.AsNoTracking()
                .Where(u => u.Name == command.Name)
                .SingleOrDefaultAsync(cancellationToken);

            if (municipality is not null)
            {
                logger.LogError("Municipality already exists with name: {MunicipalityName}", command.Name);
                return Result.Failure<Guid>(MunicipalityErrors.NameNotUnique);
            }

            var newMunicipality = new Municipality(command.Name);
            context.Municipalities.Add(newMunicipality);
            await context.SaveChangesAsync(cancellationToken);

            return newMunicipality.Id;
        }
        catch (DbUpdateException dbEx)
        {
            logger.LogError(dbEx, "Database update error saving Municipalities for Municipality Name: {MunicipalityName}", command.Name);
            return Result.Failure<Guid>(Error.Failure("Municipality.DatabaseUpdateFailure", "Database update error"));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error adding municipality with id: {MunicipalityName}", command.Name);
            return Result.Failure<Guid>(Error.Failure("Municipality.Failue", "Error creating Municipality"));
        }
    }
}
