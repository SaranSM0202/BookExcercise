namespace Excercise2.Repository
{
    /// <summary>
    /// Class to handle all kind of response 
    /// </summary>
    public class Response
    {
        public int StatusCode { get; set; }
        public int item1 { get; set; }
        public string item2 { get; set; }
        public string Message { get; set; }
        public bool IsError { get; set; }
        public object Data { get; set; }
    }

    /// <summary>
    /// Response Status
    /// </summary>
    public enum Status
    {
        Success = 0,
        Failed = 1,
        Exception = 2,
    }

}
