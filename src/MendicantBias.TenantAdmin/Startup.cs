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
            //    //��¼·�������ǵ��û���ͼ������Դ��δ���������֤ʱ�����򽫻Ὣ�����ض���������·��
            //    o.LoginPath = new PathString("/Account/Login");

            //    //��ֹ����·�������û���ͼ������Դʱ����δͨ������Դ���κ���Ȩ���ԣ����󽫱��ض���������·����
            //    o.AccessDeniedPath = new PathString("/Home/Privacy");
            //});
            Configuration.GetSection(nameof(TenantAdminWebConfig.Instance.MendicantBiasAPI)).Bind(TenantAdminWebConfig.Instance.MendicantBiasAPI);
            Configuration.GetSection(nameof(TenantAdminWebConfig.Instance.Auth)).Bind(TenantAdminWebConfig.Instance.Auth);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,//�Ƿ���֤Issuer
                        ValidateAudience = true,//�Ƿ���֤Audience
                        ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                        ClockSkew = TimeSpan.FromSeconds(30),
                        ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                        ValidAudience = TenantAdminWebConfig.Instance.Auth.JWTAudience,//Audience
                        ValidIssuer = TenantAdminWebConfig.Instance.Auth.JWTAudience,//Issuer���������ǰ��ǩ��jwt������һ��
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TenantAdminWebConfig.Instance.Auth.SecretKey))//�õ�SecurityKey
                    };

                }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    //��¼·�������ǵ��û���ͼ������Դ��δ���������֤ʱ�����򽫻Ὣ�����ض���������·��
                    o.LoginPath = new PathString("/Account/Login");

                    //��ֹ����·�������û���ͼ������Դʱ����δͨ������Դ���κ���Ȩ���ԣ����󽫱��ض���������·����
                    o.AccessDeniedPath = new PathString("/Home/Privacy");
                });

                //ʵ����Autofac����
                RegisterAutofac(services);
            services.AddAutoMapper(typeof(AutomapperConfig));
            
        }

        private IServiceProvider RegisterAutofac(IServiceCollection services)
        {
            //ʵ����Autofac����
            var builder = new ContainerBuilder();
            //��Services�еķ�����䵽Autofac��
            builder.Populate(services);
            //��ģ�����ע��    
            //builder.RegisterModule<AutofacServiceExtProvider>();
            //��������
            var Container = builder.Build();
            //������IOC�ӹ� core����DI���� 
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
