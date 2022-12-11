namespace POS.Domain.ViewModels
{
    public abstract class BaseResponse
    {
        public int ResultCode { get; set; }
        public string ResultDescription { get; set; }

    }
}
