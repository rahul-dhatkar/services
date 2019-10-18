namespace FinoBank.Cola.Manager.ViewModels
{
    public class GenerateOTPResultViewModel
    {
        public string RequestID { get; set; }
        public string DisplayMessage { get; set; }
        public string ResponseMessage { get; set; }
        public string ResponseCode { get; set; }
        public string MessageType { get; set; }

        public string ResponseData
        {
            get; set;
        }

        public string MessageId { get; set; }
        public string SessionExpiryTime { get; set; }
        public string SessionId { get; set; }
        public string RouteID { get; set; }
        public int ErrorCode { get; set; }
    }

    public class GenerateResponseData
    {
        public string RequestId { get; set; }
        public string ResponseMessage { get; set; }
        public string DisplayMessage { get; set; }
        public string ResponseCode { get; set; }
    }
}