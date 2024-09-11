using Calappoint.SharedKernel;
using MediatR;

namespace Calappoint.Application.Abstractions;

public interface IQuery<TResult> : IRequest<Result<TResult>>;
