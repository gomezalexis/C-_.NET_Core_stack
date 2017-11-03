using System;

namespace DeckOfCards
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("The Deck of Cards");
            

            Deck theDeck = new Deck();
            System.Console.WriteLine("The first card is: {0}", theDeck.cards[0]);
            System.Console.WriteLine("The amount of cards is: {0}", theDeck.cards.Count);
            theDeck.shuffle();
            System.Console.WriteLine("The first card is: {0}", theDeck.cards[0]);
            theDeck.printAll();

            Player player1 = new Player("Mikah");
            theDeck.giveHand(player1);
            Console.WriteLine("Player 1 name is: {0}.",player1.name);
            Console.WriteLine("Player one has {0} cards.", player1.hand.Count);
            player1.printHand();
            System.Console.WriteLine("The Amount of the deck right now is {0}", theDeck.cards.Count);
            theDeck.printAll();
        }
    }
}
