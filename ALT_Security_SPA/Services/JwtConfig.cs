using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALT_Security_SPA.Services
{
    public class JwtConfig
    {
        public string Audience { get; set; } = "http://localhost:51891";
        public string Issuer { get; set; } = "https://localhost:44318";
        public string Secret { get; set; } = "eNRTcUbQU6u2SuaQQmXwF09m67BatkJB";
    }
}
