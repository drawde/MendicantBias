using MendicantBias.Entity;
using MendicantBias.Entity.UserCenter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MendicantBias.Service
{
    public interface IUserAuthorizationService
    {
        Task<UsersEntity> Login(UserLoginParameter userLoginParameter);
    }
}
