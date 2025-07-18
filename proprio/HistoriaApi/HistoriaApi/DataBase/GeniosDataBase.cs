using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HistoriaApi.DataBase
{
    public class GeniosDataBase
    {
        // Pega a variável de ambiente lá do Azure (ConnectionString)
        private readonly IConfiguration _configuration;

        public GeniosDataBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //---

        public string DatabaseConnection()
        {
            //return _configuration.GetSection("ConnectionString").Value!.ToString();

            var json = File.ReadAllText("appsettings.json");
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json)!;
            string connectionString = jsonObj!["ConnectionStrings"]["DataBaseConnection"];

            return connectionString;
        }

        public List<Genio> SelectTodos()
        {
            List<Genio> listaGenios = new List<Genio>();

            SqlConnection conn = new SqlConnection(DatabaseConnection());
            SqlCommand cmd = new SqlCommand("SELECT * FROM genios;", conn);
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    listaGenios.Add(new Genio() 
                    {
                        ID = Int32.Parse(reader["ID"].ToString()),
                        Nome = reader["Nome"].ToString(),
                        AnoNascimento = Int32.Parse(reader["AnoNascimento"].ToString()),
                        AnoObito = Int32.Parse(reader["AnoObito"].ToString()),
                        Descricao = reader["Descricao"].ToString(),
                        Contribuicoes = reader["Contribuicoes"].ToString()
                    });
                }
            }
            cmd.Connection.Close();
            return listaGenios;
        }

        public Genio? GetGenioAno(int ano)
        {

            Genio? genio = null;

            SqlConnection conn = new SqlConnection(DatabaseConnection());
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM genios WHERE AnoNascimento <= @Ano AND AnoObito >= @Ano ORDER BY NEWID();", conn);
            cmd.Parameters.AddWithValue("@Ano", ano);
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                genio = new Genio 
                {
                    ID = Int32.Parse(reader["ID"].ToString()),
                    Nome = reader["Nome"].ToString(),
                    AnoNascimento = Int32.Parse(reader["AnoNascimento"].ToString()),
                    AnoObito = Int32.Parse(reader["AnoObito"].ToString()),
                    Descricao = reader["Descricao"].ToString(),
                    Contribuicoes = reader["Contribuicoes"].ToString(),
                };                
            }
            cmd.Connection.Close();
            return genio;
        }

        public int Post(Genio novoGenio)
        {
            try
            {
                SqlConnection conn = new SqlConnection(DatabaseConnection());
                string sql = @"INSERT INTO genios (Nome, AnoNascimento, AnoObito, Descricao, Contribuicoes) VALUES (@Nome, @AnoNascimento, @AnoObito, @Descricao, @Contribuicoes); SELECT SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Nome", novoGenio.Nome);
                cmd.Parameters.AddWithValue("@AnoNascimento", novoGenio.AnoNascimento);
                cmd.Parameters.AddWithValue("@AnoObito", novoGenio.AnoObito);
                cmd.Parameters.AddWithValue("@Descricao", novoGenio.Descricao);
                cmd.Parameters.AddWithValue("@Contribuicoes", novoGenio.Contribuicoes);
                cmd.Connection.Open();

                var result = cmd.ExecuteScalar();
                cmd.Connection.Close();
                return Convert.ToInt32(result);


            }
            catch (Exception)
            {
                return 0;
            }
        }

        public Genio? Uptade(int id, Genio uptadeGenio)
        {
            try
            {
                Genio? genio = null;

                SqlConnection conn = new SqlConnection(DatabaseConnection());
                SqlCommand cmd = new SqlCommand("UPDATE genios SET Nome = @Nome, AnoNascimento = @AnoNascimento, AnoObito = @AnoObito, Descricao = @Descricao, Contribuicoes = @Contribuicoes WHERE ID = @ID;", conn);
                cmd.Parameters.AddWithValue("@Nome", uptadeGenio.Nome);
                cmd.Parameters.AddWithValue("@AnoNascimento", uptadeGenio.AnoNascimento);
                cmd.Parameters.AddWithValue("@AnoObito", uptadeGenio.AnoObito);
                cmd.Parameters.AddWithValue("@Descricao", uptadeGenio.Descricao);
                cmd.Parameters.AddWithValue("@Contribuicoes", uptadeGenio.Contribuicoes);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();


                cmd.Connection.Close();
                return uptadeGenio;

            }
            catch (Exception)
            {

                return null;
            }

            
        }

        public bool Delete(int id) 
        {
            try
            {

                SqlConnection conn = new SqlConnection(DatabaseConnection());
                SqlCommand cmd = new SqlCommand("DELETE FROM genios WHERE ID = @ID;", conn);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
