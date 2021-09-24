using AutoMapper;
using MendicantBias.Entity;
using MendicantBias.Entity.UserCenter;
using SmartSql;
using SmartSql.DbSession;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MendicantBias.Repository
{
    public class UsersRepository : BaseRepository, IUsersRepository
    {
        public async Task<long> DeleteById(long id)
        {
            using var dbSession = SmartSqlBuilder.GetDbSessionFactory().Open();
            return await dbSession.ExecuteScalarAsync<long>(new RequestContext
            {
                Scope = nameof(UsersEntity),
                SqlId = "Delete",
                Request = id
            });
        }

        public async Task<int> GetRecord(object reqParams)
        {
            using var dbSession = SmartSqlBuilder.GetDbSessionFactory().Open();
            return await dbSession.ExecuteScalarAsync<int>(new RequestContext
            {
                Scope = nameof(UsersEntity),
                SqlId = "GetRecord",
                Request = reqParams
            });
        }

        public async Task<IList<UsersEntity>> QueryByPage(object reqParams)
        {
            using var dbSession = SmartSqlBuilder.GetDbSessionFactory().Open();
            return await dbSession.QueryAsync<UsersEntity>(new RequestContext
            {
                Scope = nameof(UsersEntity),
                SqlId = "QueryByPage",
                Request = reqParams
            });
        }

        public async Task<long> Insert(UsersEntity users)
        {
            using var dbSession = SmartSqlBuilder.GetDbSessionFactory().Open();
            return await dbSession.ExecuteScalarAsync<long>(new RequestContext
            {
                Scope = nameof(UsersEntity),
                SqlId = "Insert",
                Request = users
            });
        }

        

        public async Task<int> Update(UsersEntity users)
        {
            using var dbSession = SmartSqlBuilder.GetDbSessionFactory().Open();
            return await dbSession.ExecuteScalarAsync<int>(new RequestContext
            {
                Scope = nameof(UsersEntity),
                SqlId = "Update",
                Request = users
            });
        }

        public async Task<UsersEntity> GetEntity(string userName, string tenantCode)
        {
            using var dbSession = SmartSqlBuilder.GetDbSessionFactory().Open();
            return await dbSession.QuerySingleAsync<UsersEntity>(new RequestContext
            {
                Scope = nameof(UsersEntity),
                SqlId = "GetRecord",
                Request = new
                {
                    UserName = userName,
                    TenantCode = tenantCode
                }
            });
        }

        public async Task<UsersEntity> CheckLogin(string userName, string tenantCode, string password)
        {
            using var dbSession = SmartSqlBuilder.GetDbSessionFactory().Open();
            return await dbSession.QuerySingleAsync<UsersEntity>(new RequestContext
            {
                Scope = nameof(UsersEntity),
                SqlId = "CheckLogin",
                Request = new
                {
                    UserName = userName,
                    TenantCode = tenantCode,
                    Password = password,
                    Status = (int)UserStatus.正常
                }
            });
        }
    }
}
