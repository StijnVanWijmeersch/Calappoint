using Calappoint.Application.Abstractions;
using Calappoint.Application.Abstractions.Data;
using Calappoint.Domain.Users;
using Calappoint.SharedKernel;

namespace Calappoint.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = User.Create(request.FirstName, request.LastName, request.Email, passwordHash, request.PhoneNumber);

        userRepository.Insert(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
