using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using WebApplication1.Services;

namespace WebApplication1
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
            //services.AddDbContext<ApplicationDBContext>(
            //    option=>option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ApplicationDBContext>(
                option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                ) ;

            //services.AddCors(options =>
            //    {
            //        options.AddDefaultPolicy(
            //            builder =>
            //            {
            //                var frontendurl = Configuration.GetValue<string>("front_endUrl");
            //                builder.WithOrigins(frontendurl).AllowAnyMethod().AllowAnyHeader();
            //            });
            //    });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    var frontenturld = Configuration.GetValue<string>("front_endUrl");
                    builder.WithOrigins("https://localhost:44388").AllowAnyMethod().AllowAnyHeader();
                });
            });
            // Add service of auto mapper
            services.AddAutoMapper(typeof (Startup));
            
            services.AddControllers();
            services.AddSingleton<IProducts, InMemoryProductsData>();
            services.AddSingleton<IEmp, InMemoryEmpData>();
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

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
