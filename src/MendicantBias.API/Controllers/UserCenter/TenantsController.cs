//*******************************
// Create By Ahoo Wang
// Date 2021-08-24 14:59
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
using MendicantBias.Util;
namespace MendicantBias.API.Controllers
{
    ///<summary>
    /// Table, Tenants
    ///</summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class TenantsController : Controller
    {
        public TenantsService TenantsService { get; }
        public ITenantsRepository TenantsRepository { get; }
        public TenantsController(TenantsService tenantsService, ITenantsRepository tenantsRepository)
        {
            TenantsService = tenantsService;
            TenantsRepository = tenantsRepository;
        }
        [HttpPost]
        public async Task<ResponseMessageWrap<long>> Insert([FromBody] TenantsEntity tenants)
        {
            return new ResponseMessageWrap<long>
            {
                Body = await TenantsService.Insert(tenants)
            };
        }
        [HttpDelete]
        public async Task<ResponseMessageWrap<long>> DeleteById(int id)
        {
            return new ResponseMessageWrap<long>
            {
                Body = await TenantsService.DeleteById(id)
            };
        }
        [HttpPut]
        public async Task<ResponseMessageWrap<int>> Update([FromBody] TenantsEntity tenants)
        {
            return new ResponseMessageWrap<int>
            {
                Body = await TenantsService.Update(tenants)
            };
        }

    }
}