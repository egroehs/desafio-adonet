using desafio_adonet.Models;
using Microsoft.Data.SqlClient;

namespace desafio_adonet.Banco
{
    public class ProdutoCategoriaDAL
    {
        public void Associar(int produtoId, int categoriaId)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "INSERT INTO ProdutoCategoria (ProdutoId, CategoriaId) VALUES (@ProdutoId, @CategoriaId)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ProdutoId", produtoId);
            command.Parameters.AddWithValue("@CategoriaId", categoriaId);
            command.ExecuteNonQuery();
        }

        public IEnumerable<Categoria> ObterCategoriasPorProduto(int produtoId)
        {
            var categorias = new List<Categoria>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = @"
            SELECT c.Id, c.Nome 
            FROM Categorias c
            INNER JOIN ProdutoCategoria pc ON c.Id = pc.CategoriaId
            WHERE pc.ProdutoId = @ProdutoId";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ProdutoId", produtoId);

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                categorias.Add(new Categoria
                {
                    Id = (int)reader["Id"],
                    Nome = reader["Nome"].ToString()
                });
            }

            var categoriasOrdenadas = categorias.OrderBy(c => c.Nome);

            return categoriasOrdenadas;
        }
    }
}
