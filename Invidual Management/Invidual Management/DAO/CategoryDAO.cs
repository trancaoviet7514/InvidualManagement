using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class CategoryDAO
    {

        private static CategoryDAO instance;

        public static CategoryDAO Instance
        {
            get { if (instance == null)instance = new CategoryDAO(); return CategoryDAO.instance; }
            private set { CategoryDAO.instance = value; }
        }

        public List<Category> getListCategory()
        {
            List<Category> listCategory = new List<Category>();

            string query = "select id as CategoryID, name as CategoryName from FoodCategory";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {

                Category category = new Category(item);

                listCategory.Add(category);

            }

            return listCategory;
        }

        public Category getCategoryByid(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from FoodCategory where id = '" + id + "'");
            foreach (DataRow item in data.Rows)
            {
                return new Category(item);
            }
            return null;
        }

        public bool insertCategory(string name)
        {
            string query = string.Format("insert foodCategory (name) values (N'{0}')", name);
            int i = DataProvider.Instance.ExecuteNonQuery(query);
            return i > 0;
        }

        public bool updateCategory(int id, string name)
        {
            string query = string.Format("update foodCategory set name = N'{0}' where id = {1}", name, id);
            int i = DataProvider.Instance.ExecuteNonQuery(query);
            return i > 0;
        }

        public bool deleteCategory(int id)
        {
            string query = string.Format("delete foodCategory where id = {0}", id);
            int i = DataProvider.Instance.ExecuteNonQuery(query);
            return i > 0;
        }
    }
}
