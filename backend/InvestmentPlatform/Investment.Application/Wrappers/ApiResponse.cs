namespace Investment.Application.Wrappers
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public ErrorDetail? Error { get; set; }

        public static ApiResponse<T> SuccessResponse(T data, string message)
        {
            return new ApiResponse<T> { Success = true, Data = data, Message = message };
        }

        public static ApiResponse<T> FailResponse(T? data, string message, int code = 500, string type = "ServerError", List<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Data = data,
                Message = message,
                Error = new ErrorDetail
                {
                    Code = code,
                    Type = type,
                    Messages = errors ?? new List<string> { message }
                }
            };
        }
    }

    public class ErrorDetail
    {
        public int Code { get; set; }
        public string Type { get; set; } = "";
        public List<string> Messages { get; set; } = new();
    }
}