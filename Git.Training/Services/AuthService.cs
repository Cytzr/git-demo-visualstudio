using Binus.WS.Pattern.Helper;
using Binus.WS.Pattern.Output;
using Binus.WS.Pattern.RouteGuardV3;
using Binus.WS.Pattern.Service;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Text;
using System.Security.Claims;
using Binus.WS.Pattern.Extensions;


namespace Git.Training.Services
{
    [ApiController]
    [Route("auth")]
    public class AuthService : BaseService
    {
        public AuthService(ILogger<BaseService> logger) : base(logger)
        {
        }

        /// <summary>
        /// Get JWT Token
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /auth/token
        ///
        /// </remarks>
        /// <returns>A JWT Token that will lasts 60 minutes. Used by placing on Headers.</returns>
        /// <response code="200">Returns Generated Token</response>
        [HttpGet]
        [Route("token")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TokenOutput), StatusCodes.Status200OK)]
        [RouteGuardV3(Authentication = "Basic")]
        public IActionResult GetToken()
        {
            try
            {
                string encodedDivisionId = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
                return new OkObjectResult(AuthHelper.GenerateJSONWebToken(Encoding.UTF8.GetString(Convert.FromBase64String(encodedDivisionId))));
            }
            catch (Exception ex)
            {
                return StatusCode(401, new OutputBase(ex));
            }
        }

        /// <summary>
        /// Get JWT Token with MSAL
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /auth/token-msal
        ///
        /// </remarks>
        /// <returns>A JWT Token that will lasts 60 minutes. Used by placing on Headers.</returns>
        /// <response code="200">Returns Generated Token</response>
        [HttpGet]
        [Route("token-msal")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TokenOutput), StatusCodes.Status200OK)]
        [RouteGuardV3(Authentication = "Bearer")]
        public IActionResult GetTokenMsal()
        {
            try
            {
                string tokenMsal = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
                string divisionID = Encoding.UTF8.GetString(Convert.FromBase64String(HttpContext.Request.Headers["BinusAuthorization"].ToString().Split(" ")[1]));
                int duration = Program.Configuration.GetSection("Jwt:Duration").Value.ToInt();

                // Get user data with email from Database
                //return new OkObjectResult(AuthHelper.GenerateJSONWebToken(tokenMsal));
                HttpContext.Items.TryGetValue("email", out var theEmail);

                // Create new Claims to generate new Token
                Claim[] claims = new Claim[] {
                        new Claim("Email", theEmail.ToString()),
                        new Claim("BinusianId", "testValue1"),
                        new Claim("Name", "testValue2")
                        // Add more claims as needed
                    };

                return new OkObjectResult(AuthHelper.CreateJWTToken(divisionID, claims));
            }
            catch (Exception ex)
            {
                return StatusCode(401, new OutputBase(ex));
            }
        }
    }
}
