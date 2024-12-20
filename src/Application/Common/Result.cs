using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common;

public class Result<T>
{
    public T Value { get; }
    public bool IsSuccess { get; }
    public string ErrorMessage { get; }



    private Result(bool isSuccess, string errorMessage, T value)

    {
        Value = value;
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;

    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, string.Empty, value);
    }

    public static Result<T> Failure(string errorMessage)
    {
        return new Result<T>(false, errorMessage, default);
    }
}