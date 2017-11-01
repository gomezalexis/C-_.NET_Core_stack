using System;
namespace NinjaWizardSamurai{

    public class Wizard : Human{
        public new int health = 50;
        public new int intelligence = 25;

        public Wizard(string name) : base(){
            this.name = name;
        }

        public void heal(){
            System.Console.WriteLine($"{this.name} is healing.");
            this.health += (10 * this.intelligence); 
        }

        public void fireball(Human human){  
            Random rand = new Random();
            System.Console.WriteLine("Wizard attacking {0}", human.name);
            human.health -= rand.Next(20,50);
            System.Console.WriteLine($"{human.name} is now {human.health}.");
        }
    }
}