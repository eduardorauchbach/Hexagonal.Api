using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hexagonal.Common.DTO
{
    public class Result<T> where T : class
    {
        public T Value { get; }
        public bool IsSuccess { get; }
        public string? Message { get; }
        public HttpStatusCode StatusCode { get; }

        protected Result(T value, bool isSuccess, string? message, HttpStatusCode httpStatusCode)
        {
            Value = value;
            IsSuccess = isSuccess;
            Message = message;
            StatusCode = httpStatusCode;
        }

        public static Result<T> Success<T>(T value) where T : class
        {
            return new Result<T>(value, true, null, HttpStatusCode.OK);
        }

        public static Result<object> Success(object value)
        {
            return new Result<object>(value, true, null, HttpStatusCode.OK);
        }

        public static implicit operator Result<T>(Result result)
        {
            return new Result<T>(null, result.IsSuccess, result.Message, result.StatusCode);
        }

        public static implicit operator ActionResult(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(result.Value);
            }
            else
            {
                return new ObjectResult(new { result.Message })
                {
                    StatusCode = (int)result.StatusCode
                };
            }
        }
    }

    public class Result : Result<object>
    {
        public Result(bool isSuccess, string message, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError)
            : base(null, isSuccess, message, httpStatusCode)
        {
        }

        public static Result Failure(string message, HttpStatusCode httpStatusCode)
        {
            return new Result(false, message, httpStatusCode);
        }
    }
}
