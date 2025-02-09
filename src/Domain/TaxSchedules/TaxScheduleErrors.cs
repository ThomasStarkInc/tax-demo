using SharedKernel;

namespace Domain.Municipalities;

public static class TaxScheduleErrors
{
    public static Error NotFound(Guid municipalityId) => Error.NotFound(
        "Municipalities.NotFound",
        $"The municipality with the Id = '{municipalityId}' was not found");

    public static Error Unauthorized() => Error.Failure(
        "Municipalities.Unauthorized",
        "You are not authorized to perform this action.");

    public static Error NotFoundByMunicipalityName(string municipalityName) => Error.NotFound(
        "Municipalities.NotFound",
        $"The municipality with the Municipality name = '{municipalityName}' was not found");

    public static readonly Error NameNotUnique = Error.Conflict(
        "Municipalities.NameNotUnique ",
        "The provided name is not unique");
}
