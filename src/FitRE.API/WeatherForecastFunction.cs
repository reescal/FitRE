using System;
using System.Net;
using System.Threading.Tasks;
using FitRE.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace FitRE.API
{
    public class WeatherForecastFunction
    {
        private readonly ILogger<WeatherForecastFunction> _logger;

        public WeatherForecastFunction(ILogger<WeatherForecastFunction> log)
        {
            _logger = log;
        }

        [FunctionName("WeatherForecast")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "WeatherForecast" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WeatherForecast[]), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var responseMessage = new WeatherForecast[]
            {
                new WeatherForecast()
                {
                    date = DateTime.Today,
                    Summary = "Warm",
                    Temperature = 25
                },
                new WeatherForecast()
                {
                    date = DateTime.Today.AddDays(1),
                    Summary = "Warm",
                    Temperature = 30
                }
            };

            return new OkObjectResult(responseMessage);
        }
    }
}

