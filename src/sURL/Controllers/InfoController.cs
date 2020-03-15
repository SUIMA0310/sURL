using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace sURL.Controllers
{
    [Route("Info")]
    [ApiController]
    public class InfoController : Controller
    {
        [HttpGet("Url/{hashid}")]
        public async Task<IActionResult> URL(string hashid)
        {
            await Task.Yield();

            return Ok("https://google.co.jp/");
        }

        [HttpGet("AccessCount/{hashid}")]
        public async Task<IActionResult> AccessCount(string hashid)
        {
            await Task.Yield();

            return Ok(3);            
        }
    }
}