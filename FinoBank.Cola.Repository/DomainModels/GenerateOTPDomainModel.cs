namespace FinoBank.Cola.Repository.DomainModels
{
    public class GenerateOTPDomainModel
    {
        public string RequestId { get; set; }
        public int MethodId { get; set; }
        public string TellerID { get; set; }

        public string SessionId
        {
            get;
            set;
        }

        public bool IsEncrypt { get; set; }

        public string RequestData
        {
            get; set;
        }
    }

    public class GenerateRequestDataDomainModel
    {
        public string MethodId { get; set; }
        public string CustomerMobileNo { get; set; }
        public int MessageId { get; set; }
        public string OtpParam { get { return "{}"; } }
    }
}