namespace Core.Identity.Models;
public class Result
{
	public bool Succeeded { get; }
	public IEnumerable<string> Errors { get; }
	internal Result(bool succeeded, IEnumerable<string> errors)
	{
		Succeeded = succeeded;
		Errors = errors;
	}

	public static Result Success()
	{
		return new Result(true, []);
	}

	public static Result Failure(IEnumerable<string> errors)
	{
		return new Result(false, errors);
	}
}

public class Result<T> : Result
{
	public T Data { get; }

	internal Result(bool succeeded, IEnumerable<string> errors, T data) : base(succeeded, errors)
	{
		Data = data;
	}

	public static Result<T> Success(T data)
	{
		return new Result<T>(true, [], data);
	}

	public static new Result<T> Failure(IEnumerable<string> errors)
	{
		return new Result<T>(false, errors, default!);
	}
}
