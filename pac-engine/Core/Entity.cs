using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Entity
{

}

public class VulnerableEntity : Entity
{
    private double health;
    private double maxHealth;
    private double speed;
    private double damage;

    public void DamageDeal()
    {

    }
}

public class InvulnerableEntity : Entity
{
    private Money money;

    public InvulnerableEntity(Money money)
    {
        this.money = money;

    }
    public void Purchase()
    {

    }
}

public class Money
{
    private int amount;

    public Money(int amount)
    {
        this.amount = amount;
    }
}

public class BoltMoney : Money
{
    public BoltMoney(int amount) : base(amount)
    {
    }
}

public class CoinMoney : Money
{
    public CoinMoney(int amount) : base(amount)
    {
    }
}

public class Pacbot
{
    private double health;
    private double maxHealth;
    private double speed;
    private double absorption;
    private double fieldOfVision;
    private double regen;

    private BoltMoney bolts;
    private CoinMoney coins;

    private int shyLevel;
    private int luckyLevel;

    public ActiveSpell activeSpell;

    public Pacbot()
    {
        health = 3;
        maxHealth = 3;
        speed = 100;
        absorption = 0;
        fieldOfVision = 100;
        regen = 0;

        bolts = new BoltMoney(0);
        coins = new CoinMoney(0);

        shyLevel = 0;
        luckyLevel = 0;

        System.Console.WriteLine("Hi, im Pacbot!");
    }

    public void CastSpell()
    {
        if (activeSpell != null)
        {
            activeSpell.Cast(this);
        }
    }
}

public class ActiveSpell
{
    private DateTime spellCast;
    private int level;

    public ActiveSpell()
    {
        spellCast = DateTime.MinValue;
        level = 1;
        System.Console.WriteLine($"Choose spell is {GetType().Name}!");
    }

    public virtual void Cast(Pacbot player)
    {
        if ((DateTime.Now - spellCast).TotalSeconds > 240)
        {
            spellCast = DateTime.Now;
            System.Console.WriteLine("Spell Casted!");
        }
        else
        {
            System.Console.WriteLine("Spell is on cooldown!");
            System.Console.WriteLine($"You have to wait {Math.Max(Math.Round(240 - (DateTime.Now - spellCast).TotalSeconds, 0), 0)} seconds before recasting!");
        }
    }
}

public class ShieldSpell : ActiveSpell
{
    public override void Cast(Pacbot player)
    {
        base.Cast(player);
    }
}

public class FearSpell : ActiveSpell
{

}

public class InvisibleSpell : ActiveSpell
{

}

class TestClass
{
    public Pacbot player;
    static void Main(string[] args)
    {
        var tc = new TestClass();
        tc.player = new Pacbot();
        tc.player.activeSpell = new FearSpell();
        tc.player.CastSpell();
        tc.player.CastSpell();
        System.Threading.Thread.Sleep(2000);
        tc.player.CastSpell();
    }
}