

using System.Threading.Tasks;
using ApplicationServices;
using Microsoft.AspNetCore.Http;

namespace WebApi.StartupAndScheduleMiddleware
{
    public class StartupAndScheduleMiddleware
    {
        private readonly DatabaseGetAllAvailableCurrenciesService _databaseGetAllAvailableCurrenciesService;
        private readonly DatabaseInsertAllAvailableCurrenciesService _databaseInsertAllAvailableCurrenciesService;
        private readonly DatabaseDeleteAllAvailableCurrenciesService _databaseDeleteAllAvailableCurrenciesService;
        private readonly DatabaseGetLatestRatesService _databaseGetLatestRatesService;
        private readonly DatabaseInsertLatestRatesService _databaseInsertLatestRatesService;
        private readonly DatabaseDeleteLatestRatesService _databaseDeleteLatestRatesService;
        private readonly WebGetAllAvailableCurrenciesService _webGetAllAvailableCurrenciesService;
        private readonly WebGetLatestRatesService _webGetLatestRatesService;

        public StartupAndScheduleMiddleware(
            DatabaseGetAllAvailableCurrenciesService databaseGetAllAvailableCurrenciesService,
            DatabaseInsertAllAvailableCurrenciesService databaseInsertAllAvailableCurrenciesService,
            DatabaseDeleteAllAvailableCurrenciesService databaseDeleteAllAvailableCurrenciesService,
            DatabaseGetLatestRatesService databaseGetLatestRatesService,
            DatabaseInsertLatestRatesService databaseInsertLatestRatesService,
            DatabaseDeleteLatestRatesService deleteLatestRatesService,
            WebGetAllAvailableCurrenciesService webGetAllAvailableCurrenciesService,
            WebGetLatestRatesService webGetLatestRatesService)
        {
            _databaseGetAllAvailableCurrenciesService = databaseGetAllAvailableCurrenciesService;
            _databaseInsertAllAvailableCurrenciesService = databaseInsertAllAvailableCurrenciesService;
            _databaseDeleteAllAvailableCurrenciesService = databaseDeleteAllAvailableCurrenciesService;
            _databaseGetLatestRatesService = databaseGetLatestRatesService;
            _databaseInsertLatestRatesService = databaseInsertLatestRatesService;
            _databaseDeleteLatestRatesService = deleteLatestRatesService;
            _webGetAllAvailableCurrenciesService = webGetAllAvailableCurrenciesService;
            _webGetLatestRatesService = webGetLatestRatesService;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if ((await _databaseGetAllAvailableCurrenciesService.GetAll()).Count != 0)
            {
                await _databaseDeleteAllAvailableCurrenciesService.DeleteAll();
            }

            var allAvailableCurrencies = await _webGetAllAvailableCurrenciesService.GetAll();
            await _databaseInsertAllAvailableCurrenciesService.InsertAll(allAvailableCurrencies);

            var lr = await _databaseGetLatestRatesService.GetAll();
            if (lr != null)
            {
                await _databaseDeleteLatestRatesService.DeleteAll(lr);
            }

            var latestRates = await _webGetLatestRatesService.GetAll();
            await _databaseInsertLatestRatesService.InsertAll(latestRates);
        }
    }
}