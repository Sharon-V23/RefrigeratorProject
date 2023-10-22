using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefrigeratorProject
{
    public class Item
    {
        public static int uniqueId = 1;// Id משתנה סטטי שיחלק מזהה יחודי בבנאי גישה תתבצע ע"י המשתנה
        public int Id { get; }
        public string ProductName { get; set; }
        public int ShelfNumber { get; set; }
        public string ItemType { get; set; }
        public string KosherType { get; set; } 
        public DateTime ExpiryDate { get; set; }
        public double ProductVolume { get; set; } //(בסנטימטרים (מותאם למדף
      
        public Item(string productName, string itemType, string kosherType, DateTime expiryDate, double productVolume)
        {
            Id = uniqueId++;
            ProductName = productName;
            ItemType = itemType;
            KosherType = kosherType;
            ExpiryDate = expiryDate;
            ProductVolume = productVolume;
            ShelfNumber = ShelfNumber;

        }
        public override string ToString()
        {
            var result = "Item id: " + Id + " name: " + ProductName + " type: " + ItemType + " KosherType: " + KosherType.ToString() + " expery date: " + ExpiryDate.ToString() + " place taken by item: " + ProductVolume + " ";
            return result;
        }

    }
}
