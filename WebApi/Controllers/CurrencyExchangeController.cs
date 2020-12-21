using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyExchangeController : ControllerBase
    {
        public CurrencyExchangeController()
        {
            
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using var client = new HttpClient();
            var content = await client.GetStringAsync(
                "http://data.fixer.io/api/symbols?access_key=ecd46d5e28c44d88658a7b109cc29b2a");
            return Ok(content);
        }
    }
}