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
    public class QueryByPageRequest
    {
        [Range(1, int.MaxValue)]
        public int PageIndex { get; set; } = 1;
        [Range(1, 100)]
        public int PageSize { get; set; } = 10;
        public int Offset { get { return (PageIndex - 1) * PageSize; } }
    }
}