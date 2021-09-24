using Autofac;
using MendicantBias.Util;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MendicantBias.TenantAdmin
{
    public class AutofacServiceExtProvider : Autofac.Module
    {
        //重写Autofac管道Load方法，在这里注册注入
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(GetAssemblyByName("MendicantBias.Util")).AsImplementedInterfaces().InstancePerDependency();
        }
        /// <summary>
        /// 根据程序集名称获取程序集
        /// </summary>
        /// <param name="AssemblyName">程序集名称</param>
        /// <returns></returns>
        public static Assembly GetAssemblyByName(string AssemblyName)
        {
            return Assembly.Load(AssemblyName);
        }
    }
}
