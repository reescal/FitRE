using System.Net;
using System.Threading.Tasks;
using FitRE.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace FitRE.API
{
    public class BudgetFunction
    {
        private readonly ILogger<BudgetFunction> _logger;

        public BudgetFunction(ILogger<BudgetFunction> log)
        {
            _logger = log;
        }

        [FunctionName("GetBudget")]
        [OpenApiOperation(operationId: "GetBudget", tags: new[] { "Budget" })]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **Id** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Budget), Description = "The OK response")]
        public async Task<IActionResult> GetBudget(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "budget/{id}")] HttpRequest req,
            [CosmosDB(databaseName: "%CosmosConfiguration:Database%", collectionName: "%CosmosConfiguration:Container%", ConnectionStringSetting = "CosmosDBConnection", Id = "{id}", PartitionKey = "{id}")] Budget budgetById,
            string id)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            if (budgetById != null)
                return new OkObjectResult(budgetById);
            
            _logger.LogInformation($"Budget {id} not found");
            return new NotFoundResult();
        }
    }
}

