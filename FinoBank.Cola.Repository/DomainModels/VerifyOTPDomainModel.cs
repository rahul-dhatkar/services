namespace FinoBank.Cola.Repository.DomainModels
{
    public class VerifyOTPDomainModel
    {
        public string RequestId { get; set; }
        public int MethodId { get; set; }
        public string TellerID { get; set; }
        public string SessionId { get; set; }
        public bool IsEncrypt { get; set; }

        public string RequestData
        {
            get; set;
        }
    }

    public class VerifyOTPRequestDataDomainModel
    {
        public string MethodId { get; set; }
        public string RequestId { get; set; }
        public string CustomerMobileNo { get; set; }
        public string OtpPin { get; set; }
        public int MessageId { get; set; }
        public string OtpParam { get { return "{ }"; } }
    }
}