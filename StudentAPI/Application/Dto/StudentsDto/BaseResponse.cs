namespace StudentAPI.Application.Dto.StudentDto
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public object? Error { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public static BaseResponse<T> Ok(T data, string message = "Request completed successfully")
        {
            return new BaseResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static BaseResponse<T> Fail(string message, object? error = null)
        {
            return new BaseResponse<T>
            {
                Success = false,
                Message = message,
                Error = error
            };
        }
    }

}
