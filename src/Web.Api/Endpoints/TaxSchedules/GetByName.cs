using Application.TaxSchedules.GetByNameAndDate;

using MediatR;

using SharedKernel;

using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.TaxSchedules;

internal sealed class GetByNameAndDate : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/taxschedules/{municipalityName}", async (string municipalityName, DateTime dateUtc, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetTaxSchedulesByMunicipalityNameAndDateQuery(municipalityName, dateUtc);

            Result<TaxScheduleResponse> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.TaxSchedules);
    }
}
