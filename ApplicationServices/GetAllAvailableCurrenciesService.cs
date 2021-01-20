using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyExchangeDomain;

namespace ApplicationServices
{
    public sealed class GetAllAvailableCurrenciesService
    {
        private readonly GetAllAvailableCurrenciesFromWebService _getAllAvailableCurrenciesFromWebService;
        private readonly DatabaseGetAllAvailableCurrenciesService _databaseGetAllAvailableCurrenciesService;
        private readonly DatabaseInsertAllAvailableCurrenciesService _databaseInsertAllAvailableCurrenciesService;

        public GetAllAvailableCurrenciesService(
            GetAllAvailableCurrenciesFromWebService getAllAvailableCurrenciesFromWebService,
            DatabaseGetAllAvailableCurrenciesService databaseGetAllAvailableCurrenciesService,
            DatabaseInsertAllAvailableCurrenciesService databaseInsertAllAvailableCurrenciesService)
        {
            _getAllAvailableCurrenciesFromWebService = getAllAvailableCurrenciesFromWebService;
            _databaseGetAllAvailableCurrenciesService = databaseGetAllAvailableCurrenciesService;
            _databaseInsertAllAvailableCurrenciesService = databaseInsertAllAvailableCurrenciesService;
        }

        public async Task<List<Currency>> GetAll()
        {
            var result = await _databaseGetAllAvailableCurrenciesService.GetAll();
            if (result.Count == 0)
            {
                result = await _getAllAvailableCurrenciesFromWebService.GetAll();
                await _databaseInsertAllAvailableCurrenciesService.InsertAll(result);
            }

            return result;
        }
    }
}