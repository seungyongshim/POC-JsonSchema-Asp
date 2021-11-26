using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static LanguageExt.Prelude;
using NJsonSchema;
using WebApi.Dto;
using NJsonSchema.Validation.FormatValidators;
using NJsonSchema.Validation;
using System.Net.Http;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SendMailController : ControllerBase
    {
        public ILogger Logger { get; }

        public SendMailController(ILogger<SendMailController> logger) => Logger = logger;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var schema = JsonSchema.FromType<SendMailDto>();
            return Ok(schema.ToJson());
        }

        [HttpPost]
        public async Task<Unit> ValidationTest([FromBody] JsonElement json)
        {
            var schema = await JsonSchema.FromJsonAsync(@"
{
  ""$schema"": ""http://json-schema.org/draft-04/schema#"",
  ""title"": ""SendMailDto"",
  ""type"": ""object"",
  ""additionalProperties"": false,
  ""properties"": {
    ""From"": {
      ""oneOf"": [
        {
          ""type"": ""null""
        },
        {
          ""$ref"": ""#/definitions/MailAddress""
        }
      ]
    },
    ""To"": {
      ""type"": [
        ""array"",
        ""null""
      ],
      ""items"": {
        ""$ref"": ""#/definitions/MailAddress""
      }
    },
    ""Cc"": {
      ""type"": [
        ""array"",
        ""null""
      ],
      ""items"": {
        ""$ref"": ""#/definitions/MailAddress""
      }
    },
    ""Bcc"": {
      ""type"": [
        ""array"",
        ""null""
      ],
      ""items"": {
        ""$ref"": ""#/definitions/MailAddress""
      }
    },
    ""Subject"": {
      ""type"": [
        ""null"",
        ""string""
      ]
    },
    ""Body"": {
      ""type"": [
        ""null"",
        ""string""
      ]
    },
    ""IsBodyHtml"": {
      ""type"": ""boolean""
    },
    ""Priority"": {
      ""type"": [
        ""null"",
        ""string""
      ]
    },
    ""Attachments"": {
      ""type"": [
        ""array"",
        ""null""
      ],
      ""items"": {
        ""$ref"": ""#/definitions/Attachment""
      }
    }
  },
  ""definitions"": {
    ""MailAddress"": {
      ""type"": ""object"",
      ""additionalProperties"": false,
      ""properties"": {
        ""Name"": {
          ""type"": [
            ""null"",
            ""string""
          ]
        },
        ""Address"": {
          ""type"": [
            ""null"",
            ""string""
          ]
        }
      }
    },
    ""Attachment"": {
      ""type"": ""object"",
      ""additionalProperties"": false,
      ""properties"": {
        ""FileName"": {
          ""type"": [
            ""null"",
            ""string""
          ]
        },
        ""ContentId"": {
          ""type"": [
            ""null"",
            ""string""
          ]
        },
        ""Data"": {
          ""type"": [
            ""null"",
            ""string""
          ],
          ""format"": ""byte""
        }
      }
    }
  }
}");
            var errors = string.Join('\n', schema.Validate(json.GetRawText()));
            Console.WriteLine($"{errors}");

            HttpClient httpClient = new HttpClient();
            await httpClient.PostAsync("http://localhost:5000/typedsendmail", 
            {
                Headers = ""
            });
            return unit;
        }
    }
}
