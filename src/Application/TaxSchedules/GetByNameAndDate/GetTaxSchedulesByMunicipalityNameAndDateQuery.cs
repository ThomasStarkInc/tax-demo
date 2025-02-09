using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Abstractions.Messaging;

namespace Application.TaxSchedules.GetByNameAndDate;

public sealed class GetTaxSchedulesByMunicipalityNameAndDateQuery : IQuery<TaxScheduleResponse>
{
    public string MunicipalityName { get; }
    public DateTime DateUtc { get; }

    public GetTaxSchedulesByMunicipalityNameAndDateQuery(string municipalityName, DateTime dateUtc)
    {
        MunicipalityName = municipalityName;
        DateUtc = dateUtc;
    }
}
