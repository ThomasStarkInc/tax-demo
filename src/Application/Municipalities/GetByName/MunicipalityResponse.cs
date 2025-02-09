﻿using Domain.TaxSchedules;

namespace Application.Municipalities.GetByName;

public sealed record MunicipalityResponse
{
    public Guid Id { get; init; }

    public required string Name { get; init; }

    public required List<TaxSchedule> TaxSchedules { get; init; }
}
