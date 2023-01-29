namespace POS.Domain.ViewModels
{
    public abstract class BaseResponse
    {
        public int ResultCode { get; set; }
        public string ResultDescription { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public int ID { get; set; }

    }
}
