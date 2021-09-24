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
    public class ResponseMessage : ResponseMessageWrap<object>
    {
    }
    public class ResponseMessageWrap<TBody>
    {
    public bool IsSuccess { get; set; } = true;
    public String ErrorCode { get; set; } = "0000";
    public String Message { get; set; }
    public TBody Body { get; set; }
    }
    }