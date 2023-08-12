using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AsyncInn.Services
{
    public class JwtTokenService
    {
        private SignInManager<ApplicationUser> signInManager;

        private IConfiguration configuration;

        public JwtTokenService(SignInManager<ApplicationUser> manager, IConfiguration config)
        {
            signInManager = manager;
            configuration = config;
        }

        public static TokenValidationParameters GetValidationParameters(IConfiguration configuration)
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                // This Is Our main goal: Make sure the security key, which comes from configuration is valid
                IssuerSigningKey = GetSecurityKey(configuration),

                // For simplifying testing
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        }
        // IConfiguration read appsetting.json give us access to configuration settings within your code 
        private static SecurityKey GetSecurityKey(IConfiguration configuration)
        {
            var secret = configuration["JWT:Secret"];
            if (secret == null) { throw new InvalidOperationException("JWT:Secret is midding"); }
            //return array if string to charterte
            var secretBytes = Encoding.UTF8.GetBytes(secret);
            return new SymmetricSecurityKey(secretBytes);
        }
        public async Task<string> GetToken(ApplicationUser user, TimeSpan expiresIn)
        {
            var principal = await signInManager.CreateUserPrincipalAsync(user);
            //principal its not user record its information about user
            if (principal == null) { return null; }

            var signingKey = GetSecurityKey(configuration);
            var token = new JwtSecurityToken(
              expires: DateTime.UtcNow + expiresIn,
              //string represents how long the token is good for
              signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
              claims: principal.Claims
             );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
