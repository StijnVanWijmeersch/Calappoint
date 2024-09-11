namespace Calappoint.SharedKernel;

public sealed record ValidationError : Error
{
    public Error[] Errors { get; }

    public ValidationError(Error[] errors) : base("Validation", "One or more validation failures occurred.", ErrorType.Validation)
    {
        Errors = errors;
    }
}
