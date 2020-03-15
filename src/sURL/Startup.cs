using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using sURL.Services;
using sURL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace sURL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configration = configuration;
        }

        public IConfiguration Configration {get;}

        // This method gets called by the runtime. 
        // Use this method to add services to the container.
        // For more information on how to configure your application, 
        // visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHashidsService, HashidsService>();
            services.AddSingleton<ICodingService, CodingService>();
            services.AddControllers();

            // Use Sqlite to store URLs.
            services.AddDbContext<UrlRecordContext>(bilder => bilder.UseSqlite(
                Configration.GetConnectionString(Constants.SqliteConnectionStringKey)
            ));
        }

        // This method gets called by the runtime. 
        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
