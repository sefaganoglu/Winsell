using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Winsell.Hopi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Winsell.Hopi.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<HopiContext>(options => options.UseSqlServer(Configuration["ConnectionString"].ToString()));
            Genel.connectionString = Configuration["ConnectionString"].ToString();
            services.AddMvc();

            Genel.hopiWSDL = Configuration["HopiWSDL"].ToString();

            Genel.hopiIndirimOdemeKodu = Genel.odemeTipiGetir(Genel.OdemeTipi.HopiIndirim);
            Genel.hopiOdemeKodu = Genel.odemeTipiGetir(Genel.OdemeTipi.HopiTahsilat);
            Genel.magazaKodu = Genel.parametreOku("MAGAZA_KODU", "").TOSTRING();
            Genel.storeCode = "brs1"; //clsGenel.magazaKodu;

            Genel.kampanyaServerName = Genel.parametreOku("SERVER_NAME", "").TOSTRING();
            Genel.kampanyaDatabaseName = Genel.parametreOku("DATABASE_NAME", "").TOSTRING();
            Genel.kampanyaUserName = Genel.parametreOku("USER_NAME", "").TOSTRING();
            Genel.kampanyaPassword = Genel.parametreOku("PASSWORD", "").TOSTRING();

            Genel.kullaniciKoduWS = Genel.parametreOku("HOPI_USERNAME", "").TOSTRING();
            Genel.sifreWS = Genel.parametreOku("HOPI_PASSWORD", "").TOSTRING();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
