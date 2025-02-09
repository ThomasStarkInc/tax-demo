using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;

using Domain.Municipalities;
using Domain.Users;

using Microsoft.EntityFrameworkCore;

using SharedKernel;

namespace Application.Municipalities.GetById;

internal sealed class GetMunicipalityByIdQueryHandler(IApplicationDbContext context) : IQueryHandler<GetMunicipalityByIdQuery, MunicipalityResponse>
{
    public async Task<Result<MunicipalityResponse>> Handle(GetMunicipalityByIdQuery query, CancellationToken cancellationToken)
    {
        MunicipalityResponse? user = await context.Municipalities
            .Where(u => u.Id == query.MunicipalityId)
            .Select(u => new MunicipalityResponse
            {
                Id = u.Id,
                Name = u.Name,
                TaxSchedules = u.TaxSchedules,
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result.Failure<MunicipalityResponse>(MunicipalityErrors.NotFound(query.MunicipalityId));
        }

        return user;
    }
}
