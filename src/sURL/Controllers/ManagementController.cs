using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using sURL.Models;
using sURL.Services;

namespace sURL.Controllers
{
    [Route("Management")]
    [ApiController]
    public class ManagementController : Controller
    {
        private UrlRecordContext _UrlRecordContext = null;
        private ICodingService _CodingService = null;

        public ManagementController(
            UrlRecordContext urlRecordContext,
            ICodingService codingService)
        {
            this._UrlRecordContext = urlRecordContext;
            this._CodingService = codingService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]data url)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var urlRecord = new UrlRecord()
            {
                Url = url.url
            };

            await this._UrlRecordContext.Urls.AddAsync(urlRecord);
            await this._UrlRecordContext.SaveChangesAsync();

            var uri = new UriBuilder()
            {
                Scheme = HttpContext.Request.Scheme,
                Host = HttpContext.Request.Host.Host,
                Path = this._CodingService.Encode(urlRecord.Id)
            };
            if(HttpContext.Request.Host.Port.HasValue)
            {
                uri.Port = HttpContext.Request.Host.Port.Value;
            }

            return Created(uri.Uri, uri.Uri.ToString());
        }

        public class data
        {
            public string url {get; set;}
        }
    }
}