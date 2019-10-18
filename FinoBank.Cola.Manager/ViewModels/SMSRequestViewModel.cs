namespace FinoBank.Cola.Manager.ViewModels
{
    public class SMSRequestViewModel
    {
        public string RequestId { get; set; }
        public int MethodId { get { return 1173 ; } }
        public string TellerID { get { return ""; } }
        public string SessionId { get { return ""; } }
        public bool IsEncrypt { get { return false; } }

        public string RequestData
        {
            get; set;
        }
    }

    public class SMSRequestDataViewModel
    {
        public string url { get { return null; } }
        public string MethodId { get { return "3"; } }
        public string UserID { get; set; }
        public string CustomerMobileNo { get; set; }
        public string EventId { get { return "99"; } }
        public ParamViewModel NotifyParam
        {
            get;
            set;
        }
        public string PAN_END { get { return null; } }
    }

    public class ParamViewModel
    {
        public string TemplateId { get; set; }
        public string @ID { get; set; }
        public string @MobileNo { get; set; }
        public string @Reason { get; set; }

    }
}