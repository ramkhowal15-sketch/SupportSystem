using System;

namespace Shared.Interfaces
{
    public interface IResult<T>
    {
        string Message { get; set; }
        bool IsSuccess { get; set; }
        int Code { get; set; }
        string? Token { get; set; }
        T? Data { get; set; }
        Exception? Exception { get; set; }
    }
}