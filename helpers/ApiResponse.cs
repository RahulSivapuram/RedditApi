namespace RedditApi.helpers
{
    public class ApiResponse<T>
    {
        public string? status { get; set; }
        public T? data { get; set; }
        public string? error { get; set; }
    }
}
