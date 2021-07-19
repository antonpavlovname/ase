using System.Collections.Generic;
using MatchAgregationService.Services;
using MatchAgregationServiceTests;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MatchAgregationService
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

            services.AddTransient<ISet<TeamResult>, HashSet<TeamResult>>();

            services.AddSingleton<IMatchesResultLoader, MatchesResultLoader>();
            services.AddTransient<IMatchResultClient, MatchResultClient>();
            services.AddSingleton<ITeamResultStorage, TeamResultStorage>();
            services.AddTransient<IResultParser, ResultParser>();
            services.AddSingleton<ITeamStatistic, TeamStatistic>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}