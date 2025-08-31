namespace PizzApp.Domain.Entities
{
    public class Result
    {
        public bool IsSuccess { get; private set; } = true;
        public string Message { get; private set; } = string.Empty;
        public Guid? ResourceId { get; private set; }

        public Result() { }

        public void Success(string message, Guid? resourceId)
        {
            Message = message;
            ResourceId = resourceId;
        }

        public void Error(string message)
        {
            Message = message;
            IsSuccess = !IsSuccess;
        }
    }
}
