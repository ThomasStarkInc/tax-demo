using Application.TaxSchedules.Create;

using Domain.TaxSchedules;

using MediatR;

using SharedKernel;

using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.TaxSchedules;


internal sealed class Create : IEndpoint
{
    public sealed class Request
    {
        public Guid MunicipalityId { get; set; }
        public decimal TaxRate { get; set; }
        public DateTime StartDateUtc { get; set; }
        public DateTime EndDateUtc { get; set; }
        public TaxFrequency Frequency { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/taxschedules/", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreateTaxScheduleForMunicipalityCommand
            {
                MunicipalityId = request.MunicipalityId,
                TaxRate = request.TaxRate,
                StartDateUtc = request.StartDateUtc,
                EndDateUtc = request.EndDateUtc,
                Frequency = request.Frequency,
            };

            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.TaxSchedules);
    }
}
