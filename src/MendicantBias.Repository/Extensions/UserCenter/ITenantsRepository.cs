//*******************************
// Create By Drawde
// Date 2021-08-25 14:55
// Code Generate By SmartCode
// Code Generate Github : https://github.com/Ahoo-Wang/SmartCode
//*******************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using SmartSql.DyRepository;
using SmartSql.DyRepository.Annotations;
using MendicantBias.Entity;

namespace MendicantBias.Repository
{
    ///<summary>
    /// Table, Tenants
    ///</summary>
    public interface ITenantsRepository
    {
        Task<long> Insert(TenantsEntity users);
        Task<int> DeleteById(int id);
        Task<int> Update(TenantsEntity users);
        Task<IList<TenantsEntity>> QueryByPage(object reqParams);
        Task<int> GetRecord(object reqParams);
        Task<TenantsEntity> ValidateTenant(string tenantCode);
    }
}