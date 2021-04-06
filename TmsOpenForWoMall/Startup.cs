using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TmsOpenForWoMall.Domain.IServices;
using TmsOpenForWoMall.Domain.Services;
using TmsOpenForWoMall.Dto.Response.Union;
using TmsOpenForWoMall.Infrastructure.DbCommon;
using WmsReport.Infrastructure.Config;
using WmsReport.Infrastructure.DbCommon;
using Zh.Common.ApiRespose;

namespace WmsReport
{
    /// <summary>
    /// 配置开始文件
    /// </summary>
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //配置跨域处理
            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });
            //新增接口文件需要在此处注入
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "联通商城接口文档",
                        Version = "v1.0"
                    }
                 );

                var xmlfile = $"{ Assembly.GetEntryAssembly().GetName().Name}.xml";

                var xmlpath = Path.Combine(AppContext.BaseDirectory, xmlfile);
                c.IncludeXmlComments(xmlpath);
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:client_id"],
                    ValidAudience = Configuration["Jwt:client_secret"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:corp_id"]))
                };
            });
         
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

            //模型绑定 特性验证，自定义返回格式
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    //获取验证失败的模型字段 
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e => e.Value.Errors.First().ErrorMessage)
                    .ToList();
                    var str = string.Join("|", errors);
                    //设置返回内容
                    var result = new ResModel<ResMsgDto>();
                    result.result = new ResMsgDto { msg = str };
                    result.success = "false";
                    
                    return new BadRequestObjectResult(result);
                };
            });

            services.Configure<DbConnections>(Configuration.GetSection("DbConnections"));
            AddTransient(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
            app.UseCors("any");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TMS用户中心接口文档 V1.0");
            });
        }


        public void  AddTransient(IServiceCollection services)
        {
            services.AddTransient<IWmsDbConnection, WmsDbConnection>();
            services.AddTransient<IUnionDbConnection, UnionDbConnection>();
            services.AddTransient<IUnionService, UnionService>();

        }
    }
}
