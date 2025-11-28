using desafio_adonet.Banco;
using desafio_adonet.DTOs;
using desafio_adonet.Models;
using Microsoft.AspNetCore.Mvc;

namespace desafio_adonet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriasDAL _categoriasDAL;

        public CategoriaController(CategoriasDAL categoriasDAL)
        {
            _categoriasDAL = categoriasDAL;
        }


        [HttpPost("cadastrar")]
        public IActionResult Cadastrar([FromBody] Categoria categoria)
        {
            _categoriasDAL.Cadastrar(categoria);
            return Ok();
        }

        [HttpGet("listar")]
        public IActionResult Listar()
        {
            var categorias = _categoriasDAL.Listar();
            return Ok(categorias);
        }

    }
}
