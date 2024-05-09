using MySqlConnector;
namespace database
{
    public class connect
    {
        public static MySqlDataReader Main(string query)
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "10.1.139.236",
                UserID = "a3 ",
                Password = "PacBot2024",
                Database = "basea3",
            };

            using var cn = new MySqlConnection(builder.ToString());
            cn.Open();
            using var cmd = new MySqlCommand(query, cn);
            return cmd.ExecuteReader();
        }
        
        public string[] getProfile(int UID)
        {
            string[] profile = [];
            int i=0;
            MySqlDataReader reader = Main($"SELECT * FROM profile WHERE UID ={UID}");
            while (reader.Read())
            {
                profile[i] = reader["UID"].ToString + ";" + reader["NomProfil"].ToString;
                i++;
            }
            return profile;
        }
    }
}