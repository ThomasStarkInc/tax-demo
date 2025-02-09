using Application.Municipalities.GetByName;

using MediatR;

using SharedKernel;

using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Municipalities;

internal sealed class GetByName : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/municipalities/{municipalityName}", async (string municipalityName, ISender sender, CancellationToken cancellationToken) =>
        {
            // TODO: Add validation, santization and error handling
            var query = new GetMunicipalityByNameQuery(municipalityName);

            Result<MunicipalityResponse> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Municipalities);
    }
}
