//*******************************
// Create By Drawde
// Date 2021-08-25 14:55
// Code Generate By SmartCode
// Code Generate Github : https://github.com/Ahoo-Wang/SmartCode
//*******************************
using System;
using System.Linq;
using System.Threading.Tasks;
using MendicantBias.Entity;
using MendicantBias.Repository;

namespace MendicantBias.Service
{
    ///<summary>
    /// Table, Tenants
    ///</summary>
    public class TenantsService
    {
        public ITenantsRepository TenantsRepository { get; }

        public TenantsService(ITenantsRepository tenantsRepository)
        {
            TenantsRepository = tenantsRepository;
        }

        public async Task<long> Insert(TenantsEntity tenants)
        {
            return await TenantsRepository.Insert(tenants);
        }

        public async Task<long> DeleteById(int id)
        {
            return await TenantsRepository.DeleteById(id);
        }

        public async Task<int> Update(TenantsEntity tenants)
        {
            return await TenantsRepository.Update(tenants);
        }

    }
}