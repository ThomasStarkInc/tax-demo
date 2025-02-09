using SharedKernel;

namespace Domain.Users;

public static class MunicipalityErrors
{
    public static Error NotFound(Guid municipalityId) => Error.NotFound(
        "Municipalities.NotFound",
        $"The municipality with the Id = '{municipalityId}' was not found");

    public static Error Unauthorized() => Error.Failure(
        "Municipalities.Unauthorized",
        "You are not authorized to perform this action.");

    public static readonly Error NotFoundByName = Error.NotFound(
        "Municipalities.NotFoundByName",
        "The municipality with the specified name was not found");

    public static readonly Error NameNotUnique = Error.Conflict(
        "Municipalities.NameNotUnique ",
        "The provided name is not unique");
}
