using Contesto.V2.Core.Infrastructure.Data;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Queries
{
    internal class QueryOTPServiceRepository : QueryGenericRepository<string>,IQueryOTPServiceRepository
    {
        internal QueryOTPServiceRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<string> GenerateOTP(string serviceURL, GenerateOTPDomainModel model, GenerateRequestDataDomainModel modelDetail)
        {
            HttpResponseMessage response = null;
            model.RequestData = JsonConvert.SerializeObject(modelDetail, Formatting.Indented).Replace("\r", "").Replace("\n", "").Replace(" ", string.Empty);
            string data = "";
            using (var client = new HttpClient())
            {
                var uri = new Uri(serviceURL);
                var jsonRequest = JsonConvert.SerializeObject(model);
                var stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                response = await client.PostAsync(uri, stringContent).ConfigureAwait(false);
                HttpContent stream = response.Content;
                data = await stream.ReadAsStringAsync();
                JObject obj = JObject.Parse(data);
                var responseCode = (string)obj["ResponseCode"];
                if (responseCode == "0" && response.StatusCode == HttpStatusCode.OK)
                {
                    return data.ToString();
                }
                return response.StatusCode == HttpStatusCode.OK ? "true" : "false";
            }
        }

        public async Task<string> VerifyOTP(string serviceURL, VerifyOTPDomainModel model, VerifyOTPRequestDataDomainModel verifyOTPRequestDataDomainModel)
        {
            HttpResponseMessage response = null;
            model.RequestData = JsonConvert.SerializeObject(verifyOTPRequestDataDomainModel, Formatting.Indented).Replace("\r", "").Replace("\n", "").Replace(" ", string.Empty);
            string data = "";
            HttpClient client = new HttpClient();
            var uri = new Uri(serviceURL);
            var jsonRequest = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            response = await client.PostAsync(uri, stringContent).ConfigureAwait(false);
            HttpContent stream = response.Content;
            data = await stream.ReadAsStringAsync();
            JObject obj = JObject.Parse(data);
            var responseCode = (string)obj["ResponseCode"];
            var ResponseMessage = (string)obj["ResponseMessage"];
            if (responseCode == "0" && response.StatusCode == HttpStatusCode.OK)
            {
                return response.StatusCode == HttpStatusCode.OK ? "true" : "false";
            }
            return ResponseMessage;
        }

        public async Task<string> SendSMS(string serviceURL, SMSRequestDomainModel model, SMSRequestDataDomainModel sMSRequestDataDomainModel)
        {
            HttpResponseMessage response = null;
            model.RequestData = JsonConvert.SerializeObject(sMSRequestDataDomainModel, Formatting.Indented).Replace("\r", "").Replace("\n", "").Replace(" ", string.Empty).Replace("\\","")
            .Replace("ParamID", "@ID").Replace("MobileNum", "@MobileNo/EmailID").Replace("Reason","@Reason").Replace("CaseDecision", "@CaseDecision").Replace("Amount","@Amount");

                HttpClient client = new HttpClient();
                var uri = new Uri(serviceURL);
                var jsonRequest = JsonConvert.SerializeObject(model);
                var stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                response = await client.PostAsync(uri, stringContent).ConfigureAwait(false);
                string data = "";
                HttpContent stream = response.Content;
                data = await stream.ReadAsStringAsync();
                JObject obj = JObject.Parse(data);
                var responseCode = (string)obj["ResponseCode"];
                var ResponseMessage = (string)obj["ResponseMessage"];
                if (responseCode == "0" && response.StatusCode == HttpStatusCode.OK)
                {
                    return response.StatusCode == HttpStatusCode.OK ? "true" : "false";
                }
                return ResponseMessage;
            }

        private string GetTinyURL(string uniqueTransactionId)
        {
            //todo: convert Tiny URL
            return null;
        }
    }
}

