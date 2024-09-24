using Calappoint.Application.Abstractions;
using Calappoint.Application.Abstractions.Data;
using Calappoint.Application.Abstractions.Identity;
using Calappoint.Domain.Users;
using Calappoint.SharedKernel;

namespace Calappoint.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    IIdentityProviderService identityProviderService,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        Result<string> result = await identityProviderService.RegisterUserAsync(
            new UserModel(request.Email, request.Password),
            cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }


        var user = User.Create(
            request.Email,
            result.Value);

        userRepository.Insert(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
