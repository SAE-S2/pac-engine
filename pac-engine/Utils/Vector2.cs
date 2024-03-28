namespace pac_engine.Utils
{
	public class Vector2
	{
		public int x, y;

		public Vector2() { this.x = 0; this.y = 0; }
        public Vector2(int xy) { this.x = xy; this.y = xy; }
        public Vector2(int x, int y) { this.x = x; this.y = y; }

		public float Distance(Vector2 vector)
        {
            int diffX = this.x - vector.x;
            int diffY = this.y - vector.y;

            return (float)Math.Sqrt(diffX * diffX + diffY * diffY);
        }

		public Vector2 Lerp(Vector2 vector, float percent)
		{
			return new Vector2((int)(this.x * (1 - percent) + vector.x * percent), (int)(this.y * (1 - percent) + vector.y * percent));
		}

        private bool Equals(Vector2 vector)
        {
            return (this.x == vector.x && this.y == vector.y);
        }

        public static Vector2 operator +(Vector2 a, Vector2 b) 
			=> new Vector2(a.x + b.x, a.y + b.y);
        public static Vector2 operator -(Vector2 a, Vector2 b)
            => new Vector2(a.x - b.x, a.y - b.y);
        public static bool operator ==(Vector2 a, Vector2 b)
            => a.Equals(b);
        public static bool operator !=(Vector2 a, Vector2 b)
            => !(a.Equals(b));
    }
}