//*******************************
// Create By Drawde
// Date 2021-08-25 14:55
// Code Generate By SmartCode
// Code Generate Github : https://github.com/Ahoo-Wang/SmartCode
//*******************************

using System;
namespace MendicantBias.Entity
{

    ///<summary>
    /// Table, Tenants
    ///</summary>
    public class TenantsEntity
    {
        ///<summary>
        /// Id, int
        ///</summary>
        public virtual int Id { get; set; }
        ///<summary>
        /// TenantCode, varchar
        ///</summary>
        public virtual string TenantCode { get; set; }
        ///<summary>
        /// TenantName, varchar
        ///</summary>
        public virtual string TenantName { get; set; }
        ///<summary>
        /// TenantIcon, varchar
        ///</summary>
        public virtual string TenantIcon { get; set; }
        ///<summary>
        /// Telephone, varchar
        ///</summary>
        public virtual string Telephone { get; set; }
        ///<summary>
        /// ExpirationTime, datetime
        ///</summary>
        public virtual DateTime ExpirationTime { get; set; }
        ///<summary>
        /// AddTime, datetime
        ///</summary>
        public virtual DateTime AddTime { get; set; }
        ///<summary>
        /// Status, int
        ///</summary>
        public virtual int Status { get; set; }
    }
}