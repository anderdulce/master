using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Modelss;
using WebApplication1.Repository;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoVendidoController : ControllerBase
    {
        [HttpGet("GetProductosVendidos")]
        public List<ProductoVendido> Get()
        {
            return ADO_ProductoVendido.DevolverProductosVendidos();
        }


        [HttpGet("GetProductosVendidosDeUnUsuario")]
        public List<ProductoVendido> ProductosVendidosUsuario(int idUsuario)
        {
            return ADO_ProductoVendido.DevolverProductoVendidoPorUsuario(idUsuario);
        }

    }

}
