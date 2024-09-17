using Calappoint.API.Endpoints;
using Calappoint.Application.Users.RegisterUser;
using Calappoint.SharedKernel;
using MediatR;

namespace Calappoint.API.Users;

internal sealed class RegisterUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/register", async (Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new RegisterUserCommand(
                request.Email,
                request.Password,
                request.FirstName,
                request.LastName));

            return result.IsSuccess
                ? Results.Created($"users/{result.Value}", result.Value)
                : Results.BadRequest(result.Error);
        })
        .AllowAnonymous()
        .WithTags(Tags.Users);
    }

    internal sealed class Request
    {
        public string Email { get; init; }
        public string Password { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
    }
}
