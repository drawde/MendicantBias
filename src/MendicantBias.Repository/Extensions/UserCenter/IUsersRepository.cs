using MendicantBias.Entity;
using MendicantBias.Entity.UserCenter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MendicantBias.Repository
{
    public interface IUsersRepository
    {
        Task<long> Insert(UsersEntity users);
        Task<long> DeleteById(long id);
        Task<int> Update(UsersEntity users);
        Task<IList<UsersEntity>> QueryByPage(object reqParams);
        Task<int> GetRecord(object reqParams);

        Task<UsersEntity> GetEntity(string userName, string tenantCode);
        Task<UsersEntity> CheckLogin(string userName, string tenantCode, string password);
    }
}
