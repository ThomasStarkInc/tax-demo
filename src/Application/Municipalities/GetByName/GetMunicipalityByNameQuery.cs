using Application.Abstractions.Messaging;

namespace Application.Municipalities.GetByName;

public sealed record GetMunicipalityByNameQuery(string MunicipalityName) : IQuery<MunicipalityResponse>;
