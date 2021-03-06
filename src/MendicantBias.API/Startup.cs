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
                        ValidateIssuer = true,//????????Issuer
                        ValidateAudience = true,//????????Audience
                        ValidateLifetime = true,//????????????????
                        ClockSkew = TimeSpan.FromSeconds(30),
                        ValidateIssuerSigningKey = true,//????????SecurityKey
                        ValidAudience = ApiWebConfig.Instance.Auth.JWTAudience,//Audience
                        ValidIssuer = ApiWebConfig.Instance.Auth.JWTAudience,//Issuer??????????????????jwt??????????
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ApiWebConfig.Instance.Auth.SecretKey))//????SecurityKey
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

                //????????????
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                //??header??????token????????????
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT????(????????????????????????)??????????????????Bearer {token}(??????????????????????) \"",
                    Name = "Authorization",//jwt??????????????
                    In = ParameterLocation.Header,//jwt????????Authorization??????????(????????)
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
            //??????Autofac????
            var builder = new ContainerBuilder();
            //??Services??????????????Autofac??
            builder.Populate(services);
            //??????????????    
            //builder.RegisterModule<AutofacServiceExtProvider>();
            //????????
            var Container = builder.Build();
            //??????IOC???? core????DI???? 
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
            //app.UseAuthentication();//????//??????????????
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