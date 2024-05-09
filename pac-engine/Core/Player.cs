using pac_engine.Utils;
namespace pac_engine.Core
{
	public class Player : Entity
	{
		public int money;
		public int bolts;

		public Player()
		{
			// TODO: Load from db
			maxHealth = 3.0f;
			Health = 3.0f;
			speed = 1.0f;
			damage = 0.0f;
			money = 0;
			bolts = 0;
		}

        public new bool TakeDamage(float damage)
        {
            if (imortal)
                return false;

            Health -= damage;

            if (Health <= 0.1f)
                Kill();

            return true;
        }

        public new bool Kill()
        {
            if (imortal)
                return false;
            
            actualGame.PlayerDied();
            return true;
        }

        public new async Task Movement(Map level)
        {
            bool posChange;
            await Task.Run(() =>
            {
                while (actualGame.Playing)
                {
                    posChange = false;
                    switch (angle)
                    {
                        case 0: //Z (Haut)
                            if (level.GetWall(pos.x - 1, pos.y)) { break; }
                            posChange = true;
                            pos.x -= 1;
                            break;
                        case 1: //Q (Gauche)
                            if (level.GetWall(pos.x, pos.y + 1)) { break; }
                            posChange = true;
                            pos.y += 1;
                            break;
                        case 2: //S (Bas)
                            if (level.GetWall(pos.x + 1, pos.y)) { break; }
                            posChange = true;
                            pos.x += 1;
                            break;
                        case 3: //D (Droite)
                            if (level.GetWall(pos.x, pos.y - 1)) { break; }
                            posChange = true;
                            pos.y -= 1;
                            break;
                        case 4: //STOP
                            break;
                    }

                    if (posChange)
                    {
                        if (level.EarnCoin(pos))
                            money++;
                        else if (level.EarnBolt(pos))
                            bolts++;

                        float damage = actualGame.EnemieAtPos(pos);
                        if (damage > 0.1f)
                            TakeDamage(damage);

                        if (pos.x == level.door.x && pos.y == level.door.y)
                            actualGame.PlayerAtDoor();
                    }

                    Task.Delay((int)(Globals.ENTITY_SPEED * speed)).Wait();
                }
            });
        }
    }
}