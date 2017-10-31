using System;
using System.Collections.Generic;

namespace Basic13
{
    class Program
    {
        public static void print1To255(){
            for(int i = 1; i <= 255; i++){
                System.Console.WriteLine(i);
            }
        }

        public static void printOddUpTo255(){
            for(int i = 1; i <= 255; i++){
                if(i % 2 != 0){
                    System.Console.WriteLine(i);
                }
            }
        }

        public static void sumFrom1To255(){
            int totalSum = 0;
            for(int i = 1; i <= 255; i++){
                totalSum += i;
                Console.WriteLine("New number: {0} Sum: {1}", i, totalSum);
            }
        }

        public static void iteratingTheArray(int[] arr){
            for(int i = 0; i < arr.Length; i++){
                Console.Write(arr[i] + " - ");
            }
        }

        public static int findTheMax(int[] arr){
            int max = arr[0];
            for(int i = 0; i < arr.Length; i++){
                if(arr[i] > max){
                    max = arr[i];
                }
            }
            return max;
        }

        public static void printAverage(int[] arr){
            int sumTotal = 0;
            for(int i = 0; i < arr.Length; i++){
                sumTotal += arr[i];
            }

            System.Console.WriteLine("The average is: " + sumTotal/arr.Length);
        }

        public static int[] arrayOfOddsUpTo255(){
            List<int> theOdds = new List<int>();
            for(int i = 1; i <= 255; i++){
                if(i % 2 != 0){
                    theOdds.Add(i);
                }
            }
            return theOdds.ToArray();
        }

        public static void greaterThan(int theNumber, int[] arr){
            for(int i = 0; i < arr.Length; i++){
                if(arr[i] > theNumber){
                    System.Console.WriteLine("{0} is greather than {1}", arr[i], theNumber);
                }
            }
        }

        public static int[] squareValues(int[] arr){
            for(int i = 0; i < arr.Length; i++){
                arr[i] = arr[i] * arr[i];
            }
            return arr;
        }

        public static int[] noNegative(int[] arr){
            for(int i = 0; i < arr.Length; i++){
                if(arr[i] < 0){
                    arr[i] = 0;
                }
            }
            return arr;
        }

        public static void printMinMaxAve(int[] arr){
            int min = arr[0];
            int max = arr[0];
            int sum = 0; 
            for(int i = 0; i < arr.Length; i++){
                sum += arr[i];
                if(arr[i] < min){
                    min = arr[i];
                }

                if(arr[i] > max){
                    max = arr[i];
                }
            }
            System.Console.WriteLine("The max is " + max);
            System.Console.WriteLine("The min is " + min);
            Console.WriteLine("The average is " + sum/arr.Length);
        }

        public static int[] shiftVals(int[] arr){
            int origin = arr[0];
            for(int i = 0; i < arr.Length-1; i++){
                arr[i] = arr[i + 1];
            }
            arr[arr.Length-1] = origin;
            return arr;
        }

        


        static void Main(string[] args)
        {
            int[] numArray = {1,3,5,22,7,9,-4,13};

            print1To255();
            System.Console.WriteLine("---Now only the odds---");
            printOddUpTo255();
            System.Console.WriteLine("---Now the sum of all---");
            sumFrom1To255();
            System.Console.WriteLine("---Printing all the values of an array---");
            iteratingTheArray(numArray);
            System.Console.WriteLine("------");
            System.Console.WriteLine("The max value is: {0}",findTheMax(numArray));
            System.Console.WriteLine("--Finding the average----");
            printAverage(numArray);
            System.Console.WriteLine("Printing the odd numbers in List");
            // Writing each value of an integer array
            Console.WriteLine(string.Join(",", arrayOfOddsUpTo255()));
            Console.WriteLine("---Printing all the numbers greater than");
            greaterThan(6, numArray);
            System.Console.WriteLine("Squaring the values");
            
            Console.WriteLine(string.Join(", ", squareValues(numArray)));

            int[] numArray2 = {1,3,5,22,7,9,-4,13};
            Console.WriteLine("Substituting negative by zeros");
            Console.WriteLine(string.Join(", ", noNegative(numArray2)));
            int[] numArray3 = {1,3,5,22,7,9,-4,13};
            printMinMaxAve(numArray3);
            System.Console.WriteLine("Shifting the vals");
            Console.WriteLine(string.Join(", ", shiftVals(numArray3)));
        }
    }
}
