using MendicantBias.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MendicantBias.Service
{
    public interface IUsersService
    {
        Task<long> Insert(UsersEntity users);
        Task<long> DeleteById(long id);
        Task<int> Update(UsersEntity users);
        Task<IList<UsersEntity>> QueryByPage(object reqParams);
        Task<int> GetRecord(object reqParams);
    }
}
