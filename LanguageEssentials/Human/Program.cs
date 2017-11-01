using System;
using Human;
namespace Human
{
    class Program
    {
        static void Main(string[] args)
        {
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
        }
    }
}
