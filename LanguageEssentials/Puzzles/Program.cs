using System;

namespace Puzzles
{
    class Program
    {   
        public static int[] randomArray(){
            int[] arr = new int[10];
            Random rand = new Random();
            for(int i = 0; i < arr.Length; i++){
                arr[i] = rand.Next(5, 25);
            }

            foreach(var item in arr){
                Console.Write(item.ToString() + ", ");
            }

            int min = arr[0];
            int max = arr[0];
            int sum = 0;
            for(int i = 0; i < arr.Length; i++){
                sum += arr[i];
                if(arr[i] > max){
                    max = arr[i];
                }

                if(arr[i] < min){
                    min = arr[i];
                }
                
            }
            System.Console.WriteLine("The min: {0}", min);
            System.Console.WriteLine("The max: {0}", max);
            System.Console.WriteLine("The sum: {0}", sum);
            return arr;
        }

        public static string tossCoin(){
            Random rand = new Random();
            string[] coinArray = {"tails","heads"};
            int randomNum = rand.Next();
            System.Console.WriteLine("The random number is " + randomNum);
            string result = coinArray[randomNum];
            System.Console.WriteLine("The result of the toss: {0} ", result);
            return result;
        }

        public static double tossMultipleCoins(int times){
            int heads = 0;
            for(int i = 1; i <= times; i++){
                string thisTime = tossCoin();
                if(thisTime == "heads"){
                    heads += 1;
                }
            }
            double ratio = heads/times;
            System.Console.WriteLine("The ratio of heads is: {0}", ratio);
            return ratio;
        }



        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            System.Console.WriteLine(randomArray());
            tossCoin();
            tossMultipleCoins(7);
        }
    }
}
