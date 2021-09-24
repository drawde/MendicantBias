using Autofac;
using Autofac.Extensions.DependencyInjection;
using IdentityModel;
using MendicantBias.Util;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MendicantBias.TenantAdmin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddHttpClient();
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
            //{
            //    //登录路径：这是当用户试图访问资源但未经过身份验证时，程序将会将请求重定向到这个相对路径
            //    o.LoginPath = new PathString("/Account/Login");

            //    //禁止访问路径：当用户试图访问资源时，但未通过该资源的任何授权策略，请求将被重定向到这个相对路径。
            //    o.AccessDeniedPath = new PathString("/Home/Privacy");
            //});
            Configuration.GetSection(nameof(TenantAdminWebConfig.Instance.MendicantBiasAPI)).Bind(TenantAdminWebConfig.Instance.MendicantBiasAPI);
            Configuration.GetSection(nameof(TenantAdminWebConfig.Instance.Auth)).Bind(TenantAdminWebConfig.Instance.Auth);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,//是否验证Issuer
                        ValidateAudience = true,//是否验证Audience
                        ValidateLifetime = true,//是否验证失效时间
                        ClockSkew = TimeSpan.FromSeconds(30),
                        ValidateIssuerSigningKey = true,//是否验证SecurityKey
                        ValidAudience = TenantAdminWebConfig.Instance.Auth.JWTAudience,//Audience
                        ValidIssuer = TenantAdminWebConfig.Instance.Auth.JWTAudience,//Issuer，这两项和前面签发jwt的设置一致
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TenantAdminWebConfig.Instance.Auth.SecretKey))//拿到SecurityKey
                    };

                }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    //登录路径：这是当用户试图访问资源但未经过身份验证时，程序将会将请求重定向到这个相对路径
                    o.LoginPath = new PathString("/Account/Login");

                    //禁止访问路径：当用户试图访问资源时，但未通过该资源的任何授权策略，请求将被重定向到这个相对路径。
                    o.AccessDeniedPath = new PathString("/Home/Privacy");
                });

                //实例化Autofac容器
                RegisterAutofac(services);
            services.AddAutoMapper(typeof(AutomapperConfig));
            
        }

        private IServiceProvider RegisterAutofac(IServiceCollection services)
        {
            //实例化Autofac容器
            var builder = new ContainerBuilder();
            //将Services中的服务填充到Autofac中
            builder.Populate(services);
            //新模块组件注册    
            //builder.RegisterModule<AutofacServiceExtProvider>();
            //创建容器
            var Container = builder.Build();
            //第三方IOC接管 core内置DI容器 
            return new AutofacServiceProvider(Container);
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacServiceExtProvider());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();
            app.UseAuthorization();

            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
