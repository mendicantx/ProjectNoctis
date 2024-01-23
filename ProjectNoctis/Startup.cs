using AutoMapper;
using Discord;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectNoctis.Domain.Models;
using ProjectNoctis.Domain.Repository.Concrete;
using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Domain.SheetDatabase;
using ProjectNoctis.Factories.Concrete;
using ProjectNoctis.Factories.Interfaces;
using ProjectNoctis.Services.Concrete;
using ProjectNoctis.Services.Interfaces;
using ProjectNoctis.Services.Models;
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
            services.AddSingleton<IFfrkSheetContext, FfrkSheetContext>();
            services.AddSingleton<Aliases>();
            services.AddSingleton<Settings>();

            services.AddTransient<ICharacterService, CharacterService>();
            services.AddTransient<IAbilityService, AbilityService>();
            services.AddTransient<IHeroAbilityService, HeroAbilityService>();
            services.AddTransient<IZenithAbilityService, ZenithAbilityService>();
            services.AddTransient<ICrystalForceAbilityService, CrystalForceAbilityService>();
            services.AddTransient<ISoulbreakService, SoulbreakService>();
            services.AddTransient<IDiveService, DiveService>();
            services.AddTransient<IMagiciteService, MagiciteService>();
            services.AddTransient<IStatusService, StatusService>();
            services.AddTransient<IMateriaService, MateriaService>();

            services.AddTransient<ICharacterRepository, CharacterRepository>();
            services.AddTransient<IDiveRepository, DiveRepository>();
            services.AddTransient<ISoulbreakRepository, SoulbreakRepository>();
            services.AddTransient<IMagiciteRepository, MagiciteRepository>();
            services.AddTransient<IAbilityRepository, AbilityRepository>();
            services.AddTransient<IHeroAbilityRepository, HeroAbilityRepository>();
            services.AddTransient<IZenithAbilityRepository, ZenithAbilityRepository>();
            services.AddTransient<ICrystalForceAbilityRepository, CrystalForceAbilityRepository>();
            services.AddTransient<IStatusRepository, StatusRepository>();
            services.AddTransient<IMateriaRepository, MateriaRepository>();

            services.AddTransient<IEmbedBuilderFactory, EmbedBuilderFactory>();
            services.AddMvc(option => option.EnableEndpointRouting = false);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var client = new BotService(app.ApplicationServices);
            await client.RunBotAsync();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMvc(routes =>
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}"));
        }
    }
}

