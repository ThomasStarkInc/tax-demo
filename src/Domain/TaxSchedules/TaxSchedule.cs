using Domain.Municipalities;

using SharedKernel;

namespace Domain.Schedules;

public sealed class TaxSchedule : Entity
{
    public Guid Id { get; private set; }
    public Guid MunicipalityId { get; private set; }
    public Municipality Municipality { get; private set; }
    public decimal TaxRate { get; private set; }
    public DateTime StartDateUtc { get; private set; }
    public DateTime EndDateUtc { get; private set; }
    public TaxFrequency Frequency { get; private set; }

    private TaxSchedule() { } // For EF Core

    public TaxSchedule(Guid municipalityId, decimal taxRate, DateTime startDateUtc, DateTime endDateUtc, TaxFrequency frequency)
    {
        if (startDateUtc > endDateUtc)
        {
            throw new ArgumentException("Start date must be before end date.");
        }

        if (taxRate < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(taxRate), "Tax rate must be non-negative.");
        }

        Id = Guid.NewGuid();
        MunicipalityId = municipalityId;
        TaxRate = taxRate;
        StartDateUtc = startDateUtc;
        EndDateUtc = endDateUtc;
        Frequency = frequency;
    }

    public bool AppliesToDate(DateTime date) => date >= StartDateUtc && date <= EndDateUtc;
}
