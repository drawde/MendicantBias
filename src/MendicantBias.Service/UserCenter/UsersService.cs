//*******************************
// Create By Drawde
// Date 2021-08-25 14:55
// Code Generate By SmartCode
// Code Generate Github : https://github.com/Ahoo-Wang/SmartCode
//*******************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MendicantBias.Entity;
using MendicantBias.Repository;

namespace MendicantBias.Service
{
    ///<summary>
    /// Table, Users
    ///</summary>
    public class UsersService: IUsersService
    {
        private readonly IUsersRepository _usersExtRepository;

        public UsersService(IUsersRepository usersExtRepository)
        {
            _usersExtRepository = usersExtRepository;
        }

        public async Task<long> Insert(UsersEntity users)
        {
            return await _usersExtRepository.Insert(users);
        }

        public async Task<long> DeleteById(long id)
        {
            return await _usersExtRepository.DeleteById(id);
        }

        public async Task<int> Update(UsersEntity users)
        {
            return await _usersExtRepository.Update(users);
        }

        public async Task<IList<UsersEntity>> QueryByPage(object reqParams)
        {
            return await _usersExtRepository.QueryByPage(reqParams);
        }

        public async Task<int> GetRecord(object reqParams)
        {
            return await _usersExtRepository.GetRecord(reqParams);
        }
    }
}