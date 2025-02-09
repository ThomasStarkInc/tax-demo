using Application.Municipalities.GetByName;

using MediatR;

using SharedKernel;

using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;

internal sealed class GetByName : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/municipalities/{municipalityName}", async (string municipalityName, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetMunicipalityByNameQuery(municipalityName);

            Result<MunicipalityResponse> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Municipalities);
    }
}
