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
using MendicantBias.Util;

namespace MendicantBias.API.Controllers
{
    ///<summary>
    /// Table, Users
    ///</summary>
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        [HttpPost]
        public async Task<ResponseMessageWrap<long>> Insert([FromBody] UsersEntity users)
        {
            return new ResponseMessageWrap<long>
            {
                Body = await _usersService.Insert(users)
            };
        }
        [HttpDelete]
        public async Task<ResponseMessageWrap<long>> DeleteById(long id)
        {
            return new ResponseMessageWrap<long>
            {
                Body = await _usersService.DeleteById(id)
            };
        }
        [HttpPut]
        public async Task<ResponseMessageWrap<int>> Update([FromBody] UsersEntity users)
        {
            return new ResponseMessageWrap<int>
            {
                Body = await _usersService.Update(users)
            };
        }
        [HttpPost]
        public async Task<ResponseMessageWrap<QueryByPageResponse<UsersEntity>>> QueryByPage([FromBody] QueryByPageRequest reqMsg, string userName)
        {
            var requestParameter = new
            {
                reqMsg.PageIndex,
                reqMsg.PageSize,
                reqMsg.Offset,
                UserName = userName
            };
            var total = await _usersService.GetRecord(requestParameter);
            var list = await _usersService.QueryByPage(requestParameter);
            return new ResponseMessageWrap<QueryByPageResponse<UsersEntity>>
            {
                Body =  new QueryByPageResponse<UsersEntity>
                {
                    Total = total,
                    List = list
                }
            };
        }
    }
}