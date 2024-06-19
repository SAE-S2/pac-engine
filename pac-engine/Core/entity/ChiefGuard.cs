using pac_engine.Utils;

namespace pac_engine.Core
{
    public class ChiefGuard : Entity
    {
        public ChiefGuard(int indice)
        {
            this.maxHealth = 1.0f;
            this.Health = 1.0f;
            this.speed = 0.75f;
            this.damage = 0.5f;
            this.indice = indice;
        }

        // Déplacement intelligent
        public override async Task Movement(Map level)
        {
            bool posChange = false;
            int nbDeplacment = 17;
            ShortestPath shortestPath = new ShortestPath(actualGame.map.map);
            List<int> chemin = new List<int>(); // Contient les angles du chemins
            await Task.Run(() =>
            {
                Random random = new Random();
                int varR = random.Next(1, 10);
                Task.Delay(2500 + varR * 300).Wait();
                bool stupidMove = false;

                while (Health > 0)
                {
                    if (nbDeplacment >= (7 + varR) || stupidMove == true)
                    {
                        if (actualGame.player.isInvisible || pos.Distance(actualGame.player.pos) > 10)
                        {
                            stupidMove = true;
                        }
                        else
                        {
                            stupidMove = false;
                            chemin = shortestPath.FindShortestPath(pos.x, pos.y, actualGame.player.pos.x, actualGame.player.pos.y);
                            nbDeplacment = 0; //Reset compteur
                        }
                    }

                    if (!stupidMove)
                    {
                        if (chemin == null || chemin.Count == 0)
                        {
                            AngleChange(angle);
                            nbDeplacment++;
                            Task.Delay((int)(Globals.ENTITY_SPEED / speed)).Wait();
                            continue;
                        }

                        if (chemin[0] != angle)
                        {
                            AngleChange(chemin[0]);
                        }
                        chemin.RemoveAt(0);
                    }
                    else
                    {
                        if (!posChange)
                        {
                            int max = 0;
                            int[] list = new int[4];
                            if (!level.GetWall((int)pos.x - 1, (int)pos.y))
                            {
                                list[max] = 0;
                            }
                            if (!level.GetWall((int)pos.x, (int)pos.y + 1))
                            {
                                list[max] = 3;
                            }
                            if (!level.GetWall((int)pos.x + 1, (int)pos.y))
                            {
                                list[max] = 2;
                            }
                            if (!level.GetWall((int)pos.x, (int)pos.y - 1))
                            {
                                list[max] = 1;
                            }

                            random = new Random(); // Regen d'un nouveau random
                            angle = list[random.Next(0, max)];
                        }
                    }

                    Vector2 oldPos = new Vector2(pos.x, pos.y);
                    posChange = false;
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
                    if (posChange)
                    {
                        eventPosChanged(oldPos, pos, indice);
                        if (actualGame.player.pos.x == pos.x && actualGame.player.pos.y == pos.y)
                            actualGame.player.TakeDamage(damage);
                    }

                    if (!stupidMove)
                        nbDeplacment++;

                    Task.Delay((int)(Globals.ENTITY_SPEED / speed)).Wait();
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