using Microsoft.Data.SqlClient;
using desafio_adonet.Models;

namespace desafio_adonet.Banco
{
    public class CategoriasDAL
    {
        public void Cadastrar(Categoria categoria)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "INSERT INTO Categorias (Nome) VALUES (@Nome)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Nome", categoria.Nome);
            command.ExecuteNonQuery();
        }

        public IEnumerable<Categoria> Listar()
        {
            var categorias = new List<Categoria>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT Id, Nome FROM Categorias";
            SqlCommand command = new SqlCommand(sql, connection);

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                categorias.Add(new Categoria
                {
                    Id = (int)reader["Id"],
                    Nome = reader["Nome"].ToString()
                });
            }

            return categorias;
        }
    }
}
