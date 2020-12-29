using System.Net.Http;
using ApplicationServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoAdapter;
using Ports;
using WebApi.Extensions;

namespace WebApi
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
            services.AddTransient<HttpClient>();
            services.AddSingleton<ICurrencyExchangeRepository>(
                sp => new CurrencyExchangeRepository(
                    Configuration.GetValue<string>("Database:Connection.String"),
                    Configuration.GetValue<string>("Database:Database.Name")));
            services.AddSingleton<GetAllAvailableCurrenciesFromDatabaseService>();
            services.AddSingleton<GetAllAvailableCurrenciesFromWebService>();
            services.AddSingleton<GetAllAvailableCurrenciesService>();
            services.AddSingleton<DatabaseInsertAllAvailableCurrenciesService>();
            services.AddSingleton<GetCurrencyService>();
            services.AddSingleton<GetTargetCurrencyAmountService>();
            services.AddSingleton<GetTargetCurrencyAmountFromDatabaseService>();
            services.AddSingleton<GetLatestRatesFromWebService>();
            services.AddSingleton<InsertLatestRatesService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            
            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}