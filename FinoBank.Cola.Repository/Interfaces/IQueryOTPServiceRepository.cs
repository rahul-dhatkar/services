using Contesto.V2.Core.Infrastructure.Data.Interfaces;
using FinoBank.Cola.Repository.DomainModels;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Interfaces
{
    public interface IQueryOTPServiceRepository:IQueryGenericRepository<string>
    {
        //GenerateOTPService
        Task<string> GenerateOTP(string serviceURL, GenerateOTPDomainModel model, GenerateRequestDataDomainModel generateRequestDataDomainModel);

        //VerifyOTPService
        Task<string> VerifyOTP(string serviceURL, VerifyOTPDomainModel model, VerifyOTPRequestDataDomainModel verifyOTPRequestDataDomainModel);

        //SendSMSService
        Task<string> SendSMS(string serviceURL, SMSRequestDomainModel model, SMSRequestDataDomainModel sMSRequestDataDomainModel);
    }
}