using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefrigeratorProject
{
    public class Shelf
    {
        public static int uniqueId = 1;// Id משתנה סטטי שיחלק מזהה יחודי בבנאי גישה תתבצע ע"י המשתנה
        public int Id { get; }
      //  private int floorNumber;


        public int FloorNumber { get; set ; }

        public const double Capacity = 90;// בסנטימטרים
        public double Availability { get; set; }
        public List<Item> Items { get; set; }
        public Shelf(int floorNumber)
        {
            Id = uniqueId++;
            FloorNumber = floorNumber;
            Availability = Capacity;
            Items = new List<Item>();

        }

        public override string ToString()
        {
            var result = "";
            result += " shelf: " + Id + ". the level of the shelf: " + FloorNumber.ToString() + ". place in shelf: " + Availability + ". the items in the shelf : \n";
            if (Items.Count() == 0)
            {
                result += "there is no items in this shelf.";
            }
            else
            {
                foreach (var item in Items)
                {
                    result += (item.ToString()) + ". \n";
                }
            }

            return result;
        }
    }
}
