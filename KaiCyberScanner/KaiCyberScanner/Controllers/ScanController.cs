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
    public class ScanController : ControllerBase
    {
        private readonly IProcessor<List<ScanPayloadMetadata>> _scanProcessor;
        private readonly IDatabaseHelper _databaseHelper;
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly ILogger<ScanController> _logger;

        public ScanController(IProcessor<List<ScanPayloadMetadata>> scanProcessor, IDatabaseHelper databaseHelper, 
            IHttpClientHelper httpClientHelper, ILogger<ScanController> logger)
        {
            this._databaseHelper = databaseHelper;
            this._httpClientHelper = httpClientHelper;
            this._scanProcessor = scanProcessor;
            this._logger = logger;
        }

        [HttpPost("PostScan")]
        public async Task<ActionResult<List<ScanPayloadMetadata>>> PostScanMethod([FromBody] ScanRepo scanRepo)
        {
            try
            {
                var payloadMetadataList = await this._scanProcessor.ProcessAsync(scanRepo);
                return Ok(payloadMetadataList);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message, ex);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
