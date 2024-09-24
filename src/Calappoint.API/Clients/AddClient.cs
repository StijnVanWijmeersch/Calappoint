using Calappoint.API.Endpoints;
using Calappoint.Application.Clients.AddClient;
using Calappoint.Domain.Common;
using Calappoint.SharedKernel;
using MediatR;

namespace Calappoint.API.Clients;

internal sealed class AddClient : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("clients", async (Request request, ISender sender) =>
        {
            var gp = Gender.TryParse(request.Gender, out Gender gender);

            Result<Guid> result = await sender.Send(new AddClientCommand(
                request.Id,
                request.FirstName,
                request.LastName,
                request.Email,
                request.PhoneNumber,
                request.Occupation,
                request.DateOfBirth,
                gp 
                ? gender 
                : Gender.NotSpecified
                ));

            return result.IsSuccess
                ? Results.Created($"clients/{result.Value}", result.Value)
                : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.Clients);
    }

    internal sealed class Request
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
        public string Occupation { get; init; }
        public DateTime DateOfBirth { get; init; }
        public string Gender { get; init; }

    }
}
