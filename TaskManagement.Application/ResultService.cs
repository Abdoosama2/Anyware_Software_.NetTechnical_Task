using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Application
{
    public class ResultService<T>
    {
        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; } = default!;

        public T? Data { get; set; }
        public static ResultService<T> Failure(string Error) => new() { IsSuccess = false, ErrorMessage = Error };

        public static ResultService<T> Success(T data) => new() { IsSuccess = true, Data = data };
    }
}
