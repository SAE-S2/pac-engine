using pac_engine.Utils;
using System.Numerics;

namespace pac_engine.Core
{
    public class PositionChangedEventArgs
    {
        public Utils.Vector2 NewPos {  get; set; }
        public Utils.Vector2 OldPos { get; set; }
        public Entity Entity { get; set; }
    }
}