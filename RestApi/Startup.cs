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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TravelListRepository;
using TravelListRepository.Restcountries;
using TravelListRepository.Sql;

namespace RestApi
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
            DotNetEnv.Env.Load();

            string TravelListConnection =
            "Server=" + System.Environment.GetEnvironmentVariable("TRAVEL_LIST_SERVER") +
            ";Initial Catalog=" + System.Environment.GetEnvironmentVariable("TRAVEL_LIST_INITIAL_CATALOG") +
            ";User ID=" + System.Environment.GetEnvironmentVariable("TRAVEL_LIST_USER_ID") +
            ";Password=" + System.Environment.GetEnvironmentVariable("TRAVEL_LIST_PASSWORD") +
            ";";

            services.AddDbContext<TravelListContext>(opt => opt.UseSqlServer
            (TravelListConnection, b => b.MigrationsAssembly("TravelList.Api")));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<ITravelListItemRepo, SqlTravelListItemRepo>();
            services.AddScoped<ICountryRepo, RestCountriesRepo>();
            services.AddScoped<ITravelPointOfInterestRepo, SqlTravelPointOfInterestRepo>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
