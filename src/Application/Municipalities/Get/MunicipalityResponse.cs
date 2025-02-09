﻿using Domain.TaxSchedules;

namespace Application.Municipalities.GetById;

public sealed record MunicipalityResponse
{
    public Guid Id { get; init; }

    public required string Name { get; init; }

    public required List<TaxSchedule> TaxSchedules { get; init; }

}
