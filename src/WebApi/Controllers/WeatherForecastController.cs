using System;
using System.Text.Json;
using System.Threading.Tasks;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static LanguageExt.Prelude;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        public ILogger Logger { get; }

        public TestController(ILogger<TestController> logger) => Logger = logger;

        [HttpPost]
        public async Task<Unit> ValidationTest([FromBody] JsonElement json)
        {
            //https://github.com/dotnet/runtime/issues/29887
            return unit;
        }
    }
}
