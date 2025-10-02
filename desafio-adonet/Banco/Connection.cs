using Microsoft.Data.SqlClient;

namespace desafio_adonet.Banco
{
    internal class Connection
    {
        private string connectionString = "Data Source=DBS-NOTE-889\\SQLEXPRESS11;Initial Catalog=DesafioAdoNet;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public SqlConnection ObterConexao()
        {
            return new SqlConnection(connectionString);
        }
    }
}

