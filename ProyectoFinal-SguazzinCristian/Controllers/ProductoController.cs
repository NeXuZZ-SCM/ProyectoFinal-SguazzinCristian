using IntegrandoApisConAdo.Models;
using IntegrandoApisConAdo.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IntegrandoApisConAdo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private ProductoRepository _productoRepository;

        public ProductoController()
        {
            _productoRepository = new ProductoRepository();
        }
        // GET: api/<ProductoController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<ProductoController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<ProductoController>
        [HttpPost]
        public int  Post([FromBody]Producto producto)
        {
            return _productoRepository.AddProducto(producto);
        }

        // PUT api/<ProductoController>/5
        [HttpPut]
        public int Put([FromBody] Producto producto)
        {
            return _productoRepository.UpdateProducto(producto);
        }

        // DELETE api/<ProductoController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _productoRepository.DeleteProducto(id);
        }
    }
}
