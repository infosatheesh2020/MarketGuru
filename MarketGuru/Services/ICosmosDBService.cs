using System.Collections.Generic;
using System.Threading.Tasks;
using MarketGuru.Models;

namespace MarketGuru.BusinessLogic
{
    public interface ICosmosDbService
    {
        Task AddItemAsync(Quote item);
    }
}
