using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance
        {
            get { if (instance == null) instance = new TableDAO(); return TableDAO.instance; }
            private set { TableDAO.instance = value; }
        }

        public List<Table> LoadListTable(){
            List<Table> listTable = new List<Table>();

            DataTable data = DataProvider.Instance.ExecuteQuery("select * from Foodtable");

            foreach(DataRow item in data.Rows){

                Table table = new Table(item);
                listTable.Add(table);

            }
            
            return listTable;
        }

        public void SwitchTable(int id1, int id2)
        {
            DataProvider.Instance.ExecuteQuery("exec SwitchTable @idTable1 , @idTable2", new object[] { id1, id2 });
        }

        public DataTable loadListTable()
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from FoodTable");
            return data;
        }

        public bool insertTable(string name, string tableType)
        {
            string query = string.Format("insert foodTable(name, tabletype,status) values (N'{0}','{1}',N'Trống')", name,tableType);
            int i = DataProvider.Instance.ExecuteNonQuery(query);
            return i > 0;
        }

        public bool updateTable(int id, string name, string status,string tabletype)
        {
            string query = string.Format("update foodTable set name = N'{0}' , status = N'{1}', Tabletype = N'{3}' where id = {2}", name, status, id,tabletype);
            int i = DataProvider.Instance.ExecuteNonQuery(query);
            return i > 0;
        }

        public bool deleteTable(int id)
        {
            string query = string.Format("delete foodTable where id = {0}", id);
            int i = DataProvider.Instance.ExecuteNonQuery(query);
            return i > 0;
        }

        public bool TableIsExisted(string name)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery(string.Format("select * from FoodTable where name = N'{0}'", name));
            if (data.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public DataTable LoadListTableByType(string type)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from foodtable where tabletype = '" + type+"'");
            return data;
        }
    }
}
