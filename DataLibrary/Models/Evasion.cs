using database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class Evasion
    {
        public int NumEvasion { get; set; }
        public int UtilisationPouvoirs { get; set; }
        public int HPPerdus { get; set; }
        public int EnnemisTues { get; set; }
        public int NbPiece { get; set; }
        public int NiveauEvasion { get; set; }
        public int NbBoulon { get; set; }
        public int Score { get; set; }
        public int NumAmelioration { get; set; }
        public Amelioration Amelioration { get; set; }
        public int IDProfil { get; set; }
        public Profil Profil { get; set; }
    }
}

