using Domain.Municipalities;
using Domain.Schedules;

using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Municipality> Municipalities { get; }
    DbSet<TaxSchedule> TaxSchedules { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
