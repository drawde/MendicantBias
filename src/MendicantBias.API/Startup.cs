//*******************************
// Create By Ahoo Wang
// Date 2021-08-24 14:59
// Code Generate By SmartCode
// Code Generate Github : https://github.com/Ahoo-Wang/SmartCode
//*******************************
using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using MendicantBias.Service;
using MendicantBias.API.Filters;
using SmartSql;
using MendicantBias.Repository;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using log4net.Repository;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MendicantBias.Util;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Models;

namespace MendicantBias.API
{
    public class Startup
    {
        public static ILoggerRepository LoggerRepository { get; set; }
        const string SERVICE_NAME = "MendicantBias.API";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            LoggerRepository = LogManager.CreateRepository(SERVICE_NAME);
            XmlConfigurator.Configure(LoggerRepository, new FileInfo("log4net.config"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            Configuration.GetSection(nameof(ApiWebConfig.Instance.Auth)).Bind(ApiWebConfig.Instance.Auth);
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
                        ValidAudience = ApiWebConfig.Instance.Auth.JWTAudience,//Audience
                        ValidIssuer = ApiWebConfig.Instance.Auth.JWTAudience,//Issuer，这两项和前面签发jwt的设置一致
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ApiWebConfig.Instance.Auth.SecretKey))//拿到SecurityKey
                    };

                });

            services.AddMvc(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
                options.Filters.Add<GlobalValidateModelFilter>();
                options.EnableEndpointRouting = false;
            }).SetCompatibilityVersion(CompatibilityVersion.Latest);
            RegisterRepository(services);
            RegisterAutofac(services);
            RegisterSwagger(services);
            services.AddAutoMapper(typeof(AutomapperConfig));
            
            BaseRepository.SmartSqlBuilder = new SmartSqlBuilder().UseXmlConfig().Build();            
        }
        private void RegisterRepository(IServiceCollection services)
        {
            services.AddSmartSql((sp, builder) =>
            {
                builder.UseAlias(DateTime.Now.Ticks.ToString());
            });
            //.AddRepositoryFromAssembly(options =>
            //{
            //    options.AssemblyString = "MendicantBias.Repository";
            //});
        }
        private void RegisterService(IServiceCollection services)
        {
            var assembly = Assembly.Load("MendicantBias.Service");
            var allTypes = assembly.GetTypes();
            foreach (var type in allTypes)
            {
                services.AddSingleton(type);
            }
        }

        private void RegisterSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = SERVICE_NAME,
                    Version = "v1",
                });
                c.CustomSchemaIds((type) => type.FullName);
                var filePath = Path.Combine(AppContext.BaseDirectory, $"{SERVICE_NAME}.xml");
                if (File.Exists(filePath))
                {
                    c.IncludeXmlComments(filePath);
                }

                //开启权限小锁
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                //在header中添加token，传递到后台
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传递)直接在下面框中输入Bearer {token}(注意两者之间是一个空格) \"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });

            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacServiceExtProvider());
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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();
            ConfigureSwagger(app);
            //app.UseAuthentication();//认证//身份验证中间件
        }
        private void ConfigureSwagger(IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {

            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", SERVICE_NAME);
            });
        }
    }
}