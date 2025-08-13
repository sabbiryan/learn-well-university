using Microsoft.AspNetCore.Http;

namespace LearnWellUniversity.Application.Models.Common
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public T? Data { get; set; }
        public ApiError? Error { get; set; }

        public ApiResponse() { }

        public ApiResponse(T data, int statusCode = StatusCodes.Status200OK)
        {
            Data = data;
            StatusCode = statusCode;
            IsSuccess = true;
        }

        public ApiResponse(string errorMessage, int statusCode)
        {
            Error = new ApiError(errorMessage);
            StatusCode = statusCode;
            IsSuccess = false;
        }

        public ApiResponse(ApiError error, int statusCode)
        {
            Error = error;
            StatusCode = statusCode;
            IsSuccess = false;
        }
    }

}
