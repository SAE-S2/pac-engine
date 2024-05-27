using database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace database.Models
{
    public class EquipementPossede
    {
        public int IDProfil { get; set; }
        public Profil Profil { get; set; }
        public int NumAmelioration { get; set; }
        public Amelioration Amelioration { get; set; }
        public int NiveauAmelioration { get; set; }
    }
}
