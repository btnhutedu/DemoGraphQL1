using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL;
using Microsoft.AspNetCore.Mvc;
using DemoGraphQL2Gui.Models;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace DemoGraphQL2Gui.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly HttpClient _httpClient;

        public EmployeeController(IHttpClientFactory httpClientFactory, ILogger<EmployeeController> logger)
        {
            _httpClient = new HttpClient();
            _logger = logger;
            _logger.LogInformation("EmployeeController start");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                // Create a GraphQL client
                var client = new GraphQLHttpClient(new GraphQLHttpClientOptions
                {
                    //EndPoint = new Uri("https://localhost:7188/graphql/"),
                    EndPoint = new Uri("http://localhost:82/graphql/"),
                },
                new NewtonsoftJsonSerializer(), _httpClient);

                // Define the GraphQL query
                var request = new GraphQLRequest
                {
                    Query = @"
                        query AllEmployeesWithDepartment {
                            allEmployeesWithDepartment {
                                employeeId
                                name
                                email
                                age
                                department {
                                    name
                                }
                            }
                        }"
                };

                var response = await client.SendQueryAsync<object>(request);

                var data = response.Data as JToken;

                if (data != null)
                {
                    var employeesData = data["allEmployeesWithDepartment"];
                    if (employeesData != null)
                    {
                        var employeesList = employeesData.ToObject<List<Employee>>();
                        return View(employeesList);
                    }
                }

                return View(new List<Employee>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while employees controller");
                throw;
            }
        }
    }
}
