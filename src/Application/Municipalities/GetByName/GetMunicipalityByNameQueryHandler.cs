using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;

using Domain.Municipalities;
using Domain.Users;

using Microsoft.EntityFrameworkCore;

using SharedKernel;

namespace Application.Municipalities.GetByName;

internal sealed class GetMunicipalityByNameQueryHandler(IApplicationDbContext context) : IQueryHandler<GetMunicipalityByNameQuery, MunicipalityResponse>
{
    public async Task<Result<MunicipalityResponse>> Handle(GetMunicipalityByNameQuery query, CancellationToken cancellationToken)
    {
        MunicipalityResponse? user = await context.Municipalities
            .Where(u => u.Name == query.MunicipalityName)
            .Select(u => new MunicipalityResponse
            {
                Id = u.Id,
                Name = u.Name,
                TaxSchedules = u.TaxSchedules,
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result.Failure<MunicipalityResponse>(MunicipalityErrors.NotFoundByName(query.MunicipalityName));
        }

        return user;
    }
}
