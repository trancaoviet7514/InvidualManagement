using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Menu
    {
        public Menu(string foodName, int count, float price , float totalPrice = 0)
        {
            this.FoodName = foodName;
            this.Count = count;
            this.Price = price;
            this.TotalPrice1 = price * count;
        }

        public Menu(DataRow row)
        {
            this.FoodName = row["name"].ToString();
            this.Count = (int)row["count"];
            this.Price = (float)Convert.ToDouble((row["price"].ToString()));
            this.TotalPrice1 = (float)Convert.ToDouble((row["totalPrice"].ToString()));
        }

        private float TotalPrice;  

        public float TotalPrice1
        {
            get { return TotalPrice; }
            set { TotalPrice = value; }
        }

        private string foodName;

        public string FoodName
        {
            get { return foodName; }
            set { foodName = value; }
        }
        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        float price;

        public float Price
        {
            get { return price; }
            set { price = value; }
        }

    }
}
