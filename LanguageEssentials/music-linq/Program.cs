using System;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Collections to work with
            List<Artist> Artists = JsonToFile<Artist>.ReadJson();
            List<Group> Groups = JsonToFile<Group>.ReadJson();

            
            //There is only one artist in this collection from Mount Vernon, what is their name and age?
            var fromMountVernon = from artist in Artists where 
                artist.Hometown == "Mount Vernon" select new {artist.RealName, artist.Age};

            foreach(var artist in fromMountVernon){
                System.Console.WriteLine($"From Mount Vernon: {artist.RealName} with the age of {artist.Age}");
            }

            
            //Who is the youngest artist in our collection of artists?
            var orderYoungest = (from artist in Artists orderby artist.Age
                ascending select new {artist.RealName, artist.Age}).First();
            
            System.Console.WriteLine("Youngest artist in collection is {0} with {1}",orderYoungest.RealName, orderYoungest.Age);
            
            //Display all artists with 'William' somewhere in their real name
            var william = from artist in Artists
                where artist.RealName.Contains("William")
                select new {artist.RealName, artist.Hometown};
            
            foreach(var will in william){
                Console.WriteLine($"This artist name {will.RealName} from {will.Hometown}");
            }

            //Display the 3 oldest artist from Atlanta
            var oldestAtlantaArtists = 
                (from artist in Artists
                orderby artist.Age descending
                where artist.Hometown == "Atlanta"
                select new {artist.RealName, artist.Hometown, artist.Age}).Take(3); 
            
            foreach(var artist in oldestAtlantaArtists){
                Console.WriteLine("Artist: {0}, Hometown: {1} Age: {2}", artist.RealName, artist.Hometown, artist.Age);
            }

            //(Optional) Display the Group Name of all groups that have members that are not from New York City
            var notFromNewYork =
                (from band in Groups
                join artist in Artists on band.Id equals artist.GroupId
                where artist.Hometown != "New York City"
                select new {band.GroupName, artist.RealName, artist.Hometown});

            System.Console.WriteLine("THe ones not from NYC");
            foreach(var k in notFromNewYork){
                Console.WriteLine("Group: {0}, Real Name: {1}, Hometown: {2}", k.GroupName, k.RealName, k.Hometown);
            }

            //(Optional) Display the artist names of all members of the group 'Wu-Tang Clan'
            var wuTangClanMembers =
                from band in Groups
                where band.GroupName == "Wu-Tang Clan"
                join artist in Artists on band.Id equals artist.GroupId
                select new {artist.RealName, band.GroupName};

            foreach(var member in wuTangClanMembers){
                Console.WriteLine("Artist: {0}, Group: {1}", member.RealName, member.GroupName);
            }
        }
    }
}
