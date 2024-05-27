using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class Profil
    {
        public int IDProfil { get; set; }
        public int NumProfil { get; set; }
        public string NomProfil { get; set; }
        public bool Level10Played { get; set; }
        public bool Dialogue_Garde { get; set; }
        public bool Dialogue_Prison { get; set; }
        public bool Dialogue_Debut { get; set; }
        public bool Dialogue_Inge { get; set; }
        public int TotalPieces { get; set; }
        public int TotalBoulons { get; set; }
        public int UID { get; set; }
        public Utilisateur Utilisateur { get; set; }
    }
}
