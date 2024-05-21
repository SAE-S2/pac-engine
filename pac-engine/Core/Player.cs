using pac_engine.Utils;
namespace pac_engine.Core
{
    public class Player : Entity
    {
        public int money;
        public int bolts;
        public int lucky;
        public float absorption = 0.0f;
        private int selectedPower = 1;
        private Shield shield;
        private Invisible invisible;
        public bool isInvisible = false;
        private Damage damagePower;
        private CancellationTokenSource cancellationTokenSource;

        public void StartMovement(Map level)
        {
            cancellationTokenSource = new CancellationTokenSource();
            Movement(level, cancellationTokenSource.Token);
        }

        public void StopMovement()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }
        }

		public Player()
		{
	        // TODO: Load from db
			maxHealth = 3.0f;
		    Health = 3.0f;
	        speed = 1.0f;
            damage = 0.0f;
            money = 0;
            bolts = 0;
            lucky = 0; // %
            selectedPower = 2;
            shield = new Shield(1);
            damagePower = new Damage(1);
            invisible = new Invisible(1);
        }

        public void ActivePower()
        {
            switch (selectedPower) {
                case 1:
                    shield.Active(this);
                    break;
                case 2:
                    damagePower.Active(this);
                    break;
                case 3:
                    invisible.Active(this);
                    break;
            }
        }

        public new bool TakeDamage(float damage)
        {
            if (imortal)
                return false;

            if (absorption > damage)
            {
                absorption -= damage;
                if (absorption <= 0.1f)
                    absorption = 0.0f;
            }
            else
            {
                if (absorption >= 0.1f)
                {
                    damage -= absorption;
                    absorption = 0;
                }

                Health -= damage;

                if (Health <= 0.1f)
                    Kill();
            }
            return true;
        }

        public new bool Kill()
        {
            if (imortal)
                return false;
            if (actualGame != null)
                actualGame.PlayerDied();
            return true;
        }

        public async Task Movement(Map level, CancellationToken token)
        {
            bool posChange;
            await Task.Run(() =>
            {
                while (actualGame != null && actualGame.Playing)
                {
                    if (token.IsCancellationRequested)
                    {
                        break; // Sortir de la boucle si l'annulation est demandée
                    }

                    posChange = false;
                    switch (angle)
                    {
                        case 0: //Z (Haut)
                            if (level.GetWall(pos.x - 1, pos.y) && level.map[pos.x - 1, pos.y] != 6) { break; }
                            posChange = true;
                            eventPosChanged(pos, new Vector2(pos.x - 1, pos.y), indice: -1);
                            pos.x -= 1;
                            break;
                        case 1: //Q (Gauche)
                            if (level.GetWall(pos.x, pos.y + 1) && level.map[pos.x, pos.y + 1] != 6) { break; }
                            posChange = true;
                            eventPosChanged(pos, new Vector2(pos.x, pos.y + 1), indice: -1);
                            pos.y += 1;
                            break;
                        case 2: //S (Bas)
                            if (level.GetWall(pos.x + 1, pos.y) && level.map[pos.x + 1, pos.y] != 6) { break; }
                            posChange = true;
                            eventPosChanged(pos, new Vector2(pos.x + 1, pos.y), indice: -1);
                            pos.x += 1;
                            break;
                        case 3: //D (Droite)
                            if (level.GetWall(pos.x, pos.y - 1) && level.map[pos.x, pos.y - 1] != 6) { break; }
                            posChange = true;
                            eventPosChanged(pos, new Vector2(pos.x, pos.y - 1), indice: -1);
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

                        if (level.GetCoin(pos))
                        {
                            money++;
                            Random random = new Random();
                            int chance = random.Next(1, 100);
                            if (lucky > chance)
                                money++;
                        }
                        else if (level.GetBolt(pos))
                            bolts++;

                        Entity enemy = actualGame.EnemyAtPos(pos);
                        if (enemy != this)
                        {
                            if (damage > 0.1f)
                            {
                                enemy.TakeDamage(damage);
                            }
                            else
                            {
                                TakeDamage(enemy.damage);
                            }
                        }

                        if (pos.x == level.door.x && pos.y == level.door.y)
                            actualGame.PlayerAtDoor();
                    }

                    Task.Delay((int)(Globals.ENTITY_SPEED / speed)).Wait();
                }
            }, token);
        }

    }
}