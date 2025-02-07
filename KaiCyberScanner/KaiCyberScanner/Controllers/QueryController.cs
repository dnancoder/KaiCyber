using KaiCyberScanner.Database;
using KaiCyberScanner.Helper;
using KaiCyberScanner.Model;
using KaiCyberScanner.Processor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KaiCyberScanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly IProcessor<List<Vulnerability>> _queryProcessor;
        private readonly IDatabaseHelper _databaseHelper;
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly ILogger<ScanController> _logger;

        public QueryController(IProcessor<List<Vulnerability>> queryProcessor, IDatabaseHelper databaseHelper, 
            IHttpClientHelper httpClientHelper, ILogger<ScanController> logger)
        {
            this._databaseHelper = databaseHelper;
            this._httpClientHelper = httpClientHelper;
            this._queryProcessor = queryProcessor;
            this._logger = logger;
        }

        [HttpPost("PostQuery")]
        public async Task<ActionResult<List<Vulnerability>>> PostQueryMethod([FromBody]FilterQuery filterQuery)
        {
            try
            {
                var vulnerabilityList = await this._queryProcessor.ProcessAsync(filterQuery);
                return Ok(vulnerabilityList);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message, ex);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
