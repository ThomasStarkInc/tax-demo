using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Abstractions.Messaging;

using Domain.TaxSchedules;

namespace Application.TaxSchedules.Create;

public sealed class CreateTaxScheduleForMunicipalityCommand : ICommand<Guid>
{
    public Guid MunicipalityId { get; set; }
    public decimal TaxRate { get; set; }
    public DateTime StartDateUtc { get; set; }
    public DateTime EndDateUtc { get; set; }
    public TaxFrequency Frequency { get; set; }
}
