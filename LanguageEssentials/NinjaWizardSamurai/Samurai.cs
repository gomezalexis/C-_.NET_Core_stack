using System;
namespace NinjaWizardSamurai{

    public class Samurai : Human{

        public new int health = 200;
        
        public Samurai(string name){
            this.name = name;
        }

        public void deathBlow(Human human){
            System.Console.WriteLine("Samurai {0} is attacking {1}", this.name,human.name);
            if(human.health < 50){
                human.health = 0;
            } else{
                attack(human);
            }
            System.Console.WriteLine("Now {0} health is {1}", human.name, human.health);
        }

        public void meditate(){
            System.Console.WriteLine("Samurai health is {0} but now is going to meditate", this.health);
            if(this.health < 200){
                this.health = 200;
            } else{
                Console.Write("The samurai health was already 200 or over");
            }
        }
    }
}