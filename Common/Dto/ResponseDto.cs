using Common.Enums;

namespace Common.Dto
{
    public class ResponseDto<T>
    {
        public ResponseStatus Status { get; set; }
        public string?Message { get; set; }
        public T Data { get; set; }

        public ResponseDto(string message)
        {
            Status = ResponseStatus.Error;
            Message = message;
        }
        public ResponseDto() => Status = ResponseStatus.Error;

        #region Success
        public void SetSuccessResponse(string message = null)
        {
            Status = ResponseStatus.Success;
            Message = message;
        }
        public void SetSuccessResponse(T data, string message = null)
        {
            SetSuccessResponse(message);
            Data = data;
        }
        public ResponseDto<T> GetSuccessResponse(string message = null)
        {
            SetSuccessResponse(message);
            return this;
        }
        public ResponseDto<T> GetSuccessResponse(T responseData, string? message = null)
        {
            SetSuccessResponse(responseData, message);
            return this;
        }
        #endregion Success

        #region Error
        public void SetErrorResponse(string message = null)
        {
            Status = ResponseStatus.Error;
            Message = message;
        }
        public ResponseDto<T> GetErrorResponse(string message = null)
        {
            SetErrorResponse(message);
            return this;
        }
        #endregion Error
    }
}
