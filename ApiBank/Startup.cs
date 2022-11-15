using ApiBank.DAL;
using ApiBank.Service.Implementation;
using ApiBank.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ApiBank
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
            services.AddDbContext<ApiBankDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("ApiBankDbConnection")));

            services.AddScoped<IAccountService, AccountService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                     Title = "My Bank API Learn",
                     Version = "v1",
                     Description = "Crazy enough to build a Bank API",
                     Contact = new Microsoft.OpenApi.Models.OpenApiContact
                     {
                         Name = "Olusegun AKinnola",
                         Email = "shegezzy@gmail.com",
                         Url = new Uri("https://github.com/shegezzy")
                     }



                });
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                var prefix = string.IsNullOrEmpty(x.RoutePrefix) ? "." : "..";
                x.SwaggerEndpoint($"{prefix}/swagger/v1/swagger.json", "My Bank API Learn");

            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
