//*******************************
// Create By Ahoo Wang
// Date 2021-08-24 14:59
// Code Generate By SmartCode
// Code Generate Github : https://github.com/Ahoo-Wang/SmartCode
//*******************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MendicantBias.Util
{
    public class QueryRequest
    {
        [Range(1, 100)]
        public int Taken { get; set; }
    }
}