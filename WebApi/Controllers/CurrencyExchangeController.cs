using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ApplicationServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoAdapter.DTO;
using MongoDB.Bson;
using WebApi.Dto;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyExchangeController : ControllerBase
    {
        private readonly GetAllAvailableCurrenciesService _getAllAvailableCurrenciesService;
        public CurrencyExchangeController(
            GetAllAvailableCurrenciesService getAllAvailableCurrenciesService)
        {
            _getAllAvailableCurrenciesService = getAllAvailableCurrenciesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var domainCurrencies = await _getAllAvailableCurrenciesService.GetAll();
            return Ok(domainCurrencies.Select(dc =>dc.ToDtoWebApi()).ToList());
        }
    }
}