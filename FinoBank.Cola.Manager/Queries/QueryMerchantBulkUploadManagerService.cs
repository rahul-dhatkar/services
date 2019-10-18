using AutoMapper;
using Contesto.V2.Core.Common.Manager.Base;
using Contesto.V2.Core.Common.Manager.Helpers;
using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.Interfaces;
using FinoBank.Cola.Manager.ViewModels;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Uom.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Queries
{
    /// <summary>
    /// Query OTP ManagerService
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Manager.Base.BaseManager" />
    /// <seealso cref="FinoBank.Cola.Manager.Interfaces.IQueryOTPManagerService" />
    public class QueryMerchantBulkUploadManagerService : BaseManager, IQueryMerchantBulkUploadManagerService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryOTPManagerService"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public QueryMerchantBulkUploadManagerService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, null, null)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<CommandSuccessBoolResultViewModel>> MerchantBulkUpload(string uploadFilePath)
        {
            //MerchantViewModel model = new MerchantViewModel();

            List<string> records = new List<string>();

            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(File.OpenRead(uploadFilePath)))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                string file = sr.ReadToEnd();
                records = new List<string>(file.Split('\n'));
                if(records.Contains(""))
                {
                    records.Remove("");
                }
               
            }

            List<MerchantViewModel> merchantList = new List<MerchantViewModel>();

            foreach (string record in records)
            {
                MerchantViewModel merchant = new MerchantViewModel();
                string[] textpart = record.Split(',');

                merchant.RefCode = textpart[0].Replace("\"", "");
                merchant.Name = textpart[1];
                merchant.MerchantTypeId = Convert.ToByte(textpart[2]);
                merchant.AddressLine1 = textpart[3];
                merchant.AddressLine2 = textpart[4];
                merchant.District = textpart[5];
                merchant.City = textpart[6];
                merchant.State = textpart[7];
                merchant.Country = textpart[8];
                merchant.PinCode = textpart[9];
                merchant.Email = textpart[10];
                merchant.Telephone = textpart[11];
                merchant.Extension = textpart[12];
                merchant.Fax = textpart[13];
                merchant.MobileNumber = textpart[14];

                merchant.DepositCashBalance = Convert.ToDecimal(textpart[15]);
                merchant.WithdrawCashBalance = Convert.ToDecimal(textpart[16]);
                merchant.Latitude = Convert.ToDouble(textpart[17]);
                merchant.Longitude = Convert.ToDouble(textpart[18]);
                merchant.CreatedBy = textpart[19].Replace("\"", "");

                merchantList.Add(merchant);
            }

            var copyParameters = new[]
             {
                        nameof(MerchantViewModel.RefCode),
                        nameof(MerchantViewModel.Name),
                        nameof(MerchantViewModel.MerchantTypeId),
                        nameof(MerchantViewModel.AddressLine1),
                        nameof(MerchantViewModel.AddressLine2),
                        nameof(MerchantViewModel.District),
                        nameof(MerchantViewModel.City),
                        nameof(MerchantViewModel.State),
                        nameof(MerchantViewModel.Country),
                        nameof(MerchantViewModel.PinCode),
                        nameof(MerchantViewModel.Email),
                        nameof(MerchantViewModel.Telephone),
                        nameof(MerchantViewModel.Extension),
                        nameof(MerchantViewModel.Fax),
                        nameof(MerchantViewModel.MobileNumber),
                        nameof(MerchantViewModel.DepositCashBalance),
                        nameof(MerchantViewModel.WithdrawCashBalance),
                        nameof(MerchantViewModel.Latitude),
                        nameof(MerchantViewModel.Longitude),
                        nameof(MerchantViewModel.CreatedBy)
                    };

            var details = MappService.Map<List<MerchantDomainModel>>(merchantList);//var result =
            await _unitOfWork.QueryMerchantBulkUploadRepository.MerchantBulkUpload(details).ConfigureAwait(false);
            return ResponseBuilderHelper<CommandSuccessBoolResultViewModel>.Instance.BuildSucessResult(new CommandSuccessBoolResultViewModel() { ResponseValue = true });
        }
    }
}

