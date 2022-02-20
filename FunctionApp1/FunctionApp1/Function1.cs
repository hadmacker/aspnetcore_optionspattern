using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using FunctionApp1.Options;

namespace FunctionApp1
{
    public class Function1
    {
        private readonly NamedOptions _namedOptions1;
        private readonly NamedOptions _namedOptions2;

        public Function1(IOptionsMonitor<NamedOptions> namedOptionsMonitor)
        {
            _namedOptions1 = namedOptionsMonitor.Get("NamedOptions1");
            _namedOptions2 = namedOptionsMonitor.Get("NamedOptions2");
        }

        [FunctionName("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            log.LogInformation($"NamedOptions1.Name: {_namedOptions1.Name}");
            log.LogInformation($"NamedOptions2.Name: {_namedOptions2.Name}");

            return new OkObjectResult(responseMessage);
        }
    }
}
