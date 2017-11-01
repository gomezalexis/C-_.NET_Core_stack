using System;

namespace NinjaWizardSamurai
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Hello World!");
            Human alexis = new Human("Papa Tazo");
            System.Console.WriteLine(alexis.name);
            System.Console.WriteLine("THe health is: {0}", alexis.health);
            alexis.health = 98;
            System.Console.WriteLine("THe health is: {0}", alexis.health);

            Human mahenn = new Human("Mahenn", 95, 4, 2, 2);
            Console.WriteLine("{0} health is {1}", mahenn.name, mahenn.health);
            System.Console.WriteLine("{0} dexterity is: {1}", mahenn.name, mahenn.dexterity);
            mahenn.attack(alexis);
            alexis.attack(mahenn);

            Wizard wizard1 = new Wizard(name: "Gandalf");
            System.Console.WriteLine($"The wizard name is {wizard1.name}");
            Console.WriteLine($"{wizard1.name} health is {wizard1.health}");
            wizard1.heal();
            Console.WriteLine($"{wizard1.name} health is {wizard1.health}");
            wizard1.fireball(mahenn);

            Ninja ninja1 = new Ninja(name: "David");
            System.Console.WriteLine($"{ninja1.name} health is {ninja1.health}");
            ninja1.attack(wizard1);
            ninja1.getAway();

            Samurai samurai1 = new Samurai(name:"Catalina");
            samurai1.deathBlow(mahenn);
            samurai1.deathBlow(ninja1);
            samurai1.meditate();

            
        }
    }
}
