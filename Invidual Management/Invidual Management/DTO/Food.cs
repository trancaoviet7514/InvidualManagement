using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Food
    {

        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

   

        private string category;

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        private float price;

        public float Price
        {
            get { return price; }
            set { price = value; }
        }
        
        public Food(string name, int id, string category, float price)
        {
            this.name = name;
            this.iD = id;
            this.Category = category;
            this.price = price;
        }

        public Food(DataRow row)
        {
            this.name = row["FoodName"].ToString();
            this.iD = (int)row["FoodID"];
            this.Category = row["CategoryName"].ToString();
            this.price = (float)Convert.ToDouble(row["Price"].ToString());
        }

    }
}
