using Application.Municipalities.CreateMunicipality;

using MediatR;

using SharedKernel;

using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Municipalities;

internal sealed class CreateMunicipality : IEndpoint
{
    public sealed class Request
    {
        public string Name { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/municipalities/", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreateMunicipalityCommand
            {
                Name = request.Name,
            };

            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Municipalities);
    }
}
