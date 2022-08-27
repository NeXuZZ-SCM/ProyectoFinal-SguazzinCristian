using IntegrandoApisConAdo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using IntegrandoApisConAdo.Service;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IntegrandoApisConAdo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private UsuarioService _usuarioService;
        public SessionController()
        {
            _usuarioService = new UsuarioService();
        }

        // GET: api/<SessionController>
        [HttpGet]
        [Route("/ValidateSession")]
        public ResponseValidateSession Get(string userName, string password)
        {
            ResponseValidateSession responseValidateSession = new ResponseValidateSession();
            var userValidate = _usuarioService.ValidateSession(userName, password);
            if (userValidate.Id == 0)
            {
                responseValidateSession.status = "fail";
                return responseValidateSession;

            }
            responseValidateSession.status = "ok";
            responseValidateSession.User = userValidate;
            return responseValidateSession;
        }
    }
}
