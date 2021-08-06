using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Data.DTO;
using WebAPI.Data.Entities;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        public AccountService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }


        public async Task<ServiceResponse<string>> Login(AccountLogIn request)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            var user = await _context.Accounts.FirstOrDefaultAsync(x => x.Username.ToLower().Equals(request.Username.ToLower()));

            if (user == null)
            {
                response.Message = "Log in error";
                response.Status = false;
            }
            else if (!VerifyPassswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                response.Message = "Log in error";
                response.Status = false;
            }
            else
            {
                var token = CreateToken(user);
                if (token == null)
                {
                    response.Message = "Log in error";
                    response.Status = false;
                }
                else
                {
                    response.Data = token;
                    response.Message = "Loggined!";
                }
            }

            return response;
        }

        public async Task<ServiceResponse<string>> Register(AccountRegistration request)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            Account user = new Account();

            if (await UserExists(request.Username))
            {
                response.Status = false;
                response.Message = "User already exitst";
                return response;
            }

            CreateHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            try
            {
                user.Username = request.Username;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                _context.Accounts.Add(user);
                await _context.SaveChangesAsync();
                response.Message = "Account has been registered!";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
            }

            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Accounts.AnyAsync(item => item.Username.Equals(username)))
                return true;

            return false;
        }


        private void CreateHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }


        private string CreateToken(Account user)
        {
            var identity = GetIdentity(user);
            if (identity == null)
                return null;

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.GetSection("TokenSettings").GetSection("KEY").Value));

            var jwt = new JwtSecurityToken(
                issuer: _config.GetSection("TokenSettings").GetSection("ISSUER").Value,
                audience: _config.GetSection("TokenSettings").GetSection("AUDIENCE").Value,
                notBefore: DateTime.UtcNow,
                claims: identity.Claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token;
        }
        private ClaimsIdentity GetIdentity(Account user)
        {
            try
            {
                var claims = new List<Claim> {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
                };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            catch
            {
                return null;
            }
        }


    }
}
