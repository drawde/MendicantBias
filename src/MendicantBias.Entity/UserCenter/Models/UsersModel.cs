using System;
using System.Collections.Generic;
using System.Text;

namespace MendicantBias.Entity.UserCenter
{
    public class UsersModel
    {
        ///<summary>
        /// TenantCode, varchar
        ///</summary>
        public virtual string TenantCode { get; set; }
        ///<summary>
        /// UserName, varchar
        ///</summary>
        public virtual string UserName { get; set; }
        ///<summary>
        /// HeadIcon, varchar
        ///</summary>
        public virtual string HeadIcon { get; set; }
        ///<summary>
        /// Status, int
        ///</summary>
        public virtual int Status { get; set; }
        ///<summary>
        /// NickName, nvarchar
        ///</summary>
        public virtual string NickName { get; set; }
        ///<summary>
        /// AddTime, datetime
        ///</summary>
        public virtual DateTime AddTime { get; set; }
        ///<summary>
        /// LastLoginTime, datetime
        ///</summary>
        public virtual DateTime LastLoginTime { get; set; }
        public string Token { get; set; }
    }
}
