using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Payment.Infrastructure.Extension
{
    /// <summary>
    /// پیام های بانک سامان
    /// VerifyTransaction()
    /// </summary>
    public static class SBMessages
    {
        #region Generate Saman bank's messages
        /// <summary>
        /// دیکشنری پیام های بانک سامان
        /// </summary>
        internal static Dictionary<string, string> ErrorDic = new Dictionary<string, string>() { 
            //Messages
            {"OK", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.OK},
            {"CanceledByUser", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.CanceledByUser},
            {"-100", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.SBNegative100},
            {"InvalidAmount", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.InvalidAmount},
            {"InvalidTransaction", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.InvalidTransaction},
            {"InvalidCardNumber", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.InvalidCardNumber},
            {"NoSuchIssuer", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.NoSuchIssuer},
            {"ExpiredCardPickUp", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.ExpiredCardPickUp},
            {"AllowablePINTriesExceededPickUp", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.AllowablePINTriesExceededPickUp},
            {"IncorrectPIN", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.IncorrectPIN},
            {"ExceedsWithdrawalAmountLimit", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.ExceedsWithdrawalAmountLimit},
            {"TransactionCannotBeCompleted", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.TransactionCannotBeCompleted},
            {"ResponseReceivedTooLate", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.ResponseReceivedTooLate},
            {"Suspected Fraud Pick Up", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.SuspectedFraudPickUp},
            {"NoSufficientFunds", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.NoSufficientFunds},
            {"IssuerDownSlm", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.IssuerDownSlm},
            {"TMEError", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.TMEError},
            {"Failed", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.Failed},
            {"SessionIsNull", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.SessionIsNull},
            {"InvalidParameters", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.InvalidParameters},
            {"MultiStateTxn", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.MultiStateTxn},
            {"TotalFailed", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.TotalFailed},
            //Verify transaction errors
            {"-1",  ParvazPardaz.Payment.Infrastructure.Resource.BankResource.SBNegative1 },
            {"-3",  ParvazPardaz.Payment.Infrastructure.Resource.BankResource.SBNegative3 },
            {"-4",  ParvazPardaz.Payment.Infrastructure.Resource.BankResource.SBNegative4 },
            {"-6",  ParvazPardaz.Payment.Infrastructure.Resource.BankResource.SBNegative6 },
            {"-7",  ParvazPardaz.Payment.Infrastructure.Resource.BankResource.SBNegative7 },
            {"-8",  ParvazPardaz.Payment.Infrastructure.Resource.BankResource.SBNegative8 },
            {"-9",  ParvazPardaz.Payment.Infrastructure.Resource.BankResource.SBNegative9 },
            {"-10", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.SBNegative10},
            {"-11", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.SBNegative11},
            {"-12", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.SBNegative12},
            {"-13", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.SBNegative13},
            {"-14", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.SBNegative14},
            {"-15", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.SBNegative15},
            {"-16", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.SBNegative16},
            {"-17", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.SBNegative17},
            {"-18", ParvazPardaz.Payment.Infrastructure.Resource.BankResource.SBNegative18},             
        };
        #endregion

        #region IsDicHasThisKey
        /// <summary>
        /// آیا در بین کلیدهای موجود در دیکشنری پیام ها وجود دارد؟
        /// </summary>
        /// <param name="dicKey">کد پیام</param>
        /// <returns></returns>
        public static bool IsDicHasThisKey(this string dicKey)
        {
            if (dicKey != null)
                return ErrorDic.ContainsKey(dicKey);
            else
                return false;
        }
        #endregion

        #region GetDicMessage
        /// <summary>
        /// دریافت پیام توسط کد آن
        /// </summary>
        /// <param name="dicKey">کد پیام</param>
        /// <returns></returns>
        public static string GetDicMessage(this string dicKey)
        {
            if (ErrorDic.ContainsKey(dicKey))
                return ErrorDic.FirstOrDefault(x => x.Key.Equals(dicKey)).Value;
            return dicKey;
        }
        #endregion
    }
}
