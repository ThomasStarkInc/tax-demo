using Application.Abstractions.Messaging;

namespace Application.Municipalities.Get;

public sealed record GetMunicipalitiesQuery() : IQuery<List<MunicipalityResponse>>;
