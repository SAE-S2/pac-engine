namespace pac_engine.Utils
{
	public class Vector2
	{
		public float x, y;

		public Vector2() { this.x = 0.0f; this.y = 0.0f; }
        public Vector2(float xy) { this.x = xy; this.y = xy; }
        public Vector2(float x, float y) { this.x = x; this.y = y; }

		public bool Equals(Vector2 vector)
		{
			return (this.x == vector.x && this.y == vector.y);
		}

		public float Distance(Vector2 vector)
        {
            float diffX = this.x - vector.x;
            float diffY = this.y - vector.y;

            return (float)Math.Sqrt(diffX * diffX + diffY * diffY);
        }

		public Vector2 Lerp(Vector2 vector, float percent)
		{
			return new Vector2(this.x * (1 - percent) + vector.x * percent, this.y * (1 - percent) + vector.y * percent);
		}

		public void Add(Vector2 vector)
		{
			this.x += vector.x;
			this.y += vector.y;
		}
    }
}