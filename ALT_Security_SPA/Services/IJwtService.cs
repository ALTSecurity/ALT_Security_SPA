using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace ALT_Security_SPA.Services
{
    public interface IJwtService
    {
        string GenerateToken(List<Claim> claims, out DateTime expires);
    }
}
