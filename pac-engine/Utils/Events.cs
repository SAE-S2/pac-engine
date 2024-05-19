using System.ComponentModel;
using System.Numerics;

namespace pac_engine.Utils
{
    public class PositionChangedEventArgs
    {
        public Vector2 NewPos { get; set; }
        public Vector2 OldPos { get; set; }
    }

    public class EarnCoinEventArgs 
    {
        public Vector2 Pos { get; set;}
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