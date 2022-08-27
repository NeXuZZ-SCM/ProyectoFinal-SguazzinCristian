using Microsoft.AspNetCore.Mvc;
using System.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IntegrandoApisConAdo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AppController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/<AppController>
        [HttpGet]
        [Route("/GetAppName")]
        public string Get()
        {
            return _configuration.GetSection("NameApp").Value;
        }
    }
}
