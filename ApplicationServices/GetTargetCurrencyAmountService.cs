using System;
using System.Threading.Tasks;
using CurrencyExchangeDomain;
using Ports.RepositoryExceptions;

namespace ApplicationServices
{
    public sealed class GetTargetCurrencyAmountService
    {
        private readonly GetTargetCurrencyAmountFromDatabaseService _getTargetCurrencyAmountFromDatabaseService;
        private readonly GetTargetCurrencyExchangeFromWebService _getTargetCurrencyExchangeFromWebService;
        private readonly InsertCurrencyExchangeService _insertCurrencyExchangeService;

        public GetTargetCurrencyAmountService(
            GetTargetCurrencyAmountFromDatabaseService getTargetCurrencyAmountFromDatabaseService,
            GetTargetCurrencyExchangeFromWebService getTargetCurrencyExchangeFromWebService,
            InsertCurrencyExchangeService insertCurrencyExchangeService)
        {
            _getTargetCurrencyAmountFromDatabaseService = getTargetCurrencyAmountFromDatabaseService;
            _getTargetCurrencyExchangeFromWebService = getTargetCurrencyExchangeFromWebService;
            _insertCurrencyExchangeService = insertCurrencyExchangeService;
        }

        public async Task<double> GetAmount(
            Currency baseCurrency, 
            Currency targetCurrency, 
            Amount baseCurrencyAmount)
        {
            var targetCurrencyAmount = 0.0;
            try
            {
                targetCurrencyAmount = 
                    await _getTargetCurrencyAmountFromDatabaseService
                        .GetAmount(baseCurrency, targetCurrency, baseCurrencyAmount);
            }
            catch (NoValidCurrencyExchangeRateException)
            {
                var currencyExchange = 
                    await _getTargetCurrencyExchangeFromWebService
                        .GetCurrencyExchangeFor(baseCurrency, targetCurrency, baseCurrencyAmount);
                await _insertCurrencyExchangeService.Insert(currencyExchange);
                targetCurrencyAmount = currencyExchange.TargetCurrencyAmount.AmountValue;
            }

            return targetCurrencyAmount;
        }
    }
}