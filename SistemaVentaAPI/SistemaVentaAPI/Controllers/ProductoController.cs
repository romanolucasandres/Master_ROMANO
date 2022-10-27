using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentaAPI.Entidades;
using SistemaVentaAPI.Interface;
using SistemaVentaAPI.Repository;

namespace SistemaVentaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase, IEndPoints<Producto>
    {
        //el protocolo que se usa 
        [HttpGet("GetProductos")]
        public List<Producto> Get()
        {
            return ADO_Producto.DevolverProductosPorUsuario();
        }
        [HttpDelete]
        public void Eliminar([FromBody] int x)
        {
            ADO_Producto.Eliminar(x);
        }
        [HttpPost]
        public void Alta([FromBody] Producto oProducto)
        {
            ADO_Producto.Guardar(oProducto);
        }
        [HttpPut]
        public void Modificacion([FromBody] Producto oProducto)
        {
            ADO_Producto.Guardar(oProducto);
        }
    }
}
