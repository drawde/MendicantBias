using AutoMapper;
using MendicantBias.Entity;
using MendicantBias.Entity.UserCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MendicantBias.TenantAdmin
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<UsersEntity, UsersModel>();
            CreateMap<UsersModel, UsersEntity>();
        }
    }
}
