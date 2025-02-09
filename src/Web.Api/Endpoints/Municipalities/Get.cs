using Application.Municipalities.Get;

using MediatR;

using SharedKernel;

using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Municipalities;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/municipalities", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new GetMunicipalitiesQuery();

            Result<List<MunicipalityResponse>> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Municipalities);
    }
}
