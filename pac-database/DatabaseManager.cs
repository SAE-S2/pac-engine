using System;
using System.Data.SQLite;
using System.Diagnostics;

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

        // Méthodes pour la table Utilisateur
        public static void AddUtilisateur(string login, string mdpUtilisateur)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO Utilisateur (login, MdpUtilisateur) VALUES (@login, @mdpUtilisateur)";
                    using (var command = new SQLiteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@mdpUtilisateur", mdpUtilisateur);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }
        }

        public static void UpdateUtilisateur(int uid, string login, string mdpUtilisateur)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string updateQuery = "UPDATE Utilisateur SET login = @login, MdpUtilisateur = @mdpUtilisateur WHERE UID = @uid";
                    using (var command = new SQLiteCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@uid", uid);
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@mdpUtilisateur", mdpUtilisateur);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }
        }

        public static void DeleteUtilisateur(int uid)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM Utilisateur WHERE UID = @uid";
                    using (var command = new SQLiteCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@uid", uid);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }
        }

        public static List<(int, string, string)> GetUtilisateurs()
        {
            var utilisateurs = new List<(int, string, string)>();

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM Utilisateur";
                    using (var command = new SQLiteCommand(selectQuery, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            utilisateurs.Add((reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }

            return utilisateurs;
        }

        // Méthodes pour la table Profil
        public static void AddProfil(int numProfil, string nomProfil, bool level10Played, bool dialogueGarde, bool dialoguePrison, bool dialogueDebut, bool dialogueInge, int totalPieces, int totalBoulons, int uid)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string insertQuery = @"INSERT INTO Profil (NumProfil, NomProfil, Level10Played, Dialogue_Garde, Dialogue_Prison, Dialogue_Debut, Dialogue_Inge, TotalPieces, TotalBoulons, UID)
                                       VALUES (@numProfil, @nomProfil, @level10Played, @dialogueGarde, @dialoguePrison, @dialogueDebut, @dialogueInge, @totalPieces, @totalBoulons, @uid)";
                    using (var command = new SQLiteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@numProfil", numProfil);
                        command.Parameters.AddWithValue("@nomProfil", nomProfil);
                        command.Parameters.AddWithValue("@level10Played", level10Played);
                        command.Parameters.AddWithValue("@dialogueGarde", dialogueGarde);
                        command.Parameters.AddWithValue("@dialoguePrison", dialoguePrison);
                        command.Parameters.AddWithValue("@dialogueDebut", dialogueDebut);
                        command.Parameters.AddWithValue("@dialogueInge", dialogueInge);
                        command.Parameters.AddWithValue("@totalPieces", totalPieces);
                        command.Parameters.AddWithValue("@totalBoulons", totalBoulons);
                        command.Parameters.AddWithValue("@uid", uid);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }
        }

        public static string GetProfil_name(int uid,int numProfil)
        {
            string name = null;
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string selectQuery = "SELECT NomProfil FROM Profil WHERE UID = @uid AND numProfil = @numProfil";
                    using (var command = new SQLiteCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@uid", uid);
                        command.Parameters.AddWithValue("@numProfil", numProfil);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                name = reader.GetString(0);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }

            return name;
        }

        public static void UpdateProfil(int idProfil, int numProfil, string nomProfil, bool level10Played, bool dialogueGarde, bool dialoguePrison, bool dialogueDebut, bool dialogueInge, int totalPieces, int totalBoulons, int uid)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string updateQuery = @"UPDATE Profil 
                                       SET NumProfil = @numProfil, NomProfil = @nomProfil, Level10Played = @level10Played, Dialogue_Garde = @dialogueGarde, Dialogue_Prison = @dialoguePrison, Dialogue_Debut = @dialogueDebut, Dialogue_Inge = @dialogueInge, TotalPieces = @totalPieces, TotalBoulons = @totalBoulons, UID = @uid
                                       WHERE IDProfil = @idProfil";
                    using (var command = new SQLiteCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@idProfil", idProfil);
                        command.Parameters.AddWithValue("@numProfil", numProfil);
                        command.Parameters.AddWithValue("@nomProfil", nomProfil);
                        command.Parameters.AddWithValue("@level10Played", level10Played);
                        command.Parameters.AddWithValue("@dialogueGarde", dialogueGarde);
                        command.Parameters.AddWithValue("@dialoguePrison", dialoguePrison);
                        command.Parameters.AddWithValue("@dialogueDebut", dialogueDebut);
                        command.Parameters.AddWithValue("@dialogueInge", dialogueInge);
                        command.Parameters.AddWithValue("@totalPieces", totalPieces);
                        command.Parameters.AddWithValue("@totalBoulons", totalBoulons);
                        command.Parameters.AddWithValue("@uid", uid);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }
        }

        public static void DeleteProfil(int numProfil, int uid)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM Profil WHERE uid = @uid AND numProfil = @numProfil";
                    using (var command = new SQLiteCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@uid", uid);
                        command.Parameters.AddWithValue("@numProfil", numProfil);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }
        }

        public static List<(int, int, string, bool, bool, bool, bool, bool, int, int, int)> GetProfils()
        {
            var profils = new List<(int, int, string, bool, bool, bool, bool, bool, int, int, int)>();

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM Profil";
                    using (var command = new SQLiteCommand(selectQuery, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            profils.Add((reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetBoolean(3), reader.GetBoolean(4), reader.GetBoolean(5), reader.GetBoolean(6), reader.GetBoolean(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10)));
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }

            return profils;
        }

        // Méthodes pour la table Amelioration
        public static void AddAmelioration(int rarete, string nomAmelioration, string description, bool estEquipable)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string insertQuery = @"INSERT INTO Amelioration (Rarete, NomAmelioration, Description, EstEquipable)
                                       VALUES (@rarete, @nomAmelioration, @description, @estEquipable)";
                    using (var command = new SQLiteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@rarete", rarete);
                        command.Parameters.AddWithValue("@nomAmelioration", nomAmelioration);
                        command.Parameters.AddWithValue("@description", description);
                        command.Parameters.AddWithValue("@estEquipable", estEquipable);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }
        }

        public static void UpdateAmelioration(int numAmelioration, int rarete, string nomAmelioration, string description, bool estEquipable)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string updateQuery = @"UPDATE Amelioration 
                                       SET Rarete = @rarete, NomAmelioration = @nomAmelioration, Description = @description, EstEquipable = @estEquipable
                                       WHERE NumAmelioration = @numAmelioration";
                    using (var command = new SQLiteCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@numAmelioration", numAmelioration);
                        command.Parameters.AddWithValue("@rarete", rarete);
                        command.Parameters.AddWithValue("@nomAmelioration", nomAmelioration);
                        command.Parameters.AddWithValue("@description", description);
                        command.Parameters.AddWithValue("@estEquipable", estEquipable);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }
        }

        public static void DeleteAmelioration(int numAmelioration)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM Amelioration WHERE NumAmelioration = @numAmelioration";
                    using (var command = new SQLiteCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@numAmelioration", numAmelioration);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }
        }

        public static List<(int, int, string, string, bool)> GetAmeliorations()
        {
            var ameliorations = new List<(int, int, string, string, bool)>();

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM Amelioration";
                    using (var command = new SQLiteCommand(selectQuery, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ameliorations.Add((reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetBoolean(4)));
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }

            return ameliorations;
        }

        // Méthodes pour la table Equipement_Possede
        public static void AddEquipementPossede(int idProfil, int numAmelioration, int niveauAmelioration)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string insertQuery = @"INSERT INTO Equipement_Possede (IDProfil, NumAmelioration, NiveauAmelioration)
                                       VALUES (@idProfil, @numAmelioration, @niveauAmelioration)";
                    using (var command = new SQLiteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@idProfil", idProfil);
                        command.Parameters.AddWithValue("@numAmelioration", numAmelioration);
                        command.Parameters.AddWithValue("@niveauAmelioration", niveauAmelioration);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }
        }

        public static void UpdateEquipementPossede(int idProfil, int numAmelioration, int niveauAmelioration)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string updateQuery = @"UPDATE Equipement_Possede 
                                       SET NiveauAmelioration = @niveauAmelioration
                                       WHERE IDProfil = @idProfil AND NumAmelioration = @numAmelioration";
                    using (var command = new SQLiteCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@idProfil", idProfil);
                        command.Parameters.AddWithValue("@numAmelioration", numAmelioration);
                        command.Parameters.AddWithValue("@niveauAmelioration", niveauAmelioration);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }
        }

        public static void DeleteEquipementPossede(int idProfil, int numAmelioration)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM Equipement_Possede WHERE IDProfil = @idProfil AND NumAmelioration = @numAmelioration";
                    using (var command = new SQLiteCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@idProfil", idProfil);
                        command.Parameters.AddWithValue("@numAmelioration", numAmelioration);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }
        }

        public static List<(int, int, int)> GetEquipementsPossedes()
        {
            var equipements = new List<(int, int, int)>();

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM Equipement_Possede";
                    using (var command = new SQLiteCommand(selectQuery, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            equipements.Add((reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }

            return equipements;
        }

        // Méthodes pour la table Evasion
        public static void AddEvasion(int utilisationPouvoirs, int hpPerdus, int ennemisTues, int nbPiece, int niveauEvasion, int nbBoulon, int numAmelioration, int idProfil)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string insertQuery = @"INSERT INTO Evasion (UtilisationPouvoirs, HPPerdus, EnnemisTues, NbPiece, NiveauEvasion, NbBoulon, NumAmelioration, IDProfil)
                                       VALUES (@utilisationPouvoirs, @hpPerdus, @ennemisTues, @nbPiece, @niveauEvasion, @nbBoulon, @numAmelioration, @idProfil)";
                    using (var command = new SQLiteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@utilisationPouvoirs", utilisationPouvoirs);
                        command.Parameters.AddWithValue("@hpPerdus", hpPerdus);
                        command.Parameters.AddWithValue("@ennemisTues", ennemisTues);
                        command.Parameters.AddWithValue("@nbPiece", nbPiece);
                        command.Parameters.AddWithValue("@niveauEvasion", niveauEvasion);
                        command.Parameters.AddWithValue("@nbBoulon", nbBoulon);
                        command.Parameters.AddWithValue("@numAmelioration", numAmelioration);
                        command.Parameters.AddWithValue("@idProfil", idProfil);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }
        }

        public static void UpdateEvasion(int numEvasion, int utilisationPouvoirs, int hpPerdus, int ennemisTues, int nbPiece, int niveauEvasion, int nbBoulon, int numAmelioration, int idProfil)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string updateQuery = @"UPDATE Evasion 
                                       SET UtilisationPouvoirs = @utilisationPouvoirs, HPPerdus = @hpPerdus, EnnemisTues = @ennemisTues, NbPiece = @nbPiece, NiveauEvasion = @niveauEvasion, NbBoulon = @nbBoulon, NumAmelioration = @numAmelioration, IDProfil = @idProfil
                                       WHERE NumEvasion = @numEvasion";
                    using (var command = new SQLiteCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@numEvasion", numEvasion);
                        command.Parameters.AddWithValue("@utilisationPouvoirs", utilisationPouvoirs);
                        command.Parameters.AddWithValue("@hpPerdus", hpPerdus);
                        command.Parameters.AddWithValue("@ennemisTues", ennemisTues);
                        command.Parameters.AddWithValue("@nbPiece", nbPiece);
                        command.Parameters.AddWithValue("@niveauEvasion", niveauEvasion);
                        command.Parameters.AddWithValue("@nbBoulon", nbBoulon);
                        command.Parameters.AddWithValue("@numAmelioration", numAmelioration);
                        command.Parameters.AddWithValue("@idProfil", idProfil);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }
        }

        public static void DeleteEvasion(int numEvasion)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM Evasion WHERE NumEvasion = @numEvasion";
                    using (var command = new SQLiteCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@numEvasion", numEvasion);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }
        }

        public static List<(int, int, int, int, int, int, int, int, int)> GetEvasions()
        {
            var evasions = new List<(int, int, int, int, int, int, int, int, int)>();

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM Evasion";
                    using (var command = new SQLiteCommand(selectQuery, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            evasions.Add((reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8)));
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }

            return evasions;
        }
    }
}