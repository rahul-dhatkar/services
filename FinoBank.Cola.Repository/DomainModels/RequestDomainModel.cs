namespace FinoBank.Cola.Repository.DomainModels
{
    public class RequestDomainModel
    {
        public string RequestId { get; set; }
        public int MethodId { get; set; }
        public string TellerID { get; set; }
        public string SessionId { get; set; }
        public bool IsEncrypt { get; set; }
        //public RequestData RequestData
        //{
        //    get; set;
        //}
    }
}