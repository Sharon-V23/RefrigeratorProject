using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefrigeratorProject
{
    public class ConsoleFunctions
    {
        public Refrigerator currentRefrigerator { get; set; }
        public List<Refrigerator> refrigerators { get; set; }


        public ConsoleFunctions(Refrigerator refrigerator, List<Refrigerator> refrigerators)
        {
            this.currentRefrigerator = refrigerator;
            this.refrigerators = refrigerators;
        }
        //string productName, string itemType, string kosherType, DateTime expiryDate, double ProductVolume
        private Item InputItem()
        {
            var name = EnterName();
            var ItemType = EnterItemType();
            var KosherType = EnterKosherType();
            var ExpiryDate = EnterExpiryDate();
            var ProductVolume = EnterProductVolume();

            var Item = new Item(name, ItemType, KosherType, ExpiryDate, ProductVolume);

            return Item;

        }
        private string EnterName()
        {
            Console.WriteLine("enetr the name of the item");
            var name = Console.ReadLine();
            if(name == null) { EnterName(); }

            return name;
        }


        private string EnterItemType()
        {
            Console.WriteLine("enetr the type of item");
            var ItemType = Console.ReadLine();
            if (ItemType != "Food" || ItemType != "Drink") { 
                Console.WriteLine("you need enter Food or Drink");
                EnterItemType();
            }
            return ItemType;
        }
        //private Kind EnterKind()
        //{
        //    var input = 0;
        //    do
        //    {
        //        Console.WriteLine("enter the kind of the item. 1 for food 2 for drink");
        //        input = Convert.ToInt32(Console.ReadLine());
        //    } while (input != 1 && input != 2);

        //    var kind = (Kind)input;
        //    return kind;
        //}
        private string EnterKosherType()
        {
            Console.WriteLine("enetr the type of Koser:Dairy, Meat or Parve");
            var KosherType = Console.ReadLine();
            if(KosherType != "Dairy" || KosherType != "Meat" || KosherType != "Parve")
            {
                Console.WriteLine("you need enter:Dairy, Meat or Parve");
                EnterKosherType();
            }
            return KosherType;
        }
        //private Cosher EnterCosher()
        //{
        //    var input = 0;
        //    do
        //    {
        //        Console.WriteLine("enter the Cosher of the Item 1 for milky 2 for meaty 3 for pareve");

        //        input = Convert.ToInt32(Console.ReadLine());

        //    } while (input != 1 && input != 2 && input != 3);

        //    var cosher = (Cosher)input;
        //    return cosher;
        //}
        private DateTime EnterExpiryDate()
        {
            var input = 0;
            Console.WriteLine("enter the left days until the Expiry Date");
            try { 
            input = Convert.ToInt32(Console.ReadLine());}
            catch { Console.WriteLine("you need enter int"); }
            var date = DateTime.Now.AddDays(input);

            return date;

        }
        private double EnterProductVolume()
        {
            Console.WriteLine("enter the place taken by the item");
            var ProductVolume = Convert.ToDouble(Console.ReadLine());
            return ProductVolume;
        }
        private string EnterItemId()
        {
            Console.WriteLine("enter ID of the Item");
            var itemId = Console.ReadLine();
            if (!string.IsNullOrEmpty(itemId)) { EnterItemId(); }
            return itemId;
        }


        public void PrintRefrigerator()
        {
            Console.WriteLine(currentRefrigerator);
        }

        public void PrintLeftPlaceInRefrigerator()
        {
            var space = currentRefrigerator.GetAvailableSpaceInRefrigerator();
            Console.WriteLine("Left place in refrigerator : " + space);
        }
   
        public void AddItemFromUser()
        {
            var newItem = InputItem();
            currentRefrigerator.InsertAnItem(newItem);
        }
        //בדיקות תקינות למוצר
        //מה קורה אם לא קיים
        public void PutOutItemWithId()
        {
            var id = EnterItemId();

            bool t = int.TryParse(id, out int Id);
           if (t == true ) { 
            var takenItem = currentRefrigerator.RemoveItemFromRefrigerator(Id);
            
            if (takenItem == null)
            {
                Console.WriteLine("There is no item with this ID");
            }
            else
            {
                Console.WriteLine("The item who taken out is: " + takenItem);
            }
           }
           else { Console.WriteLine("There is no item with this ID");}
        }
        // אם אין מוצרים פגי תוקף
        public void CleanRefrigerator()
        {
            var removedItems = currentRefrigerator.CleanningRefrigerator();
            if(removedItems.Count!=0 ) { 
            Console.WriteLine(" the removed items are: ");
            PrintList(removedItems);}
            else { Console.WriteLine("you dont have to clean the Refrigerator");}
        }
        //אם אין מוצרים שאני רוצה
        public void WhatIWantToEat()
        {
            Console.WriteLine("choose kind of item and kosher of item");
            var ItemType = EnterItemType();
            var KosherType = EnterKosherType();
            var wantedItems = currentRefrigerator.WhatWouldYouLikeToEat(KosherType, ItemType);
            if (wantedItems.Count!=0) { 
            Console.WriteLine(" the items with the cosher: " + KosherType + " and kind: " + ItemType); ;
            PrintList(wantedItems);}
            else { Console.WriteLine("there is no offer for you"); }
        }

        public void ItemsOrderByExpiryDate()
        {
            var itemsByExpiryDate = currentRefrigerator.SortByExpiryDate();
            if (itemsByExpiryDate.Count!=0) { 
            Console.WriteLine(" items sorted by their expiry date: ");
            PrintList(itemsByExpiryDate);}
            else { Console.WriteLine("Not enough items"); }
        }

        public void ShelvesOedersByLeftPlace()
        {
            var shelvesByLeftPlace = currentRefrigerator.SortByAvailability();
            Console.WriteLine(" shelves sorted by their left place");
            foreach (var shelf in shelvesByLeftPlace)
            {
                Console.WriteLine(shelf + "\n");
            }
        }

        public void RefrigeratorsOrdersByLeftPlace()
        {
            var refrigeratorsByLeftPlace = currentRefrigerator.SortRefrigeratorByAvailability(refrigerators);
            if (refrigeratorsByLeftPlace.Count!=0) { 
            Console.WriteLine(" Refrigerators sorted by their left place");
            foreach (var refrigerator in refrigeratorsByLeftPlace)
            {
                Console.WriteLine(refrigerator + "\n");
            }
            }
            else { Console.WriteLine("there is no refrigerators to sort");}
        }


        public void GetReadyForShopping()
        {
            currentRefrigerator.GettingReadyforShopping();
        }

        public void PrintList(List<Item> items)
        {
            foreach (var item in items)
            {
                Console.WriteLine(item + " ");
            }
        }

    }
}
