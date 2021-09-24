using System;
using System.Collections.Generic;
using System.Text;

namespace MendicantBias.Entity
{
    public enum TenantStatus
    {
        正常 = 1,
        已过期,
        禁用
    }

    public enum UserStatus
    {
        正常 = 1,
        禁用
    }
}
