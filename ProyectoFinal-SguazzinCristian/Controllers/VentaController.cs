using IntegrandoApisConAdo.Models;
using Microsoft.AspNetCore.Mvc;
using IntegrandoApisConAdo.Repository;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IntegrandoApisConAdo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        VentaRepository _ventaRepository;
        public VentaController()
        {
            _ventaRepository = new VentaRepository();
        }
        //GET: api/<VentaController>
        [HttpGet]
        public List<VentasYProductos> Get()
        {
            return _ventaRepository.GetVentasYProductos();
        }

        // POST api/<VentaController>
        [HttpPost]
        public bool Post([FromBody]List<Producto> listaProductos, int id)
        {
            if (listaProductos.Count > 0)
            {
                return _ventaRepository.AddVenta(listaProductos, id);
            }
            return false;
        }

        // DELETE api/<VentaController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _ventaRepository.DeleteVenta(id);
        }
    }
}
