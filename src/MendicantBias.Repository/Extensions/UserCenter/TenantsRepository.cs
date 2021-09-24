using MendicantBias.Entity;
using SmartSql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MendicantBias.Repository.Extensions.UserCenter
{
    public class TenantsRepository :BaseRepository, ITenantsRepository
    {
        public async Task<int> DeleteById(int id)
        {
            using (var dbSession = SmartSqlBuilder.GetDbSessionFactory().Open())
            {
                return await dbSession.ExecuteScalarAsync<int>(new RequestContext
                {
                    Scope = nameof(TenantsEntity),
                    SqlId = "Delete",
                    Request = id
                });
            }
        }

        public async Task<int> GetRecord(object reqParams)
        {
            using (var dbSession = SmartSqlBuilder.GetDbSessionFactory().Open())
            {
                return await dbSession.ExecuteScalarAsync<int>(new RequestContext
                {
                    Scope = nameof(TenantsEntity),
                    SqlId = "GetRecord",
                    Request = reqParams
                });
            }
        }

        public async Task<IList<TenantsEntity>> QueryByPage(object reqParams)
        {
            using (var dbSession = SmartSqlBuilder.GetDbSessionFactory().Open())
            {
                return await dbSession.QueryAsync<TenantsEntity>(new RequestContext
                {
                    Scope = nameof(TenantsEntity),
                    SqlId = "QueryByPage",
                    Request = reqParams
                });
            }
        }

        public async Task<long> Insert(TenantsEntity users)
        {
            using (var dbSession = SmartSqlBuilder.GetDbSessionFactory().Open())
            {
                return await dbSession.ExecuteScalarAsync<long>(new RequestContext
                {
                    Scope = nameof(TenantsEntity),
                    SqlId = "Insert",
                    Request = users
                });
            }
        }

        public async Task<TenantsEntity> ValidateTenant(string tenantCode)
        {
            using (var dbSession = SmartSqlBuilder.GetDbSessionFactory().Open())
            {
                return await dbSession.QuerySingleAsync<TenantsEntity>(new RequestContext
                {
                    Scope = nameof(TenantsEntity),
                    SqlId = "ValidateTenant",
                    Request = new 
                    {
                        TenantCode = tenantCode,
                        Status = (int)TenantStatus.正常,
                        ExpirationTime = DateTime.Now
                    }
                });
            }
        }

        public async Task<int> Update(TenantsEntity users)
        {
            using (var dbSession = SmartSqlBuilder.GetDbSessionFactory().Open())
            {
                return await dbSession.ExecuteScalarAsync<int>(new RequestContext
                {
                    Scope = nameof(TenantsEntity),
                    SqlId = "Update",
                    Request = users
                });
            }
        }
    }
}
