using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.TaxSchedules;

using SharedKernel;

namespace Domain.Municipalities;

public class Municipality : Entity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<TaxSchedule> TaxSchedules { get; set; } = new();

    private Municipality() { } // For EF Core

    public Municipality(string name)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public void AddTaxSchedule(TaxSchedule taxSchedule)
    {
        ArgumentNullException.ThrowIfNull(taxSchedule);

        TaxSchedules.Add(taxSchedule);
    }
}
