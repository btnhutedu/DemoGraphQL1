using DemoGraphQL2Gui.Models;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DemoGraphQL2Gui.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly HttpClient _httpClient;

        public DepartmentController(IHttpClientFactory httpClientFactory, ILogger<EmployeeController> logger)
        {
            _httpClient = new HttpClient();
            _logger = logger;
            _logger.LogInformation("DepartmentController start");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                string graphQLEndpoint = string.Empty;
                bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

                if (isDevelopment)
                {
                    graphQLEndpoint = "https://localhost:7188/graphql/";
                }
                else
                {
                    graphQLEndpoint = "http://localhost:82/graphql/";
                }

                // Create a GraphQL client
                var client = new GraphQLHttpClient(new GraphQLHttpClientOptions
                {
                    EndPoint = new Uri(graphQLEndpoint),
                },
                new NewtonsoftJsonSerializer(), _httpClient);

                // Define the GraphQL query
                var request = new GraphQLRequest
                {
                    Query = @"
                        query AllDepartment {
                            allDepartment {
                                departmentId
                                name
                            }
                        }"
                };

                var response = await client.SendQueryAsync<object>(request);

                var data = response.Data as JToken;

                if (data != null)
                {
                    var departmentData = data["allDepartment"];
                    if (departmentData != null)
                    {
                        var departmentList = departmentData.ToObject<List<Department>>();
                        return View(departmentList);
                    }
                }

                return View(new List<Department>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while Department controller");
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                string graphQLEndpoint = string.Empty;
                bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

                if (isDevelopment)
                {
                    graphQLEndpoint = "https://localhost:7188/graphql/";
                }
                else
                {
                    graphQLEndpoint = "http://localhost:82/graphql/";
                }

                // Create a GraphQL client
                var client = new GraphQLHttpClient(new GraphQLHttpClientOptions
                {
                    EndPoint = new Uri(graphQLEndpoint),
                },
                new NewtonsoftJsonSerializer(), _httpClient);

                // Define the GraphQL query with a variable for 'id'
                var request = new GraphQLRequest
                {
                    Query = @"
                            query DepartmentById($id: Int!) {
                                departmentById(id: $id) {
                                    departmentId
                                    name
                                }
                            }",
                    Variables = new
                    {
                        id
                    }
                };

                var response = await client.SendQueryAsync<object>(request);

                var data = response.Data as JToken;

                if (data != null)
                {
                    var departmentData = data["departmentById"];
                    if (departmentData != null)
                    {
                        var department = departmentData.ToObject<Department>();
                        return View(department);
                    }
                }

                return View(new Department()); // Return an empty department or handle the case when no department is found.
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while Department controller");
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment(Department department)
        {
            try
            {
                string graphQLEndpoint = string.Empty;
                bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

                if (isDevelopment)
                {
                    graphQLEndpoint = "https://localhost:7188/graphql/";
                }
                else
                {
                    graphQLEndpoint = "http://localhost:82/graphql/";
                }

                // Create a GraphQL client
                var client = new GraphQLHttpClient(new GraphQLHttpClientOptions
                {
                    EndPoint = new Uri(graphQLEndpoint),
                },
                new NewtonsoftJsonSerializer(), _httpClient);

                // Define the GraphQL mutation
                var request = new GraphQLRequest
                {
                    Query = @"
                            mutation CreateDepartment($departmentInput: DepartmentQueryModelInput!) {
                                createDepartment(department: $departmentInput) {
                                    departmentId
                                    name
                                }}",
                    Variables = new
                    {
                        departmentInput = department
                    }
                };

                var response = await client.SendQueryAsync<object>(request);

                if (response.Errors != null)
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating department");
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                string graphQLEndpoint = string.Empty;
                bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

                if (isDevelopment)
                {
                    graphQLEndpoint = "https://localhost:7188/graphql/";
                }
                else
                {
                    graphQLEndpoint = "http://localhost:82/graphql/";
                }

                // Create a GraphQL client
                var client = new GraphQLHttpClient(new GraphQLHttpClientOptions
                {
                    EndPoint = new Uri(graphQLEndpoint),
                },
                new NewtonsoftJsonSerializer(), _httpClient);

                // Define the GraphQL query with a variable for 'id'
                var request = new GraphQLRequest
                {
                    Query = @"
                            query DepartmentById($id: Int!) {
                                departmentById(id: $id) {
                                    departmentId
                                    name
                                }
                            }",
                    Variables = new
                    {
                        id
                    }
                };

                var response = await client.SendQueryAsync<object>(request);

                var data = response.Data as JToken;

                if (data != null)
                {
                    var departmentData = data["departmentById"];
                    if (departmentData != null)
                    {
                        var department = departmentData.ToObject<Department>();
                        return View(department);
                    }
                }

                return View(new Department()); // Return an empty department or handle the case when no department is found.
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while Department controller");
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDepartment(Department department)
        {
            try
            {
                string graphQLEndpoint = string.Empty;
                bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

                if (isDevelopment)
                {
                    graphQLEndpoint = "https://localhost:7188/graphql/";
                }
                else
                {
                    graphQLEndpoint = "http://localhost:82/graphql/";
                }

                // Create a GraphQL client
                var client = new GraphQLHttpClient(new GraphQLHttpClientOptions
                {
                    EndPoint = new Uri(graphQLEndpoint),
                },
                new NewtonsoftJsonSerializer(), _httpClient);

                // Define the GraphQL mutation
                var request = new GraphQLRequest
                {
                    Query = @"
                            mutation UpdateDepartment($departmentInput: DepartmentQueryModelInput!) {
                            updateDepartment(department: $departmentInput) {
                                departmentId
                                name
                            }
                        }",
                    Variables = new
                    {
                        departmentInput = department
                    }
                };

                var response = await client.SendQueryAsync<object>(request);

                if (response.Errors != null)
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating department");
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                string graphQLEndpoint = string.Empty;
                bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

                if (isDevelopment)
                {
                    graphQLEndpoint = "https://localhost:7188/graphql/";
                }
                else
                {
                    graphQLEndpoint = "http://localhost:82/graphql/";
                }

                // Create a GraphQL client
                var client = new GraphQLHttpClient(new GraphQLHttpClientOptions
                {
                    EndPoint = new Uri(graphQLEndpoint),
                },
                new NewtonsoftJsonSerializer(), _httpClient);

                // Define the GraphQL query with a variable for 'id'
                var request = new GraphQLRequest
                {
                    Query = @"
                            query DepartmentById($id: Int!) {
                                departmentById(id: $id) {
                                    departmentId
                                }
                            }",
                    Variables = new
                    {
                        id
                    }
                };

                var response = await client.SendQueryAsync<object>(request);

                var data = response.Data as JToken;

                if (data != null)
                {
                    var departmentData = data["departmentById"];
                    if (departmentData != null)
                    {
                        var department = departmentData.ToObject<Department>();
                        return View(department);
                    }
                }

                return View(new Department()); // Return an empty department or handle the case when no department is found.
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while Department controller");
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDepartment(Department department)
        {
            try
            {
                string graphQLEndpoint = string.Empty;
                bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

                if (isDevelopment)
                {
                    graphQLEndpoint = "https://localhost:7188/graphql/";
                }
                else
                {
                    graphQLEndpoint = "http://localhost:82/graphql/";
                }

                // Create a GraphQL client
                var client = new GraphQLHttpClient(new GraphQLHttpClientOptions
                {
                    EndPoint = new Uri(graphQLEndpoint),
                },
                new NewtonsoftJsonSerializer(), _httpClient);

                // Define the GraphQL mutation
                var request = new GraphQLRequest
                {
                    Query = @"
                            mutation DeleteDepartment($id: Int!) {
                                deleteDepartment(id: $id) {
                                    departmentId
                                    name
                                }
                            }",
                    Variables = new
                    {
                        id = department.DepartmentId
                    }
                };

                var response = await client.SendQueryAsync<object>(request);

                if (response.Errors != null)
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting department");
                throw;
            }
        }
    }
}
