using Application.Abstractions.Messaging;

namespace Application.Municipalities.GetById;

public sealed record GetMunicipalityByIdQuery(Guid MunicipalityId) : IQuery<MunicipalityResponse>;
