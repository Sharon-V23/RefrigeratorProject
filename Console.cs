using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefrigeratorProject
{
    public class ConsoleGame
    {

        public static void StartGame()
        {
            var refrigerators = ConsoleGame.CreateRefrigerators();
            var currentRefrigerator = refrigerators.ElementAt(0);

            var gameFunctions = new ConsoleFunctions(currentRefrigerator, refrigerators);



            Console.WriteLine("Console Application in C# for Refrigerators\r");
            Console.WriteLine("------------------------\n");



            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\t1 - Viewing the contents of a refrigerator.");
            Console.WriteLine("\t2 - Left Place in the refrigerator");
            Console.WriteLine("\t3 - Put an item in the refrigerator.");
            Console.WriteLine("\t4 - Remove an item from the refrigerator.");
            Console.WriteLine("\t5 - Clean the refrigerator.");
            Console.WriteLine("\t6 - What do I want to eat?.");
            Console.WriteLine("\t7 - Sorted products by their expiration date..");
            Console.WriteLine("\t8 - Sorted shelves according to the free space left on them.");
            Console.WriteLine("\t9 - Sorted refrigerators according to the free space left in them..");
            Console.WriteLine("\t10 - Prepare the refrigerator for shopping.");
            Console.WriteLine("\t100 - Close the system..");
            Console.Write("Your option? ");

            var selectedAction = Console.ReadLine();

         

                switch (selectedAction)
                {
                    case "1":

                        gameFunctions.PrintRefrigerator();
                        break;

                    case "2":
                        gameFunctions.PrintLeftPlaceInRefrigerator();
                        break;
                    case "3":
                        gameFunctions.AddItemFromUser();
                        break;
                    case "4":
                        gameFunctions.PutOutItemWithId();
                        break;
                    case "5":
                        gameFunctions.CleanRefrigerator();
                        break;
                    case "6":
                        gameFunctions.WhatIWantToEat();
                        break;
                    case "7":
                        gameFunctions.ItemsOrderByExpiryDate();
                        break;
                    case "8":
                        gameFunctions.ShelvesOedersByLeftPlace();
                        break;
                    case "9":
                        gameFunctions.RefrigeratorsOrdersByLeftPlace();
                        break;
                    case "10":
                        gameFunctions.GetReadyForShopping();
                        break;
                    case "100":
                        Console.WriteLine("You entered to finish the game.");
                        return;
                default:
                        Console.WriteLine("You did not enter any of the options above.");
                        break;
                }
                selectedAction = Console.ReadLine();
      


        }

        public static List<Refrigerator> CreateRefrigerators()
        {
            var refrigerators = new List<Refrigerator>();

            var refrigerator1 = new Refrigerator("SumsungA670", "white", 4);


            var refrigerator2 = new Refrigerator("BoshS23", "black", 6);

            var refrigerator3 = new Refrigerator("Electra", "red", 5);


            refrigerator1.InsertAnItem(new Item("chips", "Food", "Dairy", DateTime.Now.AddDays(-1), 5.0));
            refrigerator1.InsertAnItem(new Item("banana", "Food", "Dairy", DateTime.Now.AddDays(2), 5.0));
            refrigerator1.InsertAnItem(new Item("bread", "Food", "Meat", DateTime.Now.AddDays(3), 5.0));
            refrigerator1.InsertAnItem(new Item("chips", "Food", "Parve", DateTime.Now.AddDays(1), 5.0));
            refrigerator1.InsertAnItem(new Item("chips", "Food", "Parve", DateTime.Now.AddDays(100), 5.0));
            refrigerator1.InsertAnItem(new Item("chips", "Food", "Meat", DateTime.Now.AddDays(100), 5.0));



            refrigerators.Add(refrigerator1);
            refrigerators.Add(refrigerator2);
            refrigerators.Add(refrigerator3);


            return refrigerators;
        }

    }
}
