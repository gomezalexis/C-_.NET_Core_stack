using System;
namespace NinjaWizardSamurai{
    public class Human{
        public string name;
        public int strength = 3;
        public int intelligence = 3;
        public int dexterity = 3;
        private int _health = 100;

        public Human(string name= "John Doe"){
            this.name = name;
        }

        public Human(string name, int health, int dexterity, int strength, int intelligence){
            this.name = name;
            this._health = health;
            this.dexterity = dexterity;
            this.strength = strength;
            this.intelligence = intelligence;
        }

        public int health {
            get{return _health;} 
            set{_health = value;}
        }

        public void attack(Human human){
            human._health -= (this.strength * 3);
            System.Console.WriteLine("The health of {0} now is {1}", human.name, human._health);
        }
        
    }
}