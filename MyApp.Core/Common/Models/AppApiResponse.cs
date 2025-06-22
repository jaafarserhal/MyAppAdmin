using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Core.Common.Models
{
    public class AppApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static AppApiResponse<T> Success(T data, string message = "Success")
        {
            return new AppApiResponse<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message
            };
        }

        public static AppApiResponse<T> Failure(string message)
        {
            return new AppApiResponse<T>
            {
                IsSuccess = false,
                Data = default,
                Message = message
            };
        }
    }

}