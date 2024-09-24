using Calappoint.Application.Abstractions;
using Calappoint.Application.Abstractions.Data;
using Calappoint.Domain.Clients;
using Calappoint.SharedKernel;

namespace Calappoint.Application.Clients.AddClient;

internal sealed class AddClientCommandHandler(
    IClientRepository clientRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<AddClientCommand, Guid>
{
    public async Task<Result<Guid>> Handle(AddClientCommand request, CancellationToken cancellationToken)
    {
        var client = Client.Create(
            request.Id,
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            request.Occupation,
            request.DateOfBirth,
            request.Gender);

        clientRepository.Insert(client);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return client.Id;
    }
}
