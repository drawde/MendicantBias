using System;
using System.Collections.Generic;
using System.Text;

namespace MendicantBias.Util
{
    public class ApiWebConfig
    {
        private static ApiWebConfig _instance { get; set; }
        private ApiWebConfig()
        {

        }
        public static ApiWebConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ApiWebConfig
                    {
                        Auth = new Auth()
                    };
                }
                return _instance;
            }
        }
        public Auth Auth { get; set; }
    }
    public class Auth
    {
        public string SecretKey { get; set; }
        public string JWTAudience { get; set; }
    }
}
