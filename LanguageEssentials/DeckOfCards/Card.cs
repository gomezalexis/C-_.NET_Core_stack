namespace DeckOfCards{
    public class Card{
        public string stringVal;
        public string suit;
        public int val;

        public Card(string stringVal, string suit, int val){
            this.stringVal = stringVal;
            this.suit = suit;
            this.val = val;
        }

        public void writtenCard(){
            System.Console.WriteLine(" The {0} of {1}", this.stringVal, this.suit);
        }

        public override string ToString(){
            return "The " + this.stringVal +" of " + this.suit;
        }

    }
}