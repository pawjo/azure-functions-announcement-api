namespace AnnouncementsApiFunctionsApp.Dtos
{
    public class Result<T>
    {
        public Result(T response)
        {
            Response = response;
        }

        public Result(int errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public T Response { get; set; }


        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public bool IsError { get => ErrorCode != 0; }
    }
}
