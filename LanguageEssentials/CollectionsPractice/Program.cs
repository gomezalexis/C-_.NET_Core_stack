using System;
using System.Collections.Generic;

namespace CollectionsPractice
{
    class Program
    {
        static void Main(string[] args){
            int[] first = {0,1,2,3,4,5,6,7,8,9};
            string[] second= {"Tim","Martin", "Nikki", "Sara"};
            bool[] third = {true, false, true, false, true, false, true, false, true, false};

            System.Console.WriteLine(first.Length);
            System.Console.WriteLine(second.Length);
            System.Console.WriteLine(third.Length);

            //the list part
            List<string> iceCreamFlavors = new List<string>();
            iceCreamFlavors.Add("Chocolate Mint");
            iceCreamFlavors.Add("Rocky Road");
            iceCreamFlavors.Add("Strawberry");
            iceCreamFlavors.Add("Vanilla Bean");
            iceCreamFlavors.Add("Cherry Garcia");

            System.Console.WriteLine("The length of the list is {0}", iceCreamFlavors.Count);
            Console.WriteLine("The third flavor of the list is " + iceCreamFlavors[2]);

            iceCreamFlavors.RemoveAt(2);
            Console.WriteLine("The length of the list is {0}", iceCreamFlavors.Count);
            Console.WriteLine("The third flavor of the list is " + iceCreamFlavors[2]);

            //THe dictionary part of the assignment
            Dictionary<string,string> userInfo = new Dictionary<string, string>();

            Random rand = new Random();
            foreach(string name in second){
                userInfo.Add(name,iceCreamFlavors[rand.Next(iceCreamFlavors.Count)]);
            }

            foreach(var element in userInfo){
                System.Console.WriteLine(element.Key + " - " + element.Value);
            }
        }
        
    }
}
