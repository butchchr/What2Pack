namespace What2Pack.Api.Models
{
    /// <summary>
    /// I don't have access to my "Normal" libraries so this is a quick and dirty implementation of service result
    /// This should be replaced with a standard library or fully filled out.
    /// </summary>
    public class ServiceResult<T>
    {
        public bool IsError { get; set; }
        public string? ErrorMessage { get; set; }
        public T? Value { get; set; }
        public ServiceResult<T> SetValue(T value)
        {
            Value = value;
  
            return this;
        }
        public ServiceResult<T> AddError()
        {
            IsError = true;

            return this;
        }
        public ServiceResult<T> AddMessage(string message)
        {
            ErrorMessage = message;

            return this;
        }
    }
}
