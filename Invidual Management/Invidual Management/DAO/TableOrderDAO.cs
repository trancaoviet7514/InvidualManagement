using QuanLyQuanCafe.DTO;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    class TableOrderDAO
    {
        private static TableOrderDAO instance;

        public static TableOrderDAO Instance
        {
            get { if (instance == null) instance = new TableOrderDAO(); return TableOrderDAO.instance; }
            private set { TableOrderDAO.instance = value; }
        }


        public List<TableOrder> getListTableOrder()
        {
            List<TableOrder> listTable = new List<TableOrder>();

            DataTable data = DataProvider.Instance.ExecuteQuery("select IDTable, TableName, SDTNguoiDat,  cast(TimeOrder as date) as 'DateOrder', "+
                                                                "cast(TimeOrder as time) as 'TimeOrder',TableOrderType from TableOrder where TimeOrder >= cast(getdate() as date)");

            foreach (DataRow item in data.Rows)
            {

                TableOrder table = new TableOrder(item);
               listTable.Add(table);

            }

            return listTable;
        }
        public List<TableOrder> getListTableOrderBySDT(string sdt)
        {
            List<TableOrder> listTable = new List<TableOrder>();

            DataTable data = DataProvider.Instance.ExecuteQuery("select IDTable, TableName, SDTNguoiDat, cast(TimeOrder as date) as 'DateOrder', cast(TimeOrder as time) as 'TimeOrder',TableOrderType"+
                                                                " from TableOrder where SDTNguoiDat like '%" + sdt+"%'");

            foreach (DataRow item in data.Rows)
            {

                TableOrder table = new TableOrder(item);
                listTable.Add(table);

            }

            return listTable;
        }

        public DataTable loadListTableOrder()
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from TableOrder");
            return data;
        }
    

        public bool insertTableOrder(string idTableOrder, string TableOrderName, string SDTNguoiDat, string DateTimeOrder, string tableType)
        {
            string query = string.Format("insert into TableOrder values ('{0}',N'{1}','{2}','{3}','{4}')", idTableOrder, TableOrderName, SDTNguoiDat, DateTimeOrder, tableType);
            int i = DataProvider.Instance.ExecuteNonQuery(query);
            return i > 0;
        }

        public bool updateTable(int id, string name, string status)
        {
            string query = string.Format("update foodTable set name = N'{0}' , status = N'{1}' where id = {2}", name, status, id);
            int i = DataProvider.Instance.ExecuteNonQuery(query);
            return i > 0;
        }

        public bool delete(int id,string TimeOrder)
        {
            string query = string.Format("delete TableOrder where IdTable = {0} and TimeOrder = '{1}'", id,TimeOrder);
            int i = DataProvider.Instance.ExecuteNonQuery(query);
            return i > 0;
        }

        public bool isReallyOrder(string id, string timeOrder)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery(string.Format("select * from TableOrder where IdTable = {0} and "+
                                                                                    "(datediff(hh,'{1}',TimeOrder)<=3 and datediff(hh,'{1}',TimeOrder)>=-3)", id,timeOrder));
            if (data.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

    

    }
}
