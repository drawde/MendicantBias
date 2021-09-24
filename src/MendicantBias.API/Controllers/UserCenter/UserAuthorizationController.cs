//*******************************
// Create By Drawde
// Date 2021-08-25 10:18
// Code Generate By SmartCode
// Code Generate Github : https://github.com/Ahoo-Wang/SmartCode
//*******************************
using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MendicantBias.Entity;
using MendicantBias.Repository;
using MendicantBias.Service;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MendicantBias.Entity.UserCenter;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using AutoMapper;
using MendicantBias.Util;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using IdentityModel;
using System.Text;

namespace MendicantBias.API.Controllers
{
    ///<summary>
    /// Table, Users
    ///</summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserAuthorizationController : Controller
    {
        private readonly IUserAuthorizationService _userAuthorizationService;
        private readonly IMapper _mapper;

        public UserAuthorizationController(IUserAuthorizationService userAuthorizationService, IMapper mapper)
        {
            _userAuthorizationService = userAuthorizationService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPut]
        public async Task<ResponseMessageWrap<UsersModel>> Login([FromBody] UserLoginParameter userLoginParameter)
        {
            var loginUser = await _userAuthorizationService.Login(userLoginParameter);
            UsersModel user = null;
            if(loginUser != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var authTime = DateTime.UtcNow;
                var expiresAt = authTime.AddDays(7);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(JwtClaimTypes.Audience,ApiWebConfig.Instance.Auth.JWTAudience),
                        new Claim(JwtClaimTypes.Issuer,ApiWebConfig.Instance.Auth.JWTAudience),
                        new Claim(JwtClaimTypes.Id, loginUser.Id.ToString()),
                        new Claim(JwtClaimTypes.Name, loginUser.UserName),
                        new Claim(JwtClaimTypes.FamilyName, loginUser.TenantCode),
                    }),
                    Expires = expiresAt,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ApiWebConfig.Instance.Auth.SecretKey)), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                user = _mapper.Map<UsersEntity, UsersModel>(loginUser);
                user.Token = tokenString;
            }
            return new ResponseMessageWrap<UsersModel>
            {
                Body = user
            };
        }
    }
}