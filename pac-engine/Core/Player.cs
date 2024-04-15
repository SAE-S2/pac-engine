using pac_engine.Utils;
namespace pac_engine.Core
{
	public class Player : Entity
	{
		public int money;
		public int bolts;

		public Player()
		{
			this.maxHealth = 3.5f;
			this.Health = 2.5f;
			this.speed = 1.0f;
			this.damage = 0.0f;
			this.money = 0;
			this.bolts = 0;
		}
	}
}

