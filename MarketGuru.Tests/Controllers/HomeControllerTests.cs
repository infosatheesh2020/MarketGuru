using System;
using Xunit;
using System.Collections.Generic;
using System.Text;
using MarketGuru.Controllers;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Logging;
using MarketGuru.BusinessLogic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MarketGuru.Models;
using System.Linq;

namespace MarketGuru.Tests.Controllers
{
    public class HomeControllerTests
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICosmosDbService _cosmosDbService;

        [Fact]
        public void IndexAsync_returnView()
        {


            // Arrange
            Assert.Equal(1, 1);
        }
    }
}
