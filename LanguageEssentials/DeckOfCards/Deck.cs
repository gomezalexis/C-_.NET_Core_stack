using System.Collections.Generic;
using System;
namespace DeckOfCards{

    public class Deck{
        public List<Card> cards = new List<Card>();
        
        public Deck(){
            string[] stringVals = {"Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King"};
            string[] suits = {"Clubs", "Spades", "Hearts", "Diamonds"};

            // List<Card> this.cards = new List<Card>();


            foreach(string suit in suits){
                // int numCard = 1;
                foreach(string stringVal in stringVals){
                    int count = 1;
                    
                    Card card = new Card(stringVal, suit, count);
                    count += 1;
                    card.writtenCard(); 
                    this.cards.Add(card);
                }
            }
        
        }

        public void shuffle(){
            List<Card> shuffleDeck = new List<Card>();
            Random rand = new Random();
            for(int i = 0; i < 52; i++){
                int index = rand.Next(0,cards.Count - 1);
                shuffleDeck.Add(cards[index]);
                cards.RemoveAt(index);
            }
            this.cards = shuffleDeck;
        }

        public void printAll(){
            int count = 1;
            foreach(Card card in this.cards){
                Console.WriteLine(count + " - " + card);
                count += 1;
            }
        }

        public List<Card> giveHand(Player player){
            player.hand = this.cards.GetRange(0, 5);
            this.cards.RemoveRange(0,5);
            return player.hand;
        }

    }
}