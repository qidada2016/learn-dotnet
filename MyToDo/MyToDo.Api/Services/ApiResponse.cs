namespace MyToDo.Api.Services
{
    public class ApiResponse
    {

        public ApiResponse(bool status, object result)
        {
            Status = status;
            Result = result;
        }

        public ApiResponse(string message, bool status = false)
        {
            Message = message;
            Status = status;
        }

        public string Message { get; set; }
        public bool Status { get; set; }
        public object Result { get; set; }
    }
}
