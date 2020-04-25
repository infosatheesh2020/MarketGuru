using MarketGuru.BusinessLogic;
using MarketGuru.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarketGuru.IntegrationTests
{
    public class CosmosTests
    {
        public const string DataBaseName = "Tests";
        public const string UserName = "username";
        private static ServiceProvider _provider;
        //only mock we need :)
        private static Mock<IHttpContextAccessor> _httpContextAccessor;

        public static ClaimsPrincipal ClaimPrincipal { get; set; }

        static CosmosTests()
        {

            var dict = new Dictionary<string, string>
        {
             { "CosmosDBEndpoint", "https://localhost:8081"},
             { "CosmosDBKey", "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="},
             {"test","true" },
             {"dataBaseName",DataBaseName }
        };

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(dict)
                .Build();
            var startup = new Startup(config);
            var services = new ServiceCollection();
            startup.ConfigureServices(services);
            _httpContextAccessor = new Mock<IHttpContextAccessor>();

            services.AddSingleton(_httpContextAccessor.Object);
            services.AddScoped(typeof(ILoggerFactory), typeof(LoggerFactory));
            services.AddScoped(typeof(ILogger<>), typeof(Logger<>));

            _provider = services.BuildServiceProvider();

        }

        [Fact]
        public static async Task CreateTestQuoteAsync()
        {
            var itemManager = _provider.GetService<CosmosDbService>();
            Quote quote = new Quote()
            {
                Scrip = "MSFT",
                Currentprice = 170,
                High = 201
            };
            await itemManager.AddItemAsync(quote);
            Assert.NotNull(itemManager.AddItemAsync(quote));
        }

        public static void SetControllerContext(Controller controller)
        {
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = _httpContextAccessor.Object.HttpContext
            };
        }

        public static void SetControllerContext(ControllerBase controller)
        {
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = _httpContextAccessor.Object.HttpContext
            };
        }

        public static T GetInstance<T>()
        {
            T result = _provider.GetRequiredService<T>();
            ControllerBase controllerBase = result as ControllerBase;
            if (controllerBase != null)
            {
                SetControllerContext(controllerBase);
            }
            Controller controller = result as Controller;
            if (controller != null)
            {
                SetControllerContext(controller);
            }
            return result;

        }
    }
}
