﻿using System.Linq;
using System.Threading.Tasks;
using ApplicationServices;
using CurrencyExchangeDomain;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;
using CurrencyExchangeDto = WebApi.Dto.CurrencyExchangeDto;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyExchangeController : ControllerBase
    {
        private readonly GetAllAvailableCurrenciesService _getAllAvailableCurrenciesService;
        private readonly GetCurrencyService _getCurrencyService;
        private readonly GetTargetCurrencyAmountService _getTargetCurrencyAmountService;
        public CurrencyExchangeController(
            GetAllAvailableCurrenciesService getAllAvailableCurrenciesService,
            GetCurrencyService getCurrencyService,
            GetTargetCurrencyAmountService getTargetCurrencyAmountService)
        {
            _getAllAvailableCurrenciesService = getAllAvailableCurrenciesService;
            _getCurrencyService = getCurrencyService;
            _getTargetCurrencyAmountService = getTargetCurrencyAmountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableCurrencies()
        {
            var domainCurrencies = await _getAllAvailableCurrenciesService.GetAll();
            return Ok(domainCurrencies.Select(dc =>dc.ToDtoWebApi()).ToList());
        }

        [HttpPost("convert/")]
        public async Task<IActionResult> GetCurrencyExchange([FromBody]CurrencyExchangeDto currencyExchangeDto)
        {
            var domainBaseCurrency = 
                await _getCurrencyService.GetBySymbol(currencyExchangeDto.BaseCurrencySymbol);
            var domainTargetCurrency = 
                await _getCurrencyService.GetBySymbol(currencyExchangeDto.TargetCurrencySymbol);
            var domainCurrencyAmount = Amount.From(currencyExchangeDto.BaseCurrencyAmount);
            var targetCurrencyAmount = 
                await _getTargetCurrencyAmountService.GetAmount(
                    domainBaseCurrency, 
                    domainTargetCurrency, 
                    domainCurrencyAmount);
            return Ok(targetCurrencyAmount);
        }
    }
}