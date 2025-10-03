using desafio_adonet.Models;
using desafio_adonet.Banco;
using Microsoft.AspNetCore.Mvc;

namespace desafio_adonet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
         private readonly ProdutosDAL _produtosDAL;

        public ProdutosController(ProdutosDAL produtosDAL)
        {
            _produtosDAL = produtosDAL;
        }

        [HttpGet("listar")]
        public IActionResult Listar()
        {
            var produtos = _produtosDAL.Listar();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var produto = _produtosDAL.ObterPorId(id);
            if( produto != null)
            {
                return Ok(produto);
            }

            return NotFound();
        }

        [HttpPost("cadastrar")]
        public IActionResult Cadastrar([FromBody] Produto produto)
        {
            _produtosDAL.Cadastrar(produto);
            return CreatedAtAction(nameof(ObterPorId), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] Produto produto)
        {
            var existente = _produtosDAL.ObterPorId(id);
            if (existente == null)
                return NotFound();

            produto.Id = id;
            _produtosDAL.Atualizar(produto);
            return NoContent();
        }

        [HttpDelete("deletar/{id}")]
        public IActionResult Excluir(int id)
        {
            var existente = _produtosDAL.ObterPorId(id);
            if (existente == null)
                return NotFound();

            _produtosDAL.Excluir(id);
            return NoContent();
        }

        [HttpGet("filtrar")]
        public IActionResult Filtrar([FromQuery] string? nome, [FromQuery] int? precoMin, [FromQuery] int? precoMax)
        {
            var produtos = _produtosDAL.Filtrar(nome, precoMin, precoMax);
            return Ok(produtos);
        }

        [HttpGet("produtos-ordem")]
        public IActionResult GetNomesProdutos()
        {
            var produtos = _produtosDAL.Listar();

            var nomes = produtos
                .Select(p => p.Nome)
                .OrderBy(n => n)
                .ToList();

            return Ok(nomes);
        }

    }
}
