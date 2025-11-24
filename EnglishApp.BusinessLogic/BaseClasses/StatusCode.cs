using EnglishApp.ApplicationCore.Enums;

namespace EnglishApp.BusinessLogic.BaseClasses
{
    public class StatusCode
    {
        public StatusCodeEnum Code { get; set; }
        public string Message { get; set; } = string.Empty;
        public Guid IdReturn { get; set; }
        public string? StringReturn { get; set; }
        public string? URLReturn { get; set; }

        public StatusCode()
        {
            Code = StatusCodeEnum.Unchanged;
            Message = "Chưa thay đổi";
        }

        public void SetSuccess(string? message = "Thành công")
        {
            Code = StatusCodeEnum.Success;
            Message = message;
        }
        public void SetBadRequest(string? message = "Lỗi dữ liệu đầu vào")
        {
            Code = StatusCodeEnum.BadRequest;
            Message = message;
        }
        public void SetInternalError(string? message = "Lỗi trong quá trình xử lý")
        {
            Code = StatusCodeEnum.Error;
            Message = message;
            //URLReturn = "/Home/Error";
            URLReturn = "/Error/Error";
        }
    }
}
