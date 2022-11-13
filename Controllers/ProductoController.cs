using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Modelss;
using WebApplication1.Repository;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        [HttpGet(Name = "GetProductos")]
        public List<Producto> Get()
        {
            return ADO_Producto.DevolverProductos();
        }

        [HttpGet("Id")]
        public Producto TraerProducto(int id)
        {
            return ADO_Producto.TraerProducto(id);
        }

        [HttpGet("IdUsuario")]
        public List <Producto> TraerProductoIdUsuario(int idUsuario)
        {
            return ADO_Producto.TraerProductoIdUsuario(idUsuario);
        }



        [HttpPost]
        public void Crear([FromBody] Producto prod)
        {
            ADO_Producto.CrearProducto(prod);
        }

        [HttpPut]
        public void Modificar([FromBody] Producto prod)
        {
             ADO_Producto.ModificarProducto(prod);
        }

        [HttpDelete]
        public void Eliminar([FromBody] int idProducto)
        {
            ADO_Producto.EliminarProducto(idProducto);
        }
    }
}
