using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using sURL.Models;
using sURL.Services;

namespace sURL.Controllers
{
    [ApiController]
    public class RedirectController : Controller
    {
        private UrlRecordContext _UrlRecordContext = null;
        private ICodingService _CodingService = null;

        public RedirectController(
            UrlRecordContext urlRecordContext,
            ICodingService codingService)
        {
            this._UrlRecordContext = urlRecordContext;
            this._CodingService = codingService;
        }

        [HttpGet("~/{hashid}")]
        public async Task<IActionResult> JumpAsync(string hashid)
        {
            await Task.Yield();

            var id = this._CodingService.Decode(hashid);

            var urlRecord = this._UrlRecordContext
                .Urls
                .Where(x => x.Id == id)
                .SingleOrDefault();

            urlRecord.AccessCountUp();

            await this._UrlRecordContext.SaveChangesAsync();

            return RedirectPermanent(urlRecord.Url);
        }
    }
}