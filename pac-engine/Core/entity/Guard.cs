using pac_engine.Utils;

namespace pac_engine.Core
{
    public class Guard : Entity
    {
        public Guard()
        {
            this.maxHealth = 1.0f;
            this.Health = 1.0f;
            this.speed = 0.9f;
            this.damage = 1.0f;
            this.indice = indice;
        }  

        ~Guard()
        {
            indice--;
        }

        // Déplacement aléatoire
        public override async Task Movement(Map level)
        {
            bool posChange;
            Random random = new Random();
            int i = 0; // DEV VAR
            int iToGo = random.Next(1, 10); // DEV VAR
            await Task.Run(() =>
            {
                while (Health > 0)
                {
                    posChange = false;
                    Vector2 oldPos = new Vector2(pos.x, pos.y);
                    switch (angle)
                    {
                        case 0: // Haut
                            if (level.GetWall(pos.x - 1, pos.y)) { break; }
                            pos.x -= 1;
                            posChange = true;
                            break;
                        case 1: // Gauche
                            if (level.GetWall(pos.x, pos.y + 1)) { break; }
                            pos.y += 1;
                            posChange = true;
                            break;
                        case 2: // Bas
                            if (level.GetWall(pos.x + 1, pos.y)) { break; }
                            pos.x += 1;
                            posChange = true;
                            break;
                        case 3: // Droite
                            if (level.GetWall(pos.x, pos.y - 1)) { break; }
                            pos.y -= 1;
                            posChange = true;
                            break;
                        case 4: // Stop
                            break;
                    }

                    if (posChange)
                    {
                        eventPosChanged(oldPos, pos, indice);

                        if (actualGame.player.pos.x == pos.x && actualGame.player.pos.y == pos.y)
                        {
                            actualGame.player.TakeDamage(damage);
                        }
                    }

                    Task.Delay((int)(Globals.ENTITY_SPEED / speed)).Wait();

                    if (i == iToGo)
                    {
                        i = 0;
                        angle = random.Next(0, 4);
                        iToGo = random.Next(1, 10);
                    }
                    i++;
                }
            });
        }

        public bool CalledToPos()
        {
            // Call by chief guard
            return false;
        }
    }
}