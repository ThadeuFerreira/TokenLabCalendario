using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TokenLabCalendar.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TokenLabCalendar.Controllers.API
{

    public class ApiController : ControllerBase
    {

        #region Protected Members

        /// <summary>
        /// The scoped Application context
        /// </summary>
        protected CalendarioContext mContext;

        /// <summary>
        /// The manager for handling user creation, deletion, searching, roles etc...
        /// </summary>
        protected UserManager<ApplicationUser> mUserManager;

        /// <summary>
        /// The manager for handling signing in and out for our users
        /// </summary>
        protected SignInManager<ApplicationUser> mSignInManager;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context">The injected context</param>
        /// <param name="signInManager">The Identity sign in manager</param>
        /// <param name="userManager">The Identity user manager</param>
        public ApiController(
            CalendarioContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            mContext = context;
            mUserManager = userManager;
            mSignInManager = signInManager;
            Configuration = configuration;
        }

        #endregion

        public IConfiguration Configuration { get; }

        [AllowAnonymous]
        [Route("api/create")]
        public async Task<ApiResponse<RegisterResultApiModel>> RegisterAsync(
            [FromBody] RegisterCredentialsApiModel registerCredentials)
        {
            // TODO: Localize all strings
            // The message when we fail to login
            var invalidErrorMessage = "Please provide all required details to register for an account";

            // The error response for a failed login
            var errorResponse = new ApiResponse<RegisterResultApiModel>
            {
                // Set error message
                ErrorMessage = invalidErrorMessage
            };

            // If we have no credentials...
            if (registerCredentials == null)
                // Return failed response
                return errorResponse;

            // Make sure we have a user name
            if (string.IsNullOrWhiteSpace(registerCredentials.Username))
                // Return error message to user
                return errorResponse;

            // Create the desired user from the given details
            var user = new ApplicationUser
            {
                UserName = registerCredentials.Username,
                FirstName = registerCredentials.FirstName,
                LastName = registerCredentials.LastName,
                Email = registerCredentials.Email
            };

            // Try and create a user
            var result = await mUserManager.CreateAsync(user, registerCredentials.Password);

            // If the registration was successful...
            if (result.Succeeded)
            {
                // Get the user details
                var userIdentity = await mUserManager.FindByNameAsync(user.UserName);

                // Return valid response containing all users details
                return new ApiResponse<RegisterResultApiModel>
                {
                    Response = new RegisterResultApiModel
                    {
                        FirstName = userIdentity.FirstName,
                        LastName = userIdentity.LastName,
                        Email = userIdentity.Email,
                        Username = userIdentity.UserName,
                        Token = GenerateJwtToken(userIdentity)
                    }
                };
            }
            // Otherwise if it failed...
            else
                // Return the failed response
                return new ApiResponse<RegisterResultApiModel>
                {
                    // Aggregate all errors into a single error string
                    ErrorMessage = result.Errors.ToList().Select(f => f.Description).Aggregate((a, b) => $"{a}{Environment.NewLine}{b}")
                };
        }

        [Route("api/login")]
        public IActionResult LogIn()
        {
            var username = "Antonio";
            var email = "contact@email.com";

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.NameId, username),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimsIdentity.DefaultNameClaimType, username),

                new Claim("my key", "my value")

            };

            var credential = new SigningCredentials(new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: Configuration["Jwt:Issuer"],
                audience: Configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credential);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            // Set our tokens claims
            var claims = new[]
            {
                // Unique ID for this token
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),

                // The username using the Identity name so it fills out the HttpContext.User.Identity.Name value
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),

                // Add user Id so that UserManager.GetUserAsync can find the user based on Id
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            // Create the credentials used to generate the token
            var credentials = new SigningCredentials(
                // Get the secret key from configuration
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                // Use HS256 algorithm
                SecurityAlgorithms.HmacSha256);

            // Generate the Jwt Token
            var token = new JwtSecurityToken(
                issuer: Configuration["Jwt:Issuer"],
                audience: Configuration["Jwt:Audience"],
                claims: claims,
                signingCredentials: credentials,
                // Expire if not used for 3 months
                expires: DateTime.Now.AddMonths(3)
            );

            // Return the generated token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
