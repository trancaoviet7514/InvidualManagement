using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    class FoodDAO
    {

        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get { if (instance == null)instance = new FoodDAO(); return FoodDAO.instance; }
            private set { FoodDAO.instance = value; }
        }

        public List<Food> getListFoodByCategoryID(int id)
        {

            List<Food> listFood = new List<Food>();

            string query = "select f.ID FoodID,f.Name as FoodName,fc.Name as CategoryName,Price from Food f join FoodCategory fc on f.IDCategory = fc.ID where idCategory = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {

                Food food = new Food(item);

                listFood.Add(food);

            }

            return listFood;

        }

        public List<Food> getListFood()
        {
            List<Food> listFood = new List<Food>();

            string query = "select f.ID as FoodID,f.Name as FoodName,fc.Name as CategoryName,Price from Food f join FoodCategory fc on f.IDCategory = fc.ID";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {

                Food food = new Food(item);

                listFood.Add(food);

            }

            return listFood;
        }

        public bool insertFood(string foodName, int idCategory, float price)
        {
            string query = string.Format("insert food (Name, idCategory, price) values (N' {0} ' , {1} , {2})", foodName, idCategory, price);
            int i = DataProvider.Instance.ExecuteNonQuery(query);
            return i > 0;
        }

        public bool updateFood(int id, string foodName, int idCategory, float price)
        {
            string query = string.Format("update food set Name = N'{0}', idCategory = {1}, price = {2} where id = {3}", foodName, idCategory, price, id);
            int i = DataProvider.Instance.ExecuteNonQuery(query);
            return i > 0;
        }

        public bool deleteFood(int id)
        {
            BillInfoDAO.Instance.deleteBillInfoByIdFood(id);

            string query = string.Format("delete food where id = {0}", id);
            int i = DataProvider.Instance.ExecuteNonQuery(query);
            return i > 0;
        }

        public List<Food> findListFoodByName(string foodName)
        {
            List<Food> listFood = new List<Food>();

            string query = string.Format("select f.ID as FoodID,f.Name as FoodName,fc.Name as CategoryName,Price from Food f join FoodCategory fc on f.IDCategory = fc.ID where f.Name like N'%{0}%'", foodName);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {

                Food food = new Food(item);

                listFood.Add(food);

            }

            return listFood;
        }

        public Food findFoodByName(string foodName)
        {

            string query = string.Format("select f.ID FoodID,f.Name as FoodName,fc.Name as CategoryName,Price from Food f join FoodCategory fc on f.IDCategory = fc.ID where f.Name = N'{0}'", foodName);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {

                return new Food(item);
            }

            return null;
        }

    }
}
