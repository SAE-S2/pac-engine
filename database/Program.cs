using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.EntityFrameworkCore.Extensions;



public class Evasion
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    public int NumEvasion { get; set; }
    public int UtilisationPouvoirs { get; set; }
    public int HPPerdus { get; set; }
    public int EnnemisTues { get; set; }
    public int NbPiece { get; set; }
    public int NbBoulon { get; set; }
    public int Score { get; set; }
    public int NumAmelioration { get; set; }
    public int IDProfil { get; set; }
}
public class Equipement_Possede
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    public int IDProfil { get; set; }       
    public int NumAmelioration { get; set; }
    public int NiveauAmelioration { get; set; }
}
internal class Utilisateur
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    public int UID { get; set; }
    public string login { get; set; }
    public string MdpUtilisateur { get; set; }
}
public class Profil
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    public int IDProfil { get; set; }
    public int NumProfil { get; set; }
    public string NomProfil { get; set; }
    public bool Level10Played { get; set; }
    public bool Dialogue_Garde{ get; set; }
    public bool Dialogue_Prison { get; set; }
    public bool Dialogue_Debut { get; set; }
    public bool Dialogue_Inge { get; set; }
    public int TotalPieces { get; set; }
    public int TotalBoulons { get; set; }
    public int UID { get; set; }
}
public class Amelioration
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    public int NumAmelioration { get; set; }
    public int Rarete { get; set; }
    public string NomAmelioration { get; set; }
    public string Descripption { get; set; }
    public bool Est_Equipable { get; set; }
}

public class PacBotContext : DbContext
{
    public DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Remplacez par votre chaîne de connexion MongoDB
        var connectionString = "mongodb+srv://PacBot:PacBot2024@pacbot.yzzchig.mongodb.net/?retryWrites=true&w=majority&appName=PacBot";
        var databaseName = "PacBot";

        optionsBuilder.UseMongoDB(connectionString, databaseName);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuration supplémentaire si nécessaire
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        using var context = new PacBotContext();

        // Ajouter un nouvel utilisateur
        var nouvelUtilisateur = new Utilisateur
        {
            UID = 3,
            login = "PacTester",
            MdpUtilisateur = "123456789"
        };

        await context.Utilisateurs.AddAsync(nouvelUtilisateur);
        await context.SaveChangesAsync();
        Console.WriteLine("Utilisateur ajouté avec succès");

        // Récupérer tous les utilisateurs
        var utilisateurs = await context.Utilisateurs.ToListAsync();
        foreach (var utilisateur in utilisateurs)
        {
            Console.WriteLine($"UID: {utilisateur.UID}, Login: {utilisateur.login}");
        }
    }
}



