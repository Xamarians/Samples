namespace ChatDemo.Services
{
    class Result
    {
        public bool IsSuccess
        {
            get
            {
                return Status == ResultStatus.Success;
            }
        }

        public string Message { get; set; }

        public ResultStatus Status { get; set; }

        public Result()
        { }

        public Result(string message, ResultStatus status = ResultStatus.Error)
        {
            Message = message;
            Status = status;
        }

        public static Result<T> Create<T>(T data)
        {
            return new Result<T>(data);
        }

        public static Result<T> Create<T>(string message, ResultStatus status)
        {
            return new Result<T>() { Message = message, Status = status };
        }

        public static Result Create(string message, ResultStatus status)
        {
            return new Result(message, status);
        }

    }

    class Result<TData> : Result
    {
        public TData Data { get; set; }

        public Result()
        {

        }

        public Result(TData data)
        {
            Data = data;
            Status = ResultStatus.Success;
        }

        public Result(string message, ResultStatus status = ResultStatus.Error)
        {
            Message = message;
            Status = status;
        }

    }

    class EmptyResult : Result<string>
    {

    }

    public enum ResultStatus
    {
        Error, NetworkError, Success
    }
}



