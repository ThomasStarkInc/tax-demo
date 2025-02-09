using Application.Municipalities.GetById;

using MediatR;

using SharedKernel;

using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Municipalities;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/municipalities/{municipalityId:guid}", async (Guid municipalityId, ISender sender, CancellationToken cancellationToken) =>
        {
            // TODO: Add validation, santization and error handling
            var query = new GetMunicipalityByIdQuery(municipalityId);

            Result<MunicipalityResponse> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Municipalities);
    }
}
