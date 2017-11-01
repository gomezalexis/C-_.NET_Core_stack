using System.Collections.Generic;
using System;

namespace DeckOfCards{

    public class Player{
        private string _name;
        private List<Card> _hand = new List<Card>();
        public Player(string name){
            this._name = name;
            this._hand = hand;
        }

        public string name{
            get{return _name;}
            set{_name = value;}
        }

        public List<Card> hand{
            get{return _hand;}
            set{_hand = value;}
        }

        public void printHand(){
            Console.WriteLine("{0} has in hand", this._name);
            foreach(Card card in hand){
                Console.WriteLine(card);
            }
        }
    }
}