using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefrigeratorProject
{
    public class Refrigerator
    {
        public static int uniqueId = 1;// Id משתנה סטטי שיחלק מזהה יחודי בבנאי גישה תתבצע ע"י המשתנה

        public int Id { get; }
        public string Model { get; set; }
        public string Color { get; set; }
        private int numberOfShelves;

        public int NumberOfShelves
        {
            get { return numberOfShelves; }
            set
            {
                if (value > 0 && value < 10)
                {
                    numberOfShelves = value;
                }
                else
                {
                    numberOfShelves = 5;
                }
            }
        }
        public List<Shelf> Shelves { get; set; }


        public Refrigerator(string model, string color, int numberOfShelves)
        {
            Id = uniqueId++;
            Model = model;
            Color = color;
            NumberOfShelves = numberOfShelves;
            Shelves = new List<Shelf>();
            for(int i = 0; i < numberOfShelves; i++)
            {
                Shelves.Add(new Shelf(i));

            }


        }


        public override string ToString()
        {
            var result = "";
            result += " Refrigerator : " + Id + ". model: " + Model.ToString() + ". number of shelves : " + NumberOfShelves.ToString() + ". the selves: \n \n";

                foreach (var shelf in Shelves)
                {
                    result += shelf.ToString() + ". \n \n";
                }

            return result;

        }
        public double GetAvailableSpaceInRefrigerator()
        {
            double SumCapacity = 0;

            foreach (var shelf in Shelves)
            {
                SumCapacity += shelf.Availability;
            }

            return SumCapacity;
        }

        public void InsertAnItem(Item item)
        {
            if (item != null)
            {
                foreach (var shelf in Shelves)
                {
                    if (shelf.Availability >= item.ProductVolume)
                    {
                        shelf.Items.Add(item);
                        item.ShelfNumber = shelf.FloorNumber;
                        shelf.Availability = shelf.Availability + item.ProductVolume;
                        Console.WriteLine("Your item has been successfully added");
                    }
                    Console.WriteLine("There is not enough space in the fridge");

                }

            }
           Console.WriteLine("cant get null value");
        }


        public Item RemoveItemFromRefrigerator(int itemID)
        {
            foreach (var shelf in Shelves)
            {
                Item RemoveItem = shelf.Items.Find(i => i.Id == itemID);
                if (RemoveItem != null)
                {
                    shelf.Items.Remove(RemoveItem);
                    shelf.Availability += RemoveItem.ProductVolume;

                    return RemoveItem;
                }
            }

            return null;
        }

        public List <Item>  CleanningRefrigerator()
        {
          List<Item>removedItems= new List<Item>();
            Item removedItem;
            foreach (var shelf in Shelves)
            {
                foreach (var item in shelf.Items)
                {
                    if (item != null)
                    {
                        if (item.ExpiryDate < DateTime.Today)
                        {
                            removedItem = RemoveItemFromRefrigerator(item.Id);
                            removedItems.Add(removedItem);
                        }
                    }
                }
            }
            return removedItems;
        }


        public List<Item> WhatWouldYouLikeToEat(string kosher, string Type)
        {
            List<Item> OfferItems = new List<Item>();

            foreach (var shelf in Shelves)
            {
                foreach (var item in shelf.Items)
                {
                    if (item != null)
                    {
                        if (item.KosherType == kosher && item.ItemType == Type && item.ExpiryDate! < DateTime.Today)

                            OfferItems.Add(item);
                    }
                }
            }
            return OfferItems;
        }

        public List<Item> SortByExpiryDate()
        {
            List<Item> SortedList = new List<Item>();

            foreach (var shelf in Shelves)
            {
                SortedList.AddRange(shelf.Items);
            }
            SortedList.OrderBy(item => item.ExpiryDate)
            .ThenBy(item => item.ExpiryDate.GetType == null ? item.ExpiryDate : DateTime.MaxValue)//טיפול בתאריך תפוגה מסוג לסוף הרשימה null
            .ToList();
            return SortedList;
        }

        public List<Shelf> SortByAvailability()
        {
            List<Shelf> SortedList = new List<Shelf>();

            foreach (var shelf in Shelves)
            {
                SortedList= Shelves.OrderByDescending(shelf => shelf.Availability).ToList();//-כיוון שאתחלתי אותם. אין צורך לשאול על  null 
            }
            return SortedList;
        }

        public List<Refrigerator> SortRefrigeratorByAvailability(List<Refrigerator> refrigerators)
        {//שיטה קלה למיון!-שימוש בפונקציית ההשוואה
            if (refrigerators.Count!<2)
            refrigerators.Sort((fridge1, fridge2) =>
           fridge2.GetAvailableSpaceInRefrigerator().CompareTo(fridge1.GetAvailableSpaceInRefrigerator()));

            return refrigerators;
        }






        //אם אספיק-סדר בשכפול הקוד
        public string GettingReadyforShopping()
        {
            double Available = GetAvailableSpaceInRefrigerator();
            if (Available >= 20)
            {
                return "Let's go shopping";
            }
            else
            {
                CleanningRefrigerator();
                Available = GetAvailableSpaceInRefrigerator();
                if (Available >= 20)
                {
                    return "Let's go shopping";
                }
                Available = ThrowDairy(Available);
                if (Available >= 20)
                {
                    return "The dairy products were removed from the refrigerator for the alternatives";
                }
                Available = ThrowAwayMeat(Available);
                if (Available >= 20)
                {
                    return "The dairy and meat products were removed from the refrigerator for the alternatives";
                }
                Available = ThrowAwayParve(Available);
                if (Available >= 20)
                {
                    return "The dairy, meat and parve products were removed from the refrigerator for the alternatives";
                }

                return "it's not the time for shopping yet";

            }
        }

        private double ThrowDairy(double Available)
        {
            foreach (var shelf in Shelves)
            {
                foreach (var item in shelf.Items)
                {
                    if (item.ItemType == "Food" && item.KosherType == "Dairy" && (item.ExpiryDate - DateTime.Now).TotalDays < 3)
                    {
                        Available -= item.ProductVolume;
                        RemoveItemFromRefrigerator(item.Id);  
                    }                                      
                }
            }          
          return Available; 
        }

        private double ThrowAwayMeat(double Available)
        {
            foreach (var shelf in Shelves)
            {
                foreach (var item in shelf.Items)
                {
                    if (item.ItemType == "Food" && item.KosherType == "Meat" && (item.ExpiryDate - DateTime.Now).TotalDays < 7)
                    {
                        Available -= item.ProductVolume;
                        RemoveItemFromRefrigerator(item.Id);
                    }

                }
            }
            return Available;
        }


        private double ThrowAwayParve(double Available)
        {
            foreach (var shelf in Shelves)
            {
                foreach (var item in shelf.Items)
                {
                    if (item.ItemType == "Food" && item.KosherType == "Parve" && (item.ExpiryDate - DateTime.Now).TotalDays < 1)
                    {
                        Available -= item.ProductVolume;
                        RemoveItemFromRefrigerator(item.Id);
                    }
                }
            }
            return Available;
        }

    }
}
