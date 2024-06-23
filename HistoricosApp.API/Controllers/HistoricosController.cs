using HistoricosApp.API.Contexts;
using HistoricosApp.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace HistoricosApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricosController : ControllerBase
    {
        private readonly MongoDBContext? _mongoDBContext;

        public HistoricosController(MongoDBContext? mongoDBContext)
        {
            _mongoDBContext = mongoDBContext;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<LogProdutos>), 200)]
        public IActionResult Get(int page = 1, int pageSize = 10, DateTime? dataMin = null, DateTime? dataMax = null)
        {
            var filterBuilder = Builders<LogProdutos>.Filter;
            var filter = filterBuilder.Empty;

            if (dataMin.HasValue)
                filter &= filterBuilder.Gte(log => log.DataHora, dataMin.Value);

            if(dataMax.HasValue)
                filter &= filterBuilder.Lte(log => log.DataHora, dataMax.Value);

            var result = _mongoDBContext?.Produtos
                .Find(filter)
                .SortByDescending(log => log.DataHora)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToList();

            if (result.Any())
                return StatusCode(200, result);
            else
                return NoContent();
        }
    }
}
