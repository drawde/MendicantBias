//*******************************
// Create By Ahoo Wang
// Date 2021-08-24 14:59
// Code Generate By SmartCode
// Code Generate Github : https://github.com/Ahoo-Wang/SmartCode
//*******************************
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MendicantBias.API.Exceptions;
using System.Net;
using log4net;
using MendicantBias.Util;

namespace MendicantBias.API.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment env;
        private readonly ILog logger;

        public GlobalExceptionFilter(IHostingEnvironment env)
        {
            this.env = env;
            logger = LogManager.GetLogger(Startup.LoggerRepository.Name, typeof(GlobalExceptionFilter));
        }

        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            var exception = context.Exception;
            logger.Error(exception);
            string errorCode = "0001";
            if (exception is APIException) { errorCode = (exception as APIException).ErrorCode; }
            var errorResp = new ResponseMessage
            {
                Message = exception.Message,
                ErrorCode = errorCode,
                IsSuccess = false
            };
            var result = new JsonResult(errorResp)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
            context.Result = result;

        }
    }
}