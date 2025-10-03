using desafio_adonet.Banco;
using desafio_adonet.Models;
using Microsoft.AspNetCore.Mvc;

namespace desafio_adonet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriasDAL _categoriasDAL;
        private readonly ProdutoCategoriaDAL _produtoCategoriaDAL;

        public CategoriaController(CategoriasDAL categoriasDAL, ProdutoCategoriaDAL produtoCategoriaDAL)
        {
            _categoriasDAL = categoriasDAL;
            _produtoCategoriaDAL = produtoCategoriaDAL;
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

        [HttpPost("associar")]
        public IActionResult Associar([FromQuery] int produtoId, [FromQuery] int categoriaId)
        {
            _produtoCategoriaDAL.Associar(produtoId, categoriaId);
            return Ok();
        }

        [HttpGet("categorias-por-produto/{produtoId}")]
        public IActionResult ObterCategoriasPorProduto(int produtoId)
        {
            var categorias = _produtoCategoriaDAL.ObterCategoriasPorProduto(produtoId);
            return Ok(categorias);
        }

    }
}
