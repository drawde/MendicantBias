using IdentityModel;
using MendicantBias.Entity;
using MendicantBias.Entity.UserCenter;
using MendicantBias.Util;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MendicantBias.TenantAdmin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpConnector _httpConnector;

        public AccountController(IHttpConnector httpConnector)
        {
            _httpConnector = httpConnector;
        }

        public async Task<IActionResult> Login()
        {
            var response = await _httpConnector.PutAsync<ResponseMessageWrap<UsersModel>>("/UserAuthorization/Login", new 
            {
                tenantCode = "string",
                userName = "string",
                password = "string"
            });
            if (response.IsSuccess && response.Body != null)
            {

                return Content(response.Body.Token);
            }

            return View();
        }
    }
}
