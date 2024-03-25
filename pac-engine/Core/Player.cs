using pac_engine.Utils;
namespace pac_engine.Core
{
	public class Player : Entity
	{
		public int money;
		public int bolts;
		public int angle = 0;

		public Player()
		{
			this.maxHealth = 3.5f;
			this.Health = 2.5f;
			this.speed = 1.0f;
			this.damage = 0.0f;
			this.money = 100;
			this.bolts = 2;
		}

		public async Task Movement()
		{
			await Task.Run(() =>
			{
				while (this.Health > 0)
				{
					switch (this.angle)
					{
						case 0:
							this.pos.y += 1;
							break;
						case 1:
							this.pos.x += 1;
							break;
						case 2:
							this.pos.y -= 1;
							break;
						case 3:
							this.pos.x -= 1;
							break;
					}
                    Console.WriteLine($"Position: x={this.pos.x}, y={this.pos.y}");
                    Task.Delay(Globals.ENTITY_SPEED * (int)this.speed).Wait();
				}
			});
		}
		public void AngleChange(int angle)
		{
			if (this.angle < 0 && this.angle > 3)
			{
				return;
			}
			this.angle = angle;
		}
	}
}
