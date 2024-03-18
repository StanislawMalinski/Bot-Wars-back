namespace FileGatherer
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public String Message { get; set; }
    }
}
