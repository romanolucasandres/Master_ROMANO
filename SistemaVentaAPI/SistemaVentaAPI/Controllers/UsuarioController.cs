using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentaAPI.Entidades;
using SistemaVentaAPI.Interface;
using SistemaVentaAPI.Repository;

namespace SistemaVentaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase, IEndPoints<Usuario>
    {
        //el protocolo que se usa 
        [HttpGet("testing")]
        public Usuario Get(string nombre, string pass)
        {
            return ADO_Usuario.ValidarUsuario(nombre, pass);
        }

        [HttpGet]
        public Usuario Get(string nombre)
        {
            return ADO_Usuario.traerUsuario(nombre);
        }

        [HttpPut]
        public void Modificacion([FromBody] Usuario x)
        {
            ADO_Usuario.Guardar(x);
        }

        [HttpPost]
        public void Alta([FromBody] Usuario x)
        {
            ADO_Usuario.Guardar(x);
        }

        [HttpDelete]
        public void Eliminar([FromBody] int x)
        {
            ADO_Usuario.Eliminar(x);
        }



    }
        
}
