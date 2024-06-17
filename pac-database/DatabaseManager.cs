using System;
using System.Data.SQLite;

namespace PacDatabase
{
    public static class DatabaseManager
    {
        private static string connectionString;

        static DatabaseManager()
        {
            connectionString = $"Data Source=database.db;Version=3;";
            InitializeDatabase();
        }

        private static void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string createUtilisateurTable = @"
                    CREATE TABLE IF NOT EXISTS Utilisateur(
                        UID INTEGER PRIMARY KEY AUTOINCREMENT,
                        login VARCHAR(50) NOT NULL UNIQUE,
                        MdpUtilisateur VARCHAR(50) NOT NULL
                    );";
                var command = new SQLiteCommand(createUtilisateurTable, connection);
                command.ExecuteNonQuery();

                string createProfilTable = @"
                    CREATE TABLE IF NOT EXISTS Profil(
                        IDProfil INTEGER PRIMARY KEY AUTOINCREMENT,
                        NumProfil TINYINT NOT NULL,
                        NomProfil VARCHAR(50) NOT NULL,
                        Level10Played BOOLEAN,
                        Dialogue_Garde BOOLEAN,
                        Dialogue_Prison BOOLEAN,
                        Dialogue_Debut BOOLEAN,
                        Dialogue_Inge BOOLEAN,
                        TotalPieces INT,
                        TotalBoulons INT,
                        UID INT NOT NULL,
                        FOREIGN KEY(UID) REFERENCES Utilisateur(UID)
                    );";
                command = new SQLiteCommand(createProfilTable, connection);
                command.ExecuteNonQuery();

                string createAmeliorationTable = @"
                    CREATE TABLE IF NOT EXISTS Amelioration(
                        NumAmelioration INTEGER PRIMARY KEY AUTOINCREMENT,
                        Rarete TINYINT NOT NULL,
                        NomAmelioration VARCHAR(50) NOT NULL,
                        Description VARCHAR(200),
                        EstEquipable BOOLEAN
                    );";
                command = new SQLiteCommand(createAmeliorationTable, connection);
                command.ExecuteNonQuery();

                string createEquipementPossedeTable = @"
                    CREATE TABLE IF NOT EXISTS Equipement_Possede(
                        IDProfil INT,
                        NumAmelioration INT,
                        NiveauAmelioration INT,
                        PRIMARY KEY(IDProfil, NumAmelioration),
                        FOREIGN KEY(IDProfil) REFERENCES Profil(IDProfil),
                        FOREIGN KEY(NumAmelioration) REFERENCES Amelioration(NumAmelioration)
                    );";
                command = new SQLiteCommand(createEquipementPossedeTable, connection);
                command.ExecuteNonQuery();

                string createEvasionTable = @"
                    CREATE TABLE IF NOT EXISTS Evasion(
                        NumEvasion INTEGER PRIMARY KEY AUTOINCREMENT,
                        UtilisationPouvoirs INT,
                        HPPerdus INT,
                        EnnemisTues INT,
                        NbPiece INT,
                        NiveauEvasion INT,
                        NbBoulon INT,
                        Score INT GENERATED ALWAYS AS (NiveauEvasion * 1000 + EnnemisTues * 500 + NbBoulon * 100 + NbPiece * 10 - UtilisationPouvoirs * 250 - HPPerdus * 300) VIRTUAL,
                        NumAmelioration INT,
                        IDProfil INT NOT NULL,
                        FOREIGN KEY(NumAmelioration) REFERENCES Amelioration(NumAmelioration),
                        FOREIGN KEY(IDProfil) REFERENCES Profil(IDProfil)
                    );";
                command = new SQLiteCommand(createEvasionTable, connection);
                command.ExecuteNonQuery();
            }
        }

        public static void AddUtilisateur(string login, string mdpUtilisateur)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO Utilisateur (login, MdpUtilisateur) VALUES (@login, @mdpUtilisateur)";
                var command = new SQLiteCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@mdpUtilisateur", mdpUtilisateur);
                command.ExecuteNonQuery();
            }
        }

        public static void UpdateUtilisateur(int uid, string login, string mdpUtilisateur)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE Utilisateur SET login = @login, MdpUtilisateur = @mdpUtilisateur WHERE UID = @uid";
                var command = new SQLiteCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@uid", uid);
                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@mdpUtilisateur", mdpUtilisateur);
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteUtilisateur(int uid)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Utilisateur WHERE UID = @uid";
                var command = new SQLiteCommand(deleteQuery, connection);
                command.Parameters.AddWithValue("@uid", uid);
                command.ExecuteNonQuery();
            }
        }

        public static SQLiteDataReader GetUtilisateurs()
        {
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            string selectQuery = "SELECT * FROM Utilisateur";
            var command = new SQLiteCommand(selectQuery, connection);
            return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }
    }
}