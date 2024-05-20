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

}
