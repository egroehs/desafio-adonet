using desafio_adonet.Models;
using Microsoft.Data.SqlClient;
namespace desafio_adonet.Banco;

// Data Access Layer
public class ProdutosDAL
{
    public IEnumerable<Produto> Listar()
    {
        var produtos = new List<Produto>();

        using var connection = new Connection().ObterConexao();
        connection.Open();

        string sql = "SELECT * FROM Produtos";
        SqlCommand command = new SqlCommand(sql, connection);

        using SqlDataReader dataReader = command.ExecuteReader();

        while (dataReader.Read())
        {
            int idProduto = Convert.ToInt32(dataReader["Id"]);
            string nomeProduto = Convert.ToString(dataReader["Nome"]);
            int precoProduto = Convert.ToInt32(dataReader["Preco"]);
            int quantidadeProduto = Convert.ToInt32(dataReader["Quantidade"]);

            produtos.Add(
                new Produto
                {
                    Id = idProduto,
                    Nome = nomeProduto,
                    Preco = precoProduto,
                    Quantidade = quantidadeProduto,
                }

            );
        }

        return produtos;
    }

    public Produto? ObterPorId(int id)
    {
        using var connection = new Connection().ObterConexao();
        connection.Open();

        string sql = "SELECT Id, Nome, Preco, Quantidade FROM Produtos WHERE Id=@Id";
        SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", id);

        using SqlDataReader dataReader = command.ExecuteReader();

        if (dataReader.Read())
        {
            return new Produto
            {
                Id = (int)dataReader["Id"],
                Nome = dataReader["Nome"].ToString(),
                Preco = (int)dataReader["Preco"],
                Quantidade = (int)dataReader["Quantidade"]
            };
        }

        return null;
    }

    public void Cadastrar(Produto produto)
    {
        using var connection = new Connection().ObterConexao();
        connection.Open();

        string sql = "INSERT INTO Produtos (Nome, Preco, Quantidade) VALUES (@Nome, @Preco, @Quantidade)";
        SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Nome", produto.Nome);
        command.Parameters.AddWithValue("@Preco", produto.Preco);
        command.Parameters.AddWithValue("@Quantidade", produto.Quantidade);

        command.ExecuteNonQuery();
    }

    public void Atualizar(Produto produto)
    {
        using var connection = new Connection().ObterConexao();
        connection.Open();

        string sql = "UPDATE Produtos SET Nome=@Nome, Preco=@Preco, Quantidade=@Quantidade WHERE Id=@Id";
        SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Nome", produto.Nome);
        command.Parameters.AddWithValue("@Preco", produto.Preco);
        command.Parameters.AddWithValue("@Quantidade", produto.Quantidade);
        command.Parameters.AddWithValue("@Id", produto.Id);
        command.ExecuteNonQuery();
        
    }

    public void Excluir(int id)
    {

        using var connection = new Connection().ObterConexao();
        connection.Open();
        string sql = "DELETE FROM Produtos WHERE Id=@Id";
        SqlCommand command = new SqlCommand(sql,connection);
        command.Parameters.AddWithValue("@Id", id);
        command.ExecuteNonQuery();
        
    }

    public IEnumerable<Produto> Filtrar(string? nome = null, int? precoMin = null, int? precoMax = null)
    {
        var produtos = new List<Produto>();
        using var connection = new Connection().ObterConexao();
        connection.Open();

        string sql = "SELECT Id, Nome, Preco, Quantidade FROM Produtos WHERE 1=1";

            if (!string.IsNullOrEmpty(nome))
            sql += " AND Nome LIKE @Nome";
            if (precoMin.HasValue)
            sql += " AND Preco >= @PrecoMin";
            if (precoMax.HasValue)
            sql += " AND Preco <= @PrecoMax";

            var command = new SqlCommand(sql, connection);

            if (!string.IsNullOrEmpty(nome))
            command.Parameters.AddWithValue("@Nome", "%" + nome + "%");
            if (precoMin.HasValue)
            command.Parameters.AddWithValue("@PrecoMin", precoMin.Value);
            if (precoMax.HasValue)
            command.Parameters.AddWithValue("@PrecoMax", precoMax.Value);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    produtos.Add(new Produto
                    {
                        Id = (int)reader["Id"],
                        Nome = reader["Nome"].ToString(),
                        Preco = (int)reader["Preco"],
                        Quantidade = (int)reader["Quantidade"]
                    });
                }
            }
        return produtos;
    }
}
