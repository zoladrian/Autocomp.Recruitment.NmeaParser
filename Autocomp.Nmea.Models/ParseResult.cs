namespace Autocomp.Nmea.Models
{
    public class ParseResult<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}