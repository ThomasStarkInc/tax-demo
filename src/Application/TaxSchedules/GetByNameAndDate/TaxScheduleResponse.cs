using Domain.TaxSchedules;

namespace Application.TaxSchedules.GetByNameAndDate;

public sealed record TaxScheduleResponse
{
    public Guid MunicipalityId { get; init; }

    public required string MunicipalityName { get; init; }

    public required List<TaxSchedule> TaxSchedules { get; init; }
}
