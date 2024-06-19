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

        public static int GetIDProfil(int uid, int numProfil)
        {
            int idProfil = -1;

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT IDProfil 
                        FROM Profil 
                        WHERE UID = @uid AND NumProfil = @numProfil";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@uid", uid);
                        command.Parameters.AddWithValue("@numProfil", numProfil);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idProfil = reader.GetInt32(0);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }

            return idProfil;
        }

        public static int GetTotalPieces(int uid, int numProfil)
        {
            int totalPieces = 0;

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT TotalPieces FROM Profil WHERE UID = @uid AND NumProfil = @numProfil";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@uid", uid);
                        command.Parameters.AddWithValue("@numProfil", numProfil);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                totalPieces = reader.GetInt32(0);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }

            return totalPieces;
        }

        public static int GetTotalBoulons(int uid, int numProfil)
        {
            int totalBoulons = 0;

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT TotalBoulons FROM Profil WHERE UID = @uid AND NumProfil = @numProfil";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@uid", uid);
                        command.Parameters.AddWithValue("@numProfil", numProfil);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                totalBoulons = reader.GetInt32(0);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }

            return totalBoulons;
        }

        public static bool GetDialogueGarde(int uid, int numProfil)
        {
            bool dialogueGarde = false;

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Requête pour obtenir la valeur de Dialogue_Garde
                    string query = @"
                SELECT Dialogue_Garde 
                FROM Profil 
                WHERE UID = @uid AND NumProfil = @numProfil";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@uid", uid);
                        command.Parameters.AddWithValue("@numProfil", numProfil);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                dialogueGarde = reader.GetBoolean(0);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }

            return dialogueGarde;
        }

        public static bool GetLevel10Played(int uid, int numProfil)
        {
            bool level10Played = false;

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT Level10Played 
                FROM Profil 
                WHERE UID = @uid AND NumProfil = @numProfil";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@uid", uid);
                        command.Parameters.AddWithValue("@numProfil", numProfil);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                level10Played = reader.GetBoolean(0);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }

            return level10Played;
        }

        public static bool GetDialoguePrison(int uid, int numProfil)
        {
            bool dialoguePrison = false;

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT Dialogue_Prison 
                FROM Profil 
                WHERE UID = @uid AND NumProfil = @numProfil";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@uid", uid);
                        command.Parameters.AddWithValue("@numProfil", numProfil);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                dialoguePrison = reader.GetBoolean(0);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }

            return dialoguePrison;
        }

        public static bool GetDialogueDebut(int uid, int numProfil)
        {
            bool dialogueDebut = false;

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT Dialogue_Debut 
                FROM Profil 
                WHERE UID = @uid AND NumProfil = @numProfil";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@uid", uid);
                        command.Parameters.AddWithValue("@numProfil", numProfil);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                dialogueDebut = reader.GetBoolean(0);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }

            return dialogueDebut;
        }

        public static bool GetDialogueInge(int uid, int numProfil)
        {
            bool dialogueInge = false;

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT Dialogue_Inge 
                FROM Profil 
                WHERE UID = @uid AND NumProfil = @numProfil";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@uid", uid);
                        command.Parameters.AddWithValue("@numProfil", numProfil);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                dialogueInge = reader.GetBoolean(0);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }

            return dialogueInge;
        }

        public static void SetTotalBoulons(int uid, int numProfil, int totalBoulons)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Requête pour mettre à jour le nombre de boulons
                    string updateQuery = @"
                UPDATE Profil 
                SET TotalBoulons = @totalBoulons 
                WHERE UID = @uid AND NumProfil = @numProfil";

                    using (var updateCommand = new SQLiteCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@totalBoulons", totalBoulons);
                        updateCommand.Parameters.AddWithValue("@uid", uid);
                        updateCommand.Parameters.AddWithValue("@numProfil", numProfil);

                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Total de boulons mis à jour avec succès.");
                        }
                        else
                        {
                            Console.WriteLine("Aucune mise à jour effectuée. Vérifiez les paramètres.");
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }
        }

        public static void SetTotalPieces(int uid, int numProfil, int totalPieces)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Requête pour mettre à jour le nombre de boulons
                    string updateQuery = @"
                UPDATE Profil 
                SET TotalPieces = @totalPieces 
                WHERE UID = @uid AND NumProfil = @numProfil";

                    using (var updateCommand = new SQLiteCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@totalPieces", totalPieces);
                        updateCommand.Parameters.AddWithValue("@uid", uid);
                        updateCommand.Parameters.AddWithValue("@numProfil", numProfil);

                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Total de pieces mis à jour avec succès.");
                        }
                        else
                        {
                            Console.WriteLine("Aucune mise à jour effectuée. Vérifiez les paramètres.");
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }
        }

        public static void SetDialogueGarde(int uid, int numProfil, bool dialogueGarde)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Requête pour mettre à jour la valeur de Dialogue_Garde
                    string updateQuery = @"
                UPDATE Profil 
                SET Dialogue_Garde = @dialogueGarde 
                WHERE UID = @uid AND NumProfil = @numProfil";

                    using (var updateCommand = new SQLiteCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@dialogueGarde", dialogueGarde);
                        updateCommand.Parameters.AddWithValue("@uid", uid);
                        updateCommand.Parameters.AddWithValue("@numProfil", numProfil);

                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Dialogue_Garde mis à jour avec succès.");
                        }
                        else
                        {
                            Console.WriteLine("Aucune mise à jour effectuée. Vérifiez les paramètres.");
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }
        }

        public static void SetLevel10Played(int uid, int numProfil, bool level10Played)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string updateQuery = @"
                UPDATE Profil 
                SET Level10Played = @level10Played 
                WHERE UID = @uid AND NumProfil = @numProfil";

                    using (var updateCommand = new SQLiteCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@level10Played", level10Played);
                        updateCommand.Parameters.AddWithValue("@uid", uid);
                        updateCommand.Parameters.AddWithValue("@numProfil", numProfil);

                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Level10Played mis à jour avec succès.");
                        }
                        else
                        {
                            Console.WriteLine("Aucune mise à jour effectuée. Vérifiez les paramètres.");
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }
        }

        public static void SetDialoguePrison(int uid, int numProfil, bool dialoguePrison)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string updateQuery = @"
                UPDATE Profil 
                SET Dialogue_Prison = @dialoguePrison 
                WHERE UID = @uid AND NumProfil = @numProfil";

                    using (var updateCommand = new SQLiteCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@dialoguePrison", dialoguePrison);
                        updateCommand.Parameters.AddWithValue("@uid", uid);
                        updateCommand.Parameters.AddWithValue("@numProfil", numProfil);

                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Dialogue_Prison mis à jour avec succès.");
                        }
                        else
                        {
                            Console.WriteLine("Aucune mise à jour effectuée. Vérifiez les paramètres.");
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }
        }

        public static void SetDialogueDebut(int uid, int numProfil, bool dialogueDebut)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string updateQuery = @"
                UPDATE Profil 
                SET Dialogue_Debut = @dialogueDebut 
                WHERE UID = @uid AND NumProfil = @numProfil";

                    using (var updateCommand = new SQLiteCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@dialogueDebut", dialogueDebut);
                        updateCommand.Parameters.AddWithValue("@uid", uid);
                        updateCommand.Parameters.AddWithValue("@numProfil", numProfil);

                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Dialogue_Debut mis à jour avec succès.");
                        }
                        else
                        {
                            Console.WriteLine("Aucune mise à jour effectuée. Vérifiez les paramètres.");
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }
        }

        public static void SetDialogueInge(int uid, int numProfil, bool dialogueInge)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string updateQuery = @"
                UPDATE Profil 
                SET Dialogue_Inge = @dialogueInge 
                WHERE UID = @uid AND NumProfil = @numProfil";

                    using (var updateCommand = new SQLiteCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@dialogueInge", dialogueInge);
                        updateCommand.Parameters.AddWithValue("@uid", uid);
                        updateCommand.Parameters.AddWithValue("@numProfil", numProfil);

                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Dialogue_Inge mis à jour avec succès.");
                        }
                        else
                        {
                            Console.WriteLine("Aucune mise à jour effectuée. Vérifiez les paramètres.");
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }
        }

        // Méthodes pour la table Equipement_Possede

        public static void InitializeEquipementPossede(int idProfil)
        {
            for (int i = 1; i <= 8; i++)
                AddEquipementPossede(idProfil, i, 0);
        }

        public static void DeleteStuff(int idProfil) 
        {
            for(int i = 1;i <= 8; i++)
            {
                DeleteEquipementPossede(idProfil, i);
            }
        }

        public static void AddEquipementPossede(int idProfil, int numAmelioration, int niveauAmelioration)
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

        public static int GetNiveauAmelioration(int uid, int numProfil, int numAmelioration)
        {
            int niveauAmelioration = 0;

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT NiveauAmelioration 
                FROM Equipement_Possede 
                WHERE IDProfil = (
                    SELECT IDProfil 
                    FROM Profil 
                    WHERE UID = @uid AND NumProfil = @numProfil
                ) AND NumAmelioration = @numAmelioration";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@uid", uid);
                        command.Parameters.AddWithValue("@numProfil", numProfil);
                        command.Parameters.AddWithValue("@numAmelioration", numAmelioration);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                niveauAmelioration = reader.GetInt32(0);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }

            return niveauAmelioration;
        }

        public static void IncrementNiveauAmelioration(int uid, int numProfil, int numAmelioration)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Requête pour mettre à jour le niveau actuel en l'incrémentant de 1
                    string updateQuery = @"
                UPDATE Equipement_Possede 
                SET NiveauAmelioration = NiveauAmelioration + 1
                WHERE IDProfil = (
                    SELECT IDProfil 
                    FROM Profil 
                    WHERE UID = @uid AND NumProfil = @numProfil
                ) AND NumAmelioration = @numAmelioration";

                    using (var updateCommand = new SQLiteCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@uid", uid);
                        updateCommand.Parameters.AddWithValue("@numProfil", numProfil);
                        updateCommand.Parameters.AddWithValue("@numAmelioration", numAmelioration);

                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Debug.WriteLine("Niveau d'amélioration incrémenté avec succès.");
                        }
                        else
                        {
                            Debug.WriteLine("Aucune mise à jour effectuée. Vérifiez les paramètres.");
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite error: {ex.Message}");
            }
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