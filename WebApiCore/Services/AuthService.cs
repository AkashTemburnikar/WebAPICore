using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApiCore.Models;

namespace WebApiCore.Services
{
    public class AuthService
    {
        UserManager<IdentityUser> userManger;
        SignInManager<IdentityUser> signInManager;
        IConfiguration configuration;

        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
                            IConfiguration configuration)
        {
            this.userManger = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }
        public async Task<bool> CreateUserAsync(RegitserUser register)
        {
            bool isUserCreated = false;
            var newUser = new IdentityUser()
            {
                UserName = register.Email,
                Email = register.Email
            };
            var res = await userManger.CreateAsync(newUser, register.Password);
            if (res.Succeeded)
            {
                isUserCreated = true;
            }
            return isUserCreated;
        }

        public async Task<string> AuthUserAsync(LoginUser user)
        {
            string jwtToken = "";
            var result = await signInManager.PasswordSignInAsync(user.UserName, user.Password, false, lockoutOnFailure:true);
            if (result.Succeeded)
            {
                var secretKey = Convert.FromBase64String(configuration["temburaaCoreApiKey"]);
                var expiryTime = Convert.ToInt32(configuration["JWTCoreSettings:ExpiryInMinutes"]);
                IdentityUser appUser = new IdentityUser(user.UserName);
                var securityTokenDescriptor = new SecurityTokenDescriptor()
                {
                    Issuer = null,
                    Audience = null,
                    Subject = new ClaimsIdentity(new List<Claim>
                    {
                        new Claim("username", appUser.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(expiryTime),
                    IssuedAt = DateTime.UtcNow,
                    NotBefore = DateTime.UtcNow,
                    SigningCredentials = new SigningCredentials( new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256)
                };

                var jwtHandler = new JwtSecurityTokenHandler();
                var token = jwtHandler.CreateJwtSecurityToken(securityTokenDescriptor);
                jwtToken = jwtHandler.WriteToken(token);
            }
            else
            {
                jwtToken = "Login Failed";
            }
            return jwtToken;
        }
    }
}
