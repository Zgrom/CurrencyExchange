namespace ApplicationServices.JsonDeserializeClasses
{
    public sealed class FixerErrorJsonResult
    {
        public bool success { get; set; }
        public Error error { get; set; }
        
        public class Error
        {
            public int code { get; set; }
            public string info { get; set; }
        }
    }
}