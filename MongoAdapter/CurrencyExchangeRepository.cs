using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyExchangeDomain;
using MongoAdapter.DTO;
using MongoDB.Driver;
using Ports;
using Ports.RepositoryExceptions;

namespace MongoAdapter
{
    public sealed class CurrencyExchangeRepository : ICurrencyExchangeRepository
    {
        private readonly IMongoCollection<CurrencyExchangeDto> _currencyExchangeCollection;
        private readonly IMongoCollection<CurrencyDto> _availableCurrenciesCollection;
        
        public CurrencyExchangeRepository(string connectionUri, string databaseName)
        {
            var mongoClient = new MongoClient(connectionUri);
            var database = mongoClient.GetDatabase(databaseName);
            _currencyExchangeCollection = database
                .GetCollection<CurrencyExchangeDto>(nameof(CurrencyExchangeDto));
            _availableCurrenciesCollection = database
                .GetCollection<CurrencyDto>(nameof(CurrencyDto));
        }
        
        public async Task<CurrencyExchange> GetCurrencyExchangeRate(
            Currency baseCurrency, 
            Currency targetCurrency, 
            Amount baseCurrencyAmount)
        {
            var cursor = await _currencyExchangeCollection.FindAsync(
                ce =>
                    ce.BaseCurrency.Symbol == baseCurrency.Symbol.SymbolValue &&
                    ce.TargetCurrency.Symbol == targetCurrency.Symbol.SymbolValue);
            var result = await cursor.FirstOrDefaultAsync();
            if (result == null)
            {
                throw new NoValidCurrencyExchangeRateException();
            }
            return result.ToDomain(baseCurrencyAmount);
        }

        public async Task InsertCurrencyExchange(CurrencyExchange currencyExchange)
            => await _currencyExchangeCollection.InsertOneAsync(currencyExchange.ToDto());


        public async Task DeleteCurrencyExchange(CurrencyExchange currencyExchange)
            => await _currencyExchangeCollection.DeleteOneAsync(
                ce =>
                    ce.BaseCurrency.Symbol == currencyExchange.BaseCurrency.Symbol.SymbolValue &&
                    ce.TargetCurrency.Symbol == currencyExchange.TargetCurrency.Symbol.SymbolValue);

        public async Task<List<Currency>> GetAllAvailableCurrencies()
        {
            var cursor = await _availableCurrenciesCollection.FindAsync(_ => true);
            return cursor.ToEnumerable().Select(acc => acc.ToDomain()).ToList();
        }

        public async Task InsertAllAvailableCurrencies(List<Currency> allAvailableCurrencies)
        {
            foreach (var availableCurrency in allAvailableCurrencies)
            {
                await _availableCurrenciesCollection.InsertOneAsync(availableCurrency.ToDto());
            }
        }
    }
}