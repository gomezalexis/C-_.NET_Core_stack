using System;
using System.Collections.Generic;


namespace BoxingUnboxing
{
    class Program
    {
        static void Main(string[] args){
            int sumOfNumbers = 0;
            List<object> whatevers = new List<object>();

            whatevers.Add(7);
            whatevers.Add(28);
            whatevers.Add(-1);
            whatevers.Add(true);
            whatevers.Add("chair");

            foreach(object whatever in whatevers){
                if(whatever is int){
                    System.Console.WriteLine(whatever);
                    sumOfNumbers += Convert.ToInt32(whatever);
                }

                if(whatever is string){
                    System.Console.WriteLine(whatever);
                }

                if(whatever is bool){
                    System.Console.WriteLine(whatever);
                }
            }

            System.Console.WriteLine("The sum of numbers is: {0}", sumOfNumbers);



        }

        
    }
}
