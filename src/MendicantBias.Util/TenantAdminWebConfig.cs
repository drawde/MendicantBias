using System;
using System.Collections.Generic;
using System.Text;

namespace MendicantBias.Util
{
    public class TenantAdminWebConfig
    {
        private static TenantAdminWebConfig _instance { get; set; }
        public static TenantAdminWebConfig Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new TenantAdminWebConfig
                    {
                        MendicantBiasAPI = new MendicantBiasAPI(),
                        Auth = new Auth()
                    };
                }
                return _instance;
            }
        }
        private TenantAdminWebConfig()
        {

        }
        public MendicantBiasAPI MendicantBiasAPI { get; set; }
        public Auth Auth { get; set; }
    }
    public class MendicantBiasAPI
    {
        public string URL { get; set; }        
    }
}
