using System;

namespace ALT_Security_SPA.Models.Identity
{
    public class LoginResponse: ApiResponseBase
    {
        public string Token { get; set; }

        public DateTime ExpiredAt { get; set; }

        public LoginResponse(ApiResponceCode code, string message): base(code,message) { }
    }
}
