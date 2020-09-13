using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mutantes.Core.Interfaces;
using Mutantes.Core.Interfaces.Dna;
using Mutantes.Core.Interfaces.Utilities;
using Mutantes.Core.Services;
using Mutantes.Core.Services.Dna;
using Mutantes.Core.Utilities;
using Mutantes.Infraestructura.Data;
using Mutantes.Infraestructura.Interfaces;
using Mutantes.Infraestructura.Repositories;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Configuration;

namespace Mutantes.API
{
    public class Startup
    {
        IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MutantsDbContext>(optionsBuilder =>
               optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnectionString")));

            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddTransient<IMatrixUtilities, MatrixUtilities>();
            services.AddTransient<IDnaAnalyzedRepository, DnaAnalyzedRepository>();
            services.AddTransient<IStatsRepository, StatsRepository>();
            services.AddTransient<IDnaAnalyzerService, DnaAnalyzerService>();
            services.AddTransient<IStatsService, StatsService>();
            services.AddTransient<IDnaSaverService, DnaSaverService>();
            services.AddTransient<IDnaAnalyzerAlgorithm, DnaAnalyzerAlgorithm>();
            services.AddSingleton<IConnectionMultiplexer>(x => ConnectionMultiplexer.Connect("muntantes-diaznicolas.redis.cache.windows.net:6380,password=HeXWGH4sSqpK08XT9N3RpTfB9aVz3SqjHt7oX3OM8Xk=,ssl=True,abortConnect=False"));
            services.AddTransient<ICacheService, RedisCacheService>();

           

        }

       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
