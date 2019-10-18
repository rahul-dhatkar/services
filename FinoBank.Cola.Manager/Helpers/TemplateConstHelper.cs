namespace FinoBank.Cola.Manager.Helpers
{
    public static class TemplateConstHelper
    {
        /***Cash Deposit***/

                /// <summary>
                ///Customer receives an OTP post selection of Merchant to service his request
                /// </summary>
                public const string CUSTOMER_CASH_DEPOSIT = "1166";
                /// <summary>
                /// Request confirmed, SMS with request number and merchant contact details sent to customer
                /// </summary>
                public const string CUSTOMER_CASH_DEPOSIT_RECEIVED = "1167";
                /// <summary>
                /// Request confirmed, SMS with request number and customer contact details sent to merchant
                /// </summary>
                public const string MERCHANT_CASH_DEPOSIT_RECEIVED = "1168";

        /***Cash Withdrawal - Withdrawal less than 10K***/

                /// <summary>
                /// Customer receives an OTP post selection of Merchant to service his request
                /// </summary>
                public const string CUSTOMER_CASH_WITHDRAWAL_OTP = "1169";
                /// <summary>
                /// Request confirmed, SMS with request number and merchant contact details sent to customer
                /// </summary>
                public const string CUSTOMER_CASH_WITHDRAWAL_REQUEST = "1170";
                /// <summary>
                /// Request confirmed, SMS with request number and customer contact details sent to merchant
                /// </summary>
                public const string MERCHANT_CASH_WITHDRAWAL_REQUEST = "1171";

        /***Cash Withdrawal - Withdrawal greater than 10K***/

                /// <summary>
                /// Customer receives an OTP post providing details so that a suitable merchant can be shortlisted for him
                /// </summary>
                public const string CUSTOMER_SHORTLIST_MERCHANT_OTP = "1172";
                /// <summary>
                /// SMS sent to merchants in the vicinity of the customer for accepting and processing his transaction request
                /// </summary>
                public const string MERCHANT_ACCEPT_AND_PROCESS_TRANSACTION = "1173";
                /// <summary>
                /// Request confirmed, SMS with request number and merchant contact details sent to customer
                /// </summary>
                public const string CUSTOMER_WITHDRAWAL_RECEIVED = "1174";
                /// <summary>
                /// Request confirmed, SMS with request number and customer contact details sent to merchant
                /// </summary>
                public const string MERCHANT_CUSTOMER_WITHDRAWAL_RECEIVED = "1175";

        /***Completed Transaction***/

                /// <summary>
                /// SMS received by customer  on successful completion of his transaction
                /// </summary>
                public const string CUSTOMER_COMPLETED_TRANSACTION_DEPOSIT = "1177";

                /// <summary>
                ///SMS received by customer  on successful completion of his transaction
                /// </summary>
                public const string CUSTOMER_COMPLETED_TRANSACTION_WITHDRAWAL = "1178";

        /***Expired Transaction --Expire manager***/

                /// <summary>
                /// SMS received by customer on expiration of the valid time of the transaction
                /// </summary>
                public const string CUSTOMER_CASH_DEPOSIT_EXPIRE = "1179";

                /// <summary>
                /// SMS received by customer on expiration of the valid time of the transaction
                /// </summary>
                public const string CUSTOMER_CASH_WITHDRAWAL_EXPIRE = "1180";

                /// <summary>
                ///SMS received by merchant on expiration of the valid time of the transaction
                /// </summary>
                public const string MERCHANT_CASH_DEPOSIT_EXPIRE = "1181";

                /// <summary>
                ///SMS received by merchant on expiration of the valid time of the transaction
                /// </summary>
                public const string MERCHANT_CASH_WITHDRAWAL_EXPIRE = "1182";

        /***Cancelled Transaction - Cancellation by customer***/

                /// <summary>
                /// SMS received by customer on cancellation of the transaction
                /// </summary>
                public const string CASH_DEPOSIT_CANCELLED_CUSTOMER_REQUEST = "1183";

                /// <summary>
                /// SMS received by customer on cancellation of the transaction
                /// </summary>
                public const string CASH_WITHDRAWAL_CANCELLED_CUSTOMER_REQUEST = "1184";

                /// <summary>
                /// SMS received by merchant on cancellation of the transaction
                /// </summary>
                public const string MERCHANT_CASH_DEPOSIT_CANCELLED_CUSTOMER_REQUEST = "1185";

                /// <summary>
                /// SMS received by merchant on cancellation of the transaction
                /// </summary>
                public const string MERCHANT_CASH_WITHDRAWAL_CANCELLED_CUSTOMER_REQUEST = "1186";

        /***Cancelled Transaction - Cancellation by merchant***/

                /// <summary>
                /// SMS received by customer on cancellation of the transaction
                /// </summary>
                public const string CASH_DEPOSIT_CANCELLED_MERCHANT_REQUEST = "1187";

                /// <summary>
                /// SMS received by customer on cancellation of the transaction
                /// </summary>
                public const string CASH_WITHDRAWAL_CANCELLED_MERCHANT_REQUEST = "1188";

                /// <summary>
                ///SMS received by merchant on cancellation of the transaction
                /// </summary>
                public const string MERCHANT_CASH_DEPOSIT_CANCELLED_MERCHANT_REQUEST = "1189";

                /// <summary>
                /// SMS received by merchant on cancellation of the transaction
                /// </summary>
                public const string MERCHANT_CASH_WITHDRAWAL_CANCELLED_MERCHANT_REQUEST = "1190";
    }
}

