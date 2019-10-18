using FinoBank.Cola.Manager.ViewModels;
using Newtonsoft.Json;
using System;
using System.Globalization;

namespace FinoBank.Cola.Manager.Helpers
{
    public static class UtilityHelper
    {
        public static string GetReferenceNumber()
        {
            Random rand = new Random();
            return (DateTime.Now).ToString("ddMMyyyyHHmmss" + rand.Next(99, 999), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Builds the activity logger.
        /// </summary>
        /// <typeparam name="O"></typeparam>
        /// <typeparam name="N"></typeparam>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns></returns>
        public static ActivityLogViewModel BuildActivityLogger<O, N>(string actionCode, O oldValue, N newValue, string createdBy)
        {
            return new ActivityLogViewModel()
            {
                ActionCode = actionCode,
                OldJsonData = oldValue != null ? JsonConvert.SerializeObject(oldValue) : string.Empty,
                NewJsonData = newValue != null ? JsonConvert.SerializeObject(newValue) : string.Empty,
                CreatedBy = createdBy
            };
        }
    }
}