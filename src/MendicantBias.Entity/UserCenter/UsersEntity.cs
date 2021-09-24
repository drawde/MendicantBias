//*******************************
// Create By Drawde
// Date 2021-08-25 14:55
// Code Generate By SmartCode
// Code Generate Github : https://github.com/Ahoo-Wang/SmartCode
//*******************************

using System;
using System.ComponentModel;

namespace MendicantBias.Entity
{

    ///<summary>
    /// Table, Users
    ///</summary>
    public class UsersEntity
    {
        ///<summary>
        /// Id, bigint
        ///</summary>
        public virtual long Id { get; set; }
        ///<summary>
        /// TenantCode, varchar
        ///</summary>
        public virtual string TenantCode { get; set; }
        ///<summary>
        /// UserName, varchar
        ///</summary>
        public virtual string UserName { get; set; }
        ///<summary>
        /// Password, varchar
        ///</summary>
        public virtual string Password { get; set; }
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
        /// SecretKey, varchar
        ///</summary>
        public virtual string SecretKey { get; set; }
        ///<summary>
        /// AddTime, datetime
        ///</summary>
        public virtual DateTime AddTime { get; set; }
        ///<summary>
        /// LastLoginTime, datetime
        ///</summary>
        public virtual DateTime LastLoginTime { get; set; }
    }
}