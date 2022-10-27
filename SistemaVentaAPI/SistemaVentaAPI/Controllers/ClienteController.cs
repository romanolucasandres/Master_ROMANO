using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentaAPI.Entidades;
using SistemaVentaAPI.Interface;
using SistemaVentaAPI.Repository;

namespace SistemaVentaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase, IEndPoints<Cliente>
    {
       

        //el protocolo que se usa 
        [HttpGet("GetClientes")]
        public List<Cliente> Get()
        {
            return ADO_Cliente.DevolverClientes();
        }
        [HttpPost]
        public void Alta([FromBody] Cliente x)
        {
            ADO_Cliente.Alta(x);
        }
        [HttpDelete]
        public void Eliminar([FromBody] int x)
        {
            ADO_Cliente.Eliminar(x);
        }

        [HttpPut]
        public void Modificacion([FromBody] Cliente x)
        {
            ADO_Cliente.Modificar(x);
        }
    }
}
