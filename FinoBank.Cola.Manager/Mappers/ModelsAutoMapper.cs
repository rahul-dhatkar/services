using Contesto.V2.Core.Common.Manager.Base;
using FinoBank.Cola.Manager.ViewModels;
using FinoBank.Cola.Repository.DomainModels;

namespace FinoBank.Cola.Manager.Mappers
{
    /// <summary>
    /// Master models auto mapper configuration
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Manager.Base.BaseAutoMapper" />
    /// <seealso cref="BaseAutoMapper" />
    public class ModelsAutoMapper : BaseAutoMapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelsAutoMapper" /> class.
        /// </summary>
        public ModelsAutoMapper()
        {
            CreateMap<SampleViewModel, SampleDomainModel>().ReverseMap()
               .ForMember(dest => dest.Id, cfg => cfg.MapFrom(src => src.Id))
               .ForMember(dest => dest.TypeId, cfg => cfg.MapFrom(src => src.TypeId))
               .ForMember(dest => dest.Name, cfg => cfg.MapFrom(src => src.Name))
               .ForMember(dest => dest.Description, cfg => cfg.MapFrom(src => src.Description));

            CreateMap<MerchantViewModel, MerchantDomainModel>().ReverseMap()
              .ForMember(dest => dest.Id, cfg => cfg.MapFrom(src => src.Id))
              .ForMember(dest => dest.RefCode, cfg => cfg.MapFrom(src => src.RefCode))
              .ForMember(dest => dest.Name, cfg => cfg.MapFrom(src => src.Name))
              .ForMember(dest => dest.MerchantTypeId, cfg => cfg.MapFrom(src => src.MerchantTypeId))
              .ForMember(dest => dest.AddressLine1, cfg => cfg.MapFrom(src => src.AddressLine1))
              .ForMember(dest => dest.AddressLine2, cfg => cfg.MapFrom(src => src.AddressLine2))
              .ForMember(dest => dest.District, cfg => cfg.MapFrom(src => src.District))
              .ForMember(dest => dest.City, cfg => cfg.MapFrom(src => src.City))
              .ForMember(dest => dest.State, cfg => cfg.MapFrom(src => src.State))
              .ForMember(dest => dest.Country, cfg => cfg.MapFrom(src => src.Country))
              .ForMember(dest => dest.PinCode, cfg => cfg.MapFrom(src => src.PinCode))
              .ForMember(dest => dest.Email, cfg => cfg.MapFrom(src => src.Email))
              .ForMember(dest => dest.Telephone, cfg => cfg.MapFrom(src => src.Telephone))
              .ForMember(dest => dest.Extension, cfg => cfg.MapFrom(src => src.Extension))
              .ForMember(dest => dest.Fax, cfg => cfg.MapFrom(src => src.Fax))
              .ForMember(dest => dest.MobileNumber, cfg => cfg.MapFrom(src => src.MobileNumber));

            CreateMap<MerchantSetupViewModel, MerchantSetupDomainModel>().ReverseMap()
              .ForMember(dest => dest.MerchantId, cfg => cfg.MapFrom(src => src.MerchantId))
              .ForMember(dest => dest.DepositCashBalance, cfg => cfg.MapFrom(src => src.DepositCashBalance))
              .ForMember(dest => dest.WithdrawCashBalance, cfg => cfg.MapFrom(src => src.WithdrawCashBalance))
              .ForMember(dest => dest.IsOnline, cfg => cfg.MapFrom(src => src.IsOnline))
              .ForMember(dest => dest.Latitude, cfg => cfg.MapFrom(src => src.Latitude))
              .ForMember(dest => dest.Longitude, cfg => cfg.MapFrom(src => src.Longitude))
              .ForMember(dest => dest.ModifiedBy, cfg => cfg.MapFrom(src => src.ModifiedBy));

            CreateMap<TransactionTypeViewModel, TransactionTypeDomainModel>().ReverseMap();

            CreateMap<WithdrawalTypeViewModel, WithdrawalTypeDomainModel>().ReverseMap();

            CreateMap<TransactionStatusViewModel, TransactionStatusDomainModel>().ReverseMap();

            CreateMap<MerchantTypeViewModel, MerchantTypeDomainModel>().ReverseMap();

            CreateMap<MerchantSearchResultDomainModel, MerchantViewModel>()
               .ForMember(dest => dest.Id, cfg => cfg.MapFrom(src => src.Id))
               .ForMember(dest => dest.RefCode, cfg => cfg.MapFrom(src => src.RefCode))
               .ForMember(dest => dest.Name, cfg => cfg.MapFrom(src => src.Name))
               .ForMember(dest => dest.MerchantTypeId, cfg => cfg.MapFrom(src => src.MerchantTypeId))
               .ForMember(dest => dest.AddressLine1, cfg => cfg.MapFrom(src => src.AddressLine1))
               .ForMember(dest => dest.AddressLine2, cfg => cfg.MapFrom(src => src.AddressLine2))
               .ForMember(dest => dest.District, cfg => cfg.MapFrom(src => src.District))
               .ForMember(dest => dest.City, cfg => cfg.MapFrom(src => src.City))
               .ForMember(dest => dest.State, cfg => cfg.MapFrom(src => src.State))
               .ForMember(dest => dest.Country, cfg => cfg.MapFrom(src => src.Country))
               .ForMember(dest => dest.PinCode, cfg => cfg.MapFrom(src => src.PinCode))
               .ForMember(dest => dest.Email, cfg => cfg.MapFrom(src => src.Email))
               .ForMember(dest => dest.Telephone, cfg => cfg.MapFrom(src => src.Telephone))
               .ForMember(dest => dest.Extension, cfg => cfg.MapFrom(src => src.Extension))
               .ForMember(dest => dest.Fax, cfg => cfg.MapFrom(src => src.Fax))
               .ForMember(dest => dest.MobileNumber, cfg => cfg.MapFrom(src => src.MobileNumber))
               .ForMember(dest => dest.Latitude, cfg => cfg.MapFrom(src => src.Latitude))
               .ForMember(dest => dest.Longitude, cfg => cfg.MapFrom(src => src.Longitude))
               .ForMember(dest => dest.WithdrawCashBalance, cfg => cfg.MapFrom(src => src.WithdrawCashBalance))
               .ForMember(dest => dest.DepositCashBalance, cfg => cfg.MapFrom(src => src.DepositCashBalance))
               .ForMember(dest => dest.Rating, cfg => cfg.MapFrom(src => src.Rating))
               .ForMember(dest => dest.Distance, cfg => cfg.MapFrom(src => src.Distance))
               .ForMember(dest => dest.IsOnline, cfg => cfg.MapFrom(src => src.IsOnline))
               .ForMember(dest => dest.WithdrawalTypes, cfg => cfg.MapFrom(src => src.WithdrawalTypes));

            CreateMap<TransactionSummaryResultDomainModel, TransactionSummaryResultViewModel>();

            CreateMap<TransactionRequestsDomainModel, TransactionViewModel>()
             .ForMember(dest => dest.ReferenceNumber, cfg => cfg.MapFrom(src => src.ReferenceNumber))
             .ForMember(dest => dest.MerchantId, cfg => cfg.MapFrom(src => src.MerchantId))
             .ForMember(dest => dest.CustomerId, cfg => cfg.MapFrom(src => src.CustomerId))
             .ForMember(dest => dest.CustomerLatitude, cfg => cfg.MapFrom(src => src.CustomerLatitude))
             .ForMember(dest => dest.CustomerLongitude, cfg => cfg.MapFrom(src => src.CustomerLongitude))
             .ForMember(dest => dest.TransactionTypeId, cfg => cfg.MapFrom(src => src.TransactionTypeId))
             .ForMember(dest => dest.WithdrawalTypeId, cfg => cfg.MapFrom(src => src.WithdrawalTypeId))
             .ForMember(dest => dest.RequestedDateTime, cfg => cfg.MapFrom(src => src.RequestedDateTime))
             .ForMember(dest => dest.RequestedAmount, cfg => cfg.MapFrom(src => src.RequestedAmount))
             .ForMember(dest => dest.RequestCompletedDateTime, cfg => cfg.MapFrom(src => src.RequestCompletedDateTime))
             .ForMember(dest => dest.TransactionStatusId, cfg => cfg.MapFrom(src => src.TransactionStatusId))
             .ForMember(dest => dest.ActualAmount, cfg => cfg.MapFrom(src => src.ActualAmount))
             .ForMember(dest => dest.Remarks, cfg => cfg.MapFrom(src => src.Remarks))
             .ForMember(dest => dest.Validity, cfg => cfg.MapFrom(src => src.Validity))
             .ForMember(dest => dest.MerchantName, cfg => cfg.MapFrom(src => src.MerchantName))
             .ForMember(dest => dest.MerchantAddress, cfg => cfg.MapFrom(src => src.MerchantAddress))
             .ForMember(dest => dest.MerchantType, cfg => cfg.MapFrom(src => src.MerchantType))
             .ForMember(dest => dest.MerchantMobile, cfg => cfg.MapFrom(src => src.MerchantMobile))
             .ForMember(dest => dest.TransactionStatus, cfg => cfg.MapFrom(src => src.TransactionStatus))
             .ForMember(dest => dest.TransactionType, cfg => cfg.MapFrom(src => src.TransactionType))
             .ForMember(dest => dest.WithdrawalType, cfg => cfg.MapFrom(src => src.WithdrawalType))
             .ForMember(dest => dest.CustomerMobile, cfg => cfg.MapFrom(src => src.CustomerMobile))
             .ForMember(dest => dest.CustomerType, cfg => cfg.MapFrom(src => src.CustomerType))
             .ForMember(dest => dest.UniqueId, cfg => cfg.MapFrom(src => src.UniqueId))
             .ForMember(dest => dest.CustomerName, cfg => cfg.MapFrom(src => src.CustomerName))
             .ForMember(dest => dest.MerchantLatitude, cfg => cfg.MapFrom(src => src.MerchantLatitude))
             .ForMember(dest => dest.MerchantLongitude, cfg => cfg.MapFrom(src => src.MerchantLongitude));


            CreateMap<TransactionFeedbacksDomainModel, TransactionFeedbackViewModel>()
            .ForMember(dest => dest.MerchantId, cfg => cfg.MapFrom(src => src.MerchantId))
            .ForMember(dest => dest.CustomerId, cfg => cfg.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.TransactionId, cfg => cfg.MapFrom(src => src.TransactionId))
            .ForMember(dest => dest.Rating, cfg => cfg.MapFrom(src => src.Rating))
            .ForMember(dest => dest.Notes, cfg => cfg.MapFrom(src => src.Notes))
            .ForMember(dest => dest.UniqueId, cfg => cfg.MapFrom(src => src.UniqueId));

            CreateMap<TransactionStatusUpdateDomainModel, TransactionStatusUpdateViewModel>().ReverseMap()
          .ForMember(dest => dest.Id, cfg => cfg.MapFrom(src => src.TransactionId))
          .ForMember(dest => dest.Remarks, cfg => cfg.MapFrom(src => src.Remarks))
          .ForMember(dest => dest.TransactionNewStatusId, cfg => cfg.MapFrom(src => src.TransactionNewStatusId))
          .ForMember(dest => dest.TransactionOldStatusId, cfg => cfg.MapFrom(src => src.TransactionOldStatusId));

            CreateMap<MerchantDeleteViewModel, MerchantDomainModel>().ReverseMap()
           .ForMember(dest => dest.Id, cfg => cfg.MapFrom(src => src.Id));

            CreateMap<MerchantSetupViewModel, MerchantSetupDomainModel>().ReverseMap()
            .ForMember(dest => dest.MerchantId, cfg => cfg.MapFrom(src => src.MerchantId))
            .ForMember(dest => dest.DepositCashBalance, cfg => cfg.MapFrom(src => src.DepositCashBalance))
            .ForMember(dest => dest.WithdrawCashBalance, cfg => cfg.MapFrom(src => src.WithdrawCashBalance))
            .ForMember(dest => dest.IsOnline, cfg => cfg.MapFrom(src => src.IsOnline))
            .ForMember(dest => dest.Latitude, cfg => cfg.MapFrom(src => src.Latitude))
            .ForMember(dest => dest.Longitude, cfg => cfg.MapFrom(src => src.Longitude));

            CreateMap<GenerateOTPDomainModel, GenerateOTPViewModel>().ReverseMap()
            .ForMember(dest => dest.RequestId, cfg => Manager.Helpers.UtilityHelper.GetReferenceNumber())
            .ForMember(dest => dest.TellerID, cfg => cfg.MapFrom(src => src.TellerID));

            CreateMap<VerifyOTPDomainModel, VerifyOTPViewModel>().ReverseMap()
           .ForMember(dest => dest.RequestId, cfg => cfg.MapFrom(src => src.RequestId))
           .ForMember(dest => dest.TellerID, cfg => cfg.MapFrom(src => src.TellerID));

            CreateMap<VerifyOTPRequestDataDomainModel, VerifyOTPViewModel>().ReverseMap()
            .ForMember(dest => dest.CustomerMobileNo, cfg => cfg.MapFrom(src => src.CustomerMobileNo))
           .ForMember(dest => dest.OtpPin, cfg => cfg.MapFrom(src => src.OtpPin))
           .ForMember(dest => dest.OtpParam, cfg => cfg.MapFrom(src => src.OtpParam));

            CreateMap<CustomerViewModel, CustomerDomainModel>().ReverseMap()
            .ForMember(dest => dest.RefCode, cfg => cfg.MapFrom(src => src.RefCode))
            .ForMember(dest => dest.Type, cfg => cfg.MapFrom(src => src.Type))
            .ForMember(dest => dest.Mobile, cfg => cfg.MapFrom(src => src.Mobile))
            .ForMember(dest => dest.IsVerified, cfg => cfg.MapFrom(src => src.IsVerified))
            .ForMember(dest => dest.FirstName, cfg => cfg.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, cfg => cfg.MapFrom(src => src.LastName));

            CreateMap<CustomerViewModel, AccessTokenViewModel>().ReverseMap()
            .ForMember(dest => dest.RefCode, cfg => cfg.MapFrom(src => src.RefCode))
            .ForMember(dest => dest.Type, cfg => cfg.MapFrom(src => src.UserType))
            .ForMember(dest => dest.Mobile, cfg => cfg.MapFrom(src => src.Mobile))
            .ForMember(dest => dest.FirstName, cfg => cfg.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, cfg => cfg.MapFrom(src => src.LastName));

            CreateMap<SMSRequestViewModel, SMSRequestDomainModel>()
            .ForMember(dest => dest.RequestId, cfg => cfg.MapFrom(src => src.RequestId))
            .ForMember(dest => dest.MethodId, cfg => cfg.MapFrom(src => src.MethodId))
            .ForMember(dest => dest.TellerID, cfg => cfg.MapFrom(src => src.TellerID))
            .ForMember(dest => dest.SessionId, cfg => cfg.MapFrom(src => src.SessionId))
            .ForMember(dest => dest.IsEncrypt, cfg => cfg.MapFrom(src => src.IsEncrypt))
            .ForMember(dest => dest.RequestData, cfg => cfg.MapFrom(src => src.RequestData));

            CreateMap<ActivityLogViewModel, ActivityLogDomainModel>()
            .ForMember(dest => dest.ActionCode, cfg => cfg.MapFrom(src => src.ActionCode))
            .ForMember(dest => dest.OldJsonData, cfg => cfg.MapFrom(src => src.OldJsonData))
            .ForMember(dest => dest.NewJsonData, cfg => cfg.MapFrom(src => src.NewJsonData))
            .ForMember(dest => dest.CreatedBy, cfg => cfg.MapFrom(src => src.CreatedBy));

            CreateMap<MobileNoRequestViewModel, MobileNoRequestDomainModel>()
           .ForMember(dest => dest.TransactionId, cfg => cfg.MapFrom(src => src.TransactionId))
           .ForMember(dest => dest.CustomerID, cfg => cfg.MapFrom(src => src.CustomerID))
           .ForMember(dest => dest.CustomerMobile, cfg => cfg.MapFrom(src => src.CustomerMobile))
           .ForMember(dest => dest.MerchantId, cfg => cfg.MapFrom(src => src.MerchantId))
           .ForMember(dest => dest.MerchantMobile, cfg => cfg.MapFrom(src => src.MerchantMobile))
           .ForMember(dest => dest.TransactionType, cfg => cfg.MapFrom(src => src.TransactionType))
           .ForMember(dest => dest.ReferenceNumber, cfg => cfg.MapFrom(src => src.ReferenceNumber))
           .ForMember(dest => dest.Remarks, cfg => cfg.MapFrom(src => src.Remarks))
           .ForMember(dest => dest.UniqueId, cfg => cfg.MapFrom(src => src.UniqueId));

            CreateMap<SMSlogViewModel, SMSlogDomainModel>()
           .ForMember(dest => dest.TransactionId, cfg => cfg.MapFrom(src => src.TransactionId))
           .ForMember(dest => dest.MerchantId, cfg => cfg.MapFrom(src => src.MerchantId));

            CreateMap<MerchantDataViewModel, MerchantDataDomainModel>().ReverseMap()
            .ForMember(dest => dest.Id, cfg => cfg.MapFrom(src => src.Id))
            .ForMember(dest => dest.RefCode, cfg => cfg.MapFrom(src => src.RefCode))
            .ForMember(dest => dest.Name, cfg => cfg.MapFrom(src => src.Name))
            .ForMember(dest => dest.MerchantTypeId, cfg => cfg.MapFrom(src => src.MerchantTypeId))
            .ForMember(dest => dest.AddressLine1, cfg => cfg.MapFrom(src => src.AddressLine1))
            .ForMember(dest => dest.AddressLine2, cfg => cfg.MapFrom(src => src.AddressLine2))
            .ForMember(dest => dest.District, cfg => cfg.MapFrom(src => src.District))
            .ForMember(dest => dest.City, cfg => cfg.MapFrom(src => src.City))
            .ForMember(dest => dest.State, cfg => cfg.MapFrom(src => src.State))
            .ForMember(dest => dest.Country, cfg => cfg.MapFrom(src => src.Country))
            .ForMember(dest => dest.PinCode, cfg => cfg.MapFrom(src => src.PinCode))
            .ForMember(dest => dest.Email, cfg => cfg.MapFrom(src => src.Email))
            .ForMember(dest => dest.Telephone, cfg => cfg.MapFrom(src => src.Telephone))
            .ForMember(dest => dest.Extension, cfg => cfg.MapFrom(src => src.Extension))
            .ForMember(dest => dest.Fax, cfg => cfg.MapFrom(src => src.Fax))
            .ForMember(dest => dest.MobileNumber, cfg => cfg.MapFrom(src => src.MobileNumber))
            .ForMember(dest => dest.Latitude, cfg => cfg.MapFrom(src => src.Latitude))
            .ForMember(dest => dest.Longitude, cfg => cfg.MapFrom(src => src.Longitude))
            .ForMember(dest => dest.WithdrawCashBalance, cfg => cfg.MapFrom(src => src.WithdrawCashBalance))
            .ForMember(dest => dest.DepositCashBalance, cfg => cfg.MapFrom(src => src.DepositCashBalance))
            .ForMember(dest => dest.Rating, cfg => cfg.MapFrom(src => src.Rating))
            .ForMember(dest => dest.Distance, cfg => cfg.MapFrom(src => src.Distance))
            .ForMember(dest => dest.IsOnline, cfg => cfg.MapFrom(src => src.IsOnline))
            .ForMember(dest => dest.WithdrawalTypes, cfg => cfg.MapFrom(src => src.WithdrawalTypes))
             .ForMember(dest => dest.LimitSetupDate, cfg => cfg.MapFrom(src => src.LimitSetupDate));
        }
    }
}