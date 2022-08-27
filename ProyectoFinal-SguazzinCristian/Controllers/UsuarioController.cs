using IntegrandoApisConAdo.Models;
using IntegrandoApisConAdo.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IntegrandoApisConAdo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private UsuarioRepository _usuarioRepository;
        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{userName}")]
        public Usuario Get(string userName)
        {
            return _usuarioRepository.GetUsuariosByUserName(userName);
        }


        // POST api/<UsuarioController>
        [HttpPost]
        public bool Post([FromBody] Usuario usuario)
        {
            return _usuarioRepository.AddUser(usuario);
        }

        // GET: api/<UsuarioController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<UsuarioController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // PUT api/<UsuarioController>/5
        [HttpPut]
        public int Put([FromBody]Usuario usuario)
        {
            return _usuarioRepository.UpdateUser(usuario);
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _usuarioRepository.DeleteUser(id);
        }

    }
}
