using System.Diagnostics.CodeAnalysis;

namespace Result;

public class Result<T>
{
    // We don't expose these publicly any more
    private readonly T? _value;
    private readonly Exception? _error;

    public T? Value
    {
        get => _value;
    }

    public Exception? Error
    {
        get => _error;
    }
    
    // Same constructors
    private Result(T value)
    {
        IsSuccess = true;
        _value = value;
        _error = null;
    }

    private Result(Exception error)
    {
        IsSuccess = false;
        _value = default;
        _error = error;
    }

    [MemberNotNullWhen(true, nameof(_value), nameof(Value))]
    [MemberNotNullWhen(false, nameof(_error), nameof(Error))]
    public bool IsSuccess { get; }

    // This Method takes two Func<T>, one for the success case and one for the error case
    public Result<TReturn> Switch<TReturn>(
        Func<T, TReturn> onSuccess,
        Func<Exception, Exception> onFailure)
    {
        if (IsSuccess)
        {
            // If this result has a value, run the success method,
            // which returns a different value, and then we create a
            // Result<TReturn> from it (implicitly)
            var result = onSuccess(_value);
            return result;
        }
        else
        {
            {
                // If this result is an error, run the error method
                // to allow the user to manipulate/inspect the error.
                // We then create a new Result<TReturn> result object
                // from the error it returns
                var err = onFailure(_error);
                return Result<TReturn>.Fail(err);
            }
        }
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Fail(Exception error) => new(error);

    public static implicit operator Result<T>(T value) => Success(value);
}