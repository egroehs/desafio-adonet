using desafio_adonet.Banco;
using desafio_adonet.DTOs;
using desafio_adonet.Models;
using Microsoft.AspNetCore.Mvc;

namespace desafio_adonet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
         private readonly ProdutosDAL _produtosDAL;
        private readonly ProdutoCategoriaDAL _produtoCategoriaDAL;

        public ProdutosController(ProdutosDAL produtosDAL, ProdutoCategoriaDAL produtoCategoriaDAL)
        {
            _produtosDAL = produtosDAL;
            _produtoCategoriaDAL = produtoCategoriaDAL;
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

        [HttpPost("associar")]
        public IActionResult Associar([FromQuery] int produtoId, [FromQuery] int categoriaId)
        {
            _produtoCategoriaDAL.Associar(produtoId, categoriaId);
            return Ok();
        }

        [HttpDelete("remover")]
        public IActionResult Remover([FromQuery] int produtoId, [FromQuery] int categoriaId)
        {
            _produtoCategoriaDAL.Remover(produtoId, categoriaId);
            return Ok(new { mensagem = "Associação removida com sucesso." });
        }

        [HttpPut("alterar")]
        public IActionResult Alterar([FromBody] AlterarCategoriasDTO dto)
        {
            _produtoCategoriaDAL.Alterar(dto.ProdutoId, dto.Categorias);
            return Ok(new { mensagem = "Categorias alteradas com sucesso." });
        }

        [HttpGet("categorias-por-produto/{produtoId}")]
        public IActionResult ObterCategoriasPorProduto(int produtoId)
        {
            var categorias = _produtoCategoriaDAL.ObterCategoriasPorProduto(produtoId);
            return Ok(categorias);
        }

    }
}
