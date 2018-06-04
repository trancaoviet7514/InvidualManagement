using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance
        {
            get { if (instance == null)instance = new MenuDAO(); return MenuDAO.instance; }
            private set { MenuDAO.instance = value; }
        }

        public List<Menu> getListMenuByTable(int id)
        {
            List<Menu> listMenu = new List<Menu>();

            string query = "select f.Name, bi.count, f.price, bi.count*f.price as N'totalPrice'" + 
                           "from Bill b , BillInfo bi, Food f " +
                           "where bi.idBill = b.id and bi.idFood = f.id and b.status = 0 and b.idTable = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);
                listMenu.Add(menu);
            }

            return listMenu;
        }

    }
}
