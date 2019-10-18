namespace FinoBank.Cola.Repository.DomainModels
{
    public class SMSRequestDomainModel
    {
        public string RequestId { get; set; }
        public int MethodId { get { return 1173; } }
        public string TellerID { get { return ""; } }
        public string SessionId { get { return ""; } }
        public bool IsEncrypt { get { return false; } }

        public string RequestData
        {
            get; set;
        }
    }

    public class SMSRequestDataDomainModel
    {
        public string url { get { return null; } }
        public string MethodId { get { return "3"; } }
        public string UserID { get { return CustomerMobileNo; } }
        // public string CustomerMobileNo { get; set; }
        public string CustomerMobileNo { get; set; }
        public string EventId { get; set; }
        public ParamDomainModel NotifyParam { get; set; }

        public string PAN_END { get { return null; } }
    }

    public class ParamDomainModel
    {
        public string TemplateId { get; set; }
        public string @ParamID { get; set; }
        public string @MobileNum { get;  set; }
        public string @Reason { get; set; }
        public string @CaseDecision { get; set; }
        public string @Amount { get; set; }
    }
}

 