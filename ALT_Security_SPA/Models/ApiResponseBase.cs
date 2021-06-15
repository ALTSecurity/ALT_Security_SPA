using System.Collections.Generic;

namespace ALT_Security_SPA.Models
{
    /// <summary>
    /// Коди повернення 
    /// </summary>
    public enum ApiResponceCode
    {
        /// <summary>
        /// Успішна операція
        /// </summary>
        OK = 0,
        /// <summary>
        /// Несанкціонований запит
        /// </summary>
        Unauthorized = -1,
        /// <summary>
        /// Помилка обробки запиту
        /// </summary>
        ProcessingError = -2,
        /// <summary>
        /// Некоректні вхідні параметри
        /// </summary>
        InvalidInputParameters = -3,
        /// <summary>
        /// Недостатньо дозволів для виконання операції
        /// </summary>
        InsufficientPrivileges = -4
    }

    public class ApiResponseBase
    {
        public ApiResponceCode Code { get; set; }

        /// <summary>
        /// Текст відповіді сервера
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Список пар "ключ"-"значення" для помилок
        /// </summary>
        public IEnumerable<KeyValuePair<string, string[]>> Errors { get; set; }

        public ApiResponseBase() { }

        public ApiResponseBase(ApiResponceCode code, string message)
        {
            Code = code;
            Message = message;
        }

        public ApiResponseBase(string message, IEnumerable<KeyValuePair<string, string[]>> errors)
        {
            Message = message;
            Errors = errors;
        }

        public ApiResponseBase(ApiResponceCode code, string message, IEnumerable<KeyValuePair<string, string[]>> errors)
        {
            Code = code;
            Message = message;
            Errors = errors;
        }
    }
}
