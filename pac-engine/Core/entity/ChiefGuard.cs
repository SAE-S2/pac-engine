using pac_engine.Utils;

namespace pac_engine.Core
{
    public class ChiefGuard : Entity
    {
        public ChiefGuard()
        {
            this.maxHealth = 1.0f;
            this.Health = 1.0f;
            this.speed = 0.7f;
            this.damage = 0.5f;
        }

        // Déplacement intelligent
        public override async Task Movement(Map level)
        {
            bool posChange;
            int nbDeplacment = 30;
            ShortestPath shortestPath = new ShortestPath(actualGame.map.map);
            List<int> chemin = new List<int>(); // Contient les angles du chemins
            await Task.Run(() =>
            {
                while (Health > 0)
                {
                    if (nbDeplacment >= 30)
                    {
                        chemin = shortestPath.FindShortestPath(pos.x, pos.y, actualGame.player.pos.x, actualGame.player.pos.y);
                        nbDeplacment -= 30; //Reset compteur
                    }

                    if (chemin == null || chemin.Count == 0)
                    {
                        nbDeplacment++;
                        Task.Delay((int)(Globals.ENTITY_SPEED * speed)).Wait();
                        continue;
                    }

                    posChange = false;
                    if (chemin[0] != angle)
                    {
                        AngleChange(chemin[0]);
                    }
                    chemin.RemoveAt(0);
                    switch (angle)
                    {
                        case 0: //Z (Haut)
                            if (level.GetWall((int)pos.x - 1, (int)pos.y)) { break; }
                            pos.x -= 1;
                            posChange = true;
                            break;
                        case 3: //Q (Gauche)
                            if (level.GetWall((int)pos.x, (int)pos.y + 1)) { break; }
                            pos.y += 1;
                            posChange = true;
                            break;
                        case 2: //S (Bas)
                            if (level.GetWall((int)pos.x + 1, (int)pos.y)) { break; }
                            pos.x += 1;
                            posChange = true;
                            break;
                        case 1: //D (Droite)
                            if (level.GetWall((int)pos.x, (int)pos.y - 1)) { break; }
                            pos.y -= 1;
                            posChange = true;
                            break;
                        case 4: //STOP
                            break;
                    }

                    if (posChange && actualGame.player.pos.x == pos.x && actualGame.player.pos.y == pos.y)
                        actualGame.player.TakeDamage(damage);

                    nbDeplacment++;

                    Task.Delay((int)(Globals.ENTITY_SPEED * speed)).Wait();
                }
            });
        }
            public bool CallToPos()
        {
            // TODO: Call other guard
            return false;
        }
    }
}