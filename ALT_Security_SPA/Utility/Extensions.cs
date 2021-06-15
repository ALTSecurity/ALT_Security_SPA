using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ALT_Security_SPA.Utility
{
    public static class Extensions
    {
        /// <summary>
        /// Model state errors
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, string[]>> Errors(this ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return modelState.ToDictionary(kvp => kvp.Key.Substring(kvp.Key.IndexOf('.') + 1),
                    kvp => kvp.Value.Errors
                                    .Select(e => !string.IsNullOrEmpty(e.ErrorMessage) ? e.ErrorMessage : e.Exception.Message).ToArray())
                                    .Where(m => m.Value.Any());
            }
            return null;
        }
    }
}
