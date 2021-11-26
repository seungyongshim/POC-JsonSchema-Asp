using System;
using System.Threading.Tasks;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;
using static LanguageExt.Prelude;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TypedSendMailController : Controller
    {
        [HttpPost]
        public async Task<Unit> Post([FromBody] SendMailDto dto)
        {
            Console.WriteLine(dto);
            return unit;
        }
}
