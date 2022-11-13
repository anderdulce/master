using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Modelss;
using WebApplication1.Repository;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet(Name = "GetUsuarios")]
        public List<Usuario> Get()
        {
            return ADO_Usuario.DevolverUsuarios();
        }

        [HttpGet("Id")]
        public Usuario TraerUsuario(int id)
        {
            return ADO_Usuario.TraerUsuario(id);
        }

        [HttpGet("InicioSesion")]
        public Usuario Get(String nombre, String contraseña)
        {
            return ADO_Usuario.InicioSesion(nombre, contraseña);
        }

        [HttpPost]
        public void Agregar([FromBody] Usuario usu)
        {
            ADO_Usuario.CrearUsuario(usu);
        }

        [HttpPut]
        public void Modificar([FromBody] Usuario usu )
        {
            ADO_Usuario.ModificarUsuario(usu);
        }


        [HttpDelete]
        public void Eliminar([FromBody] int id)
        {
            ADO_Usuario.EliminarUsuario(id);
        }
    }
}
