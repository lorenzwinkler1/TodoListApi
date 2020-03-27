using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TodoClassLibEF;
using TodoClassLib;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

namespace TodoListWebApi
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
            services.AddControllers()
                .AddNewtonsoftJson();
            services.AddScoped<ITodoRepository, TodoSqliteRepository>();
            services.AddDbContext<DataContext>();
            services.AddCors(options =>
            {
                options.AddPolicy("allowall",
                builder =>
                {
                    builder.WithOrigins("http://localhost:8061")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("allowall");
            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            //app.Use((context, next) =>
            //           {
            //               context.Response.Headers["Access-Control-Allow-Origin"] = "http://localhost:8061";
            //               context.Response.Headers["Access-Control-Allow-Methods"] = "GET, PUT, POST, DELETE, OPTIONS";
            //               context.Response.Headers["Access-Control-Allow-Headers"] = "*";
            //               return next.Invoke();
            //           });
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
