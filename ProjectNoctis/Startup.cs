using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjectNoctis.Domain.Database;
using ProjectNoctis.Domain.Repository.Concrete;
using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Services.Concrete;
using ProjectNoctis.Services.Interfaces;
using ProjectNoctis.UtilFiles.AutoMapper;

namespace ProjectNoctis
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
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup),typeof(AutoMappingProfile));
            services.AddDbContext<FFRecordContext>();
            services.AddTransient<ISoulbreakManager, SoulbreakManager>();
            services.AddTransient<ICharacterRepository, CharacterRepository>();
            services.AddTransient<ISoulbreakRepository, SoulbreakRepository>();
            services.AddTransient<IMagiciteRepository, MagiciteRepository>();
            services.AddTransient<IStatusRepository, StatusRepository>();
            services.AddTransient<ISheetUpdateService, SheetUpdateService>();
            services.AddMvc(option => option.EnableEndpointRouting = false);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();


            app.UseAuthorization();

            app.UseMvc(routes =>
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}"));
        }
    }
}

