namespace LawOfficeManagement.Core.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string Error { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new List<string>();

        public static Result<T> Success(T data) => new() { IsSuccess = true, Data = data };
        public static Result<T> Failure(string error) => new() { IsSuccess = false, Error = error };
        public static Result<T> Failure(List<string> errors) => new() { IsSuccess = false, Errors = errors };
    }

    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; } = string.Empty;

        public static Result Success() => new() { IsSuccess = true };
        public static Result Failure(string error) => new() { IsSuccess = false, Error = error };
    }
}
