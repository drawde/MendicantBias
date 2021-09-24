using MendicantBias.Entity;
using MendicantBias.Entity.UserCenter;
using MendicantBias.Repository;
using MendicantBias.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MendicantBias.Service
{
    public class UserAuthorizationService : IUserAuthorizationService
    {
        private readonly ITenantsRepository _tenantsRepository;
        private readonly IUsersRepository _usersRepository;
        public UserAuthorizationService(ITenantsRepository tenantsRepository, IUsersRepository usersRepository)
        {
            _tenantsRepository = tenantsRepository;
            _usersRepository = usersRepository;
        }

        public async Task<UsersEntity> Login(UserLoginParameter userLoginParameter)
        {
            var tenant = await _tenantsRepository.ValidateTenant(userLoginParameter.TenantCode);
            if (tenant != null)
            {
                var user = await _usersRepository.GetEntity(userLoginParameter.UserName, userLoginParameter.TenantCode);
                if (user != null)
                {
                    string password = SecretString.EncryptionString(userLoginParameter.Password + user.SecretKey);
                    return await _usersRepository.CheckLogin(userLoginParameter.UserName, userLoginParameter.TenantCode, password);
                }
            }

            return null;
        }
    }
}
