using Microsoft.AspNetCore.Mvc;
using WebApplication1.Modelss;
using WebApplication1.Repository;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        [HttpGet("GetVentas")]
        public List<Venta> Get()
        {
            return ADO_Venta.DevolverVenta();
        }

        [HttpGet("VentasIdUsuario")]
        public List<Venta> TraerVentaUsuario(int idUsuario)
        {
            return ADO_Venta.TraerVenta(idUsuario);
        }



        [HttpGet("VentasConSusProducto")]
        public List<Venta> TraerVentasIncluyendoProducto(int idVenta)
        {
            return ADO_Venta.TraerVentaConProductos(idVenta);
        }


        [HttpPost]
        public void CargarVenta([FromBody] VentaProducto ventas)
        {
            ADO_Venta.CargarVenta(ventas);
        }
        
    }

}
