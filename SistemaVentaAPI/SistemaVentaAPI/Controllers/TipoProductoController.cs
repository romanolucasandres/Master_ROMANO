using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentaAPI.Entidades;
using SistemaVentaAPI.Interface;
using SistemaVentaAPI.Repository;


namespace SistemaVentaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoProductoController : ControllerBase, IEndPoints<TipoProducto>
    {
        //el protocolo que se usa 
        [HttpGet()]
        public List<TipoProducto> Get()
        {
            return ADO_TipoProducto.Listar();
        }

        [HttpGet("TraerXId")]
        public TipoProducto Get(int id)
        {
            return ADO_TipoProducto.Obtener(id);
        }

        [HttpDelete]
        public void Eliminar([FromBody] int x)
        {
            ADO_TipoProducto.Eliminar(x);
        }
        [HttpPost]
        public void Alta([FromBody] TipoProducto x)
        {
            ADO_TipoProducto.Guardar(x);
        }
        
        
        [HttpPut]
        public void Modificacion([FromBody] TipoProducto x)
        {
            
            ADO_TipoProducto.Guardar(x);
        }
    }
}
