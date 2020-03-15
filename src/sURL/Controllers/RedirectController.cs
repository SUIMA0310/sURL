using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace sURL.Controllers
{
    [ApiController]
    public class RedirectController : Controller
    {
        public RedirectController()
        {
        }

        [HttpGet("~/{hashid}")]
        public async Task<IActionResult> JumpAsync(string hashid)
        {
            await Task.Yield();

            return RedirectPermanent("https://google.co.jp/");
        }
    }
}