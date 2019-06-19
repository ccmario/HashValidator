using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HashValidatorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HashController : ControllerBase
    {
        [HttpGet]
        public void Get()
        {
        }

        [HttpPost]
        public async Task<JsonResult> Post()
        {
            string xmlContent;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.GetEncoding("ISO-8859-1")))
            {
                xmlContent = await reader.ReadToEndAsync();
            }
            return new JsonResult(HashValidator.Business.HashValidator.ValidateHash(xmlContent));
        }
    }
}