using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentaAPI.Entidades;
using SistemaVentaAPI.Interface;
using SistemaVentaAPI.Repository;

namespace SistemaVentaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        
        ////el protocolo que se usa 
        //[HttpGet(Name = "GetVentas")]
        //public List<Venta> Get()
        //{
        //    return ADO_Venta.DevolverVentas();
        //}
        
        [HttpGet("Obtener por usuario")]
        public List<Venta> Get()
        {
            return ADO_Venta.DevolverVentasPorUsuario();
        }


        [HttpDelete]
        public void Eliminar([FromBody] int x)
        {
            ADO_Venta.Eliminar(x);
        }
        [HttpPost]
        public void Alta([FromBody] Orden orden)
        {
            ADO_Venta.Alta(orden);
        }
        [HttpPut]
        public void Modificacion([FromBody] Venta venta)
        {
            ADO_Venta.Modificar(venta);
        }
    }
}
