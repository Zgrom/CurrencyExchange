using System.Net.Http;
using ApplicationServices;
using FixerAdapter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoAdapter;
using Ports;
using Quartz;
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
            services.AddSingleton<ICurrencyExchangeWebServicePort>(
                sp => new CurrencyExchangeWebServiceFixerAdapter(
                    Configuration.GetValue<string>("Fixer.Io.Uris:Symbols"),
                    Configuration.GetValue<string>("Fixer.Io.Uris:Latest")));
            services.AddSingleton<DatabaseGetAllAvailableCurrenciesService>();
            services.AddSingleton<DatabaseInsertAllAvailableCurrenciesService>();
            services.AddSingleton<DatabaseDeleteAllAvailableCurrenciesService>();
            services.AddSingleton<DatabaseGetLatestRatesService>();
            services.AddSingleton<DatabaseInsertLatestRatesService>();
            services.AddSingleton<DatabaseDeleteLatestRatesService>();
            services.AddSingleton<WebGetAllAvailableCurrenciesService>();
            services.AddSingleton<WebGetLatestRatesService>();
            services.AddSingleton<InitializeDatabaseService>();
            services.AddSingleton<DatabaseInsertAllAvailableCurrenciesService>();
            services.AddSingleton<GetTargetCurrencyAmountService>();
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
                var jobKey = new JobKey("InitializeDatabaseJob");
                q.AddJob<InitializeDatabaseService>(opts => opts.WithIdentity(jobKey));
                q.AddTrigger(opts => opts
                    .ForJob(jobKey) 
                    .WithIdentity("InitializeDatabasedJob-trigger")
                    .StartNow()
                    .WithSimpleSchedule(
                        ssb => ssb.WithIntervalInMinutes(1).RepeatForever()));
            });
            services.AddQuartzServer(options =>
            {
                options.WaitForJobsToComplete = true;
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