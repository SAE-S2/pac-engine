﻿using System.ComponentModel;
using System.Numerics;

namespace pac_engine.Utils
{
    public class PositionChangedEventArgs
    {
        public Vector2 NewPos { get; set; }
        public Vector2 OldPos { get; set; }
        public int indice { get; set; }
    }

    public class KilledEventArgs
    {
        public int id { get; set; }
    }
    
    public class NothingsEventArgs
    {
    }

    public class EarnCoinEventArgs 
    {
        public Vector2 Pos { get; set;}
    }

    public class DamageEventArgs
    {
        public float playerHP { get; set; }
    }

    public class DoorOpenEventArgs
    {
        public Vector2 DoorPos { get; set;}
    }

    public class GameStateEventArgs
    {
        public bool win { get; set;}
        public int level { get; set;}
    }
}