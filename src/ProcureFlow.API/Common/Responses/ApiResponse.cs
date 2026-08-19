namespace ProcureFlow.API.Common.Responses
{
    public sealed class ApiResponse<T>
    {
        public bool Success {  get; init; } 
        public string Message { get; init; } = string.Empty;
        public T? Data { get; init; }
        public IReadOnlyList<ApiError>? Errors { get; init; }

        public static ApiResponse<T> SuccessResponse(T Data, string message )
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = Data,
                Errors = null,
                Message = message
            };
        }

        public static ApiResponse<T> ErrorResponse(string message, IReadOnlyList<ApiError>? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Data = default,
                Errors = errors,
                Message = message
            };
        }

    }
}
