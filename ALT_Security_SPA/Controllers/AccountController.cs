using System;
using System.Threading;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using ALT_Security_SPA.Models;
using ALT_Security_SPA.Models.Identity;
using ALT_Security_SPA.Services;
using ALT_Security_SPA.Utility;

namespace ALT_Security_SPA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<MessageResources> _resources;
        private readonly IJwtService _jwtService;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 IStringLocalizer<MessageResources> resources,
                                 IJwtService jwtService)
        {
            _userManager = userManager;
            _resources = resources;
            _jwtService = jwtService;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponseBase(ApiResponceCode.InvalidInputParameters, _resources["IncorrectInputParameters"], ModelState.Errors()));
            }

            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByNameAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var userRoles = await _userManager.GetRolesAsync(user);

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                string token = _jwtService.GenerateToken(authClaims, out DateTime expires);
                if (!string.IsNullOrEmpty(token))
                {
                    var response = new LoginResponse(ApiResponceCode.OK, _resources["SuccessfullOperation"])
                    {
                        Token = token,
                        ExpiredAt = expires
                    };

                    return Ok(response);
                }
            }
           
            return Unauthorized();
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponseBase(ApiResponceCode.InvalidInputParameters, _resources["IncorrectInputParams"], ModelState.Errors()));
            }

            cancellationToken.ThrowIfCancellationRequested();

            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
            {
                return BadRequest(new ApiResponseBase(ApiResponceCode.InvalidInputParameters, _resources["UserAlreadyExists"]));
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            { 
                var errors = new List<KeyValuePair<string, string[]>>();

                foreach (var error in result.Errors)
                {
                    errors.Add(new KeyValuePair<string, string[]>(error.Code, new string[] { error.Description }));
                }

                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponseBase(ApiResponceCode.ProcessingError, _resources["ErrorWhileProcessing"], errors));
            }

            return Ok();
        }
    }
}
