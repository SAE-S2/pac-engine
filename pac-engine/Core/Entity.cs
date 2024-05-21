using pac_engine.Core;
using pac_engine.Utils;

public class Entity
{
    private float health;
    public float Health
    {
        get { return health; }
        set { health = (value <= maxHealth) ? value : maxHealth; }
    }

    public float maxHealth;
    public float speed;
    public float damage;
    public bool imortal = false;
    public int angle = 0;
    public Vector2 pos = new Vector2();
    public Game? actualGame;
    public int indice;
    public event EventHandler<PositionChangedEventArgs>? PositionChanged;

    protected void eventPosChanged(Vector2 oldPos, Vector2 newPos, int indice)
    {
        PositionChanged?.Invoke(this, new PositionChangedEventArgs { OldPos = oldPos, NewPos = newPos, indice = indice });
    }

    public bool TakeDamage(float damage)
    {
        if (imortal)
            return false;

        Health -= damage;

        if (Health <= 0.1f) 
        {
            Kill();
        }
        return true;
    }

    public bool Kill()
        {
            if (imortal)
                return false;

            actualGame.EnemyDie(this);
            return true;
        }


    public void Heal(float health)
    {
        Health += health;
    }

    public virtual async Task Movement(Map level)
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
                        if (actualGame.player.damage > 0.1f)
                        {
                            TakeDamage(actualGame.player.damage);
                        }
                        else
                        {
                            actualGame.player.TakeDamage(damage);
                        }
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

    public void AngleChange(int angle)
    {
        if (angle < 0 || angle > 4)
        {
            return;
        }
        this.angle = angle;
    }

    public void SetActualGame(Game game)
    {
        actualGame = game;
    }

    public void ResetActualGame()
    {
        actualGame = null;
    }
}
