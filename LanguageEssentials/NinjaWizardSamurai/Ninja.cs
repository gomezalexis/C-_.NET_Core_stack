using System;
namespace NinjaWizardSamurai{
    public class Ninja : Human{
        public new int dexterity = 175;
        public Ninja(string name) : base(){
            this.name = name;
        }

        public new void attack(Human human){
            Console.WriteLine($"{human.name} has a health of {human.health} and is being attacked");
            base.attack(human);
            this.health += 10;
            Console.WriteLine($"{this.name} just attacked {human.name}.");
            Console.WriteLine($"Now {human.name} health is {human.health}");
        }

        public void getAway(){
            System.Console.WriteLine("Ninja {0} is getting away", this.name);
            this.health -= 15;
            System.Console.WriteLine("Nos his health is {0}", this.health);
        }
    }

}