namespace FinoBank.Cola.Manager.ViewModels
{
    //public class GenerateOTPViewModel
    //{
    //    public string RequestId { get; set; }
    //    public int MethodId { get; set; }
    //    public string TellerID { get; set; }
    //    public string SessionId { get; set; }
    //    public bool IsEncrypt { get; set; }
    //    public GenerateRequestData RequestData
    //    {
    //        get; set;
    //    }
    //}

    //public class GenerateRequestData
    //{
    //    public string MethodId { get; set; }
    //    public string CustomerMobileNo { get; set; }
    //    public int MessageId { get; set; }
    //    public string OtpParam { get { return "{ }"; } }
    //}

    public class GenerateOTPViewModel
    {
        public string TellerID { get; set; }
        public string CustomerMobileNo { get; set; }
        public string OtpParam { get { return "{ }"; } }
    }
}