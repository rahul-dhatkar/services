namespace FinoBank.Cola.Manager.ViewModels
{
    public class VerifyOTPViewModel
    {
        public string RequestId { get; set; }
        public string TellerID { get; set; }
        public string CustomerMobileNo { get; set; }
        public string OtpPin { get; set; }
        public string OtpParam { get { return "{ }"; } }
    }
}