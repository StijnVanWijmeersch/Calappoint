using Calappoint.SharedKernel;
using MediatR;

namespace Calappoint.Application.Abstractions;

public interface IBaseCommand;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResult> : IRequest<Result<TResult>>, IBaseCommand;
