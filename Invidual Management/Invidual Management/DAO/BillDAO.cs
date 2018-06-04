using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO();  return BillDAO.instance; }
            private set { BillDAO.instance = value; }
        }

        public int GetBillIDUnCheckoutBytableID(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from Bill where idTable = " + id + "and status = 0");
            if(data.Rows.Count > 0){

                Bill bill = new Bill(data.Rows[0]);
                return bill.Id;
            }
            else{
                return -1;
            }
        }

        public void InsertBill(int id)
        {
            DataProvider.Instance.ExecuteQuery("exec InsertBill @idTable", new object[] { id });
        }

        public int getMaxIDBill()
        {
            return (int)DataProvider.Instance.ExcuteScalar("select max(id) from Bill");
        }

        public void CheckOut(int id, int idTable, int discount, float TotalPrice)
        {

            string query = "update Bill set TotalPrice = " + TotalPrice + " , timeCheckOut = getdate() , status = 1 , Discount = "+ discount + " where id = " + id;
            DataProvider.Instance.ExecuteQuery(query);
            query = "update FoodTable set status = N'Trống' where id = "+idTable;
            DataProvider.Instance.ExecuteQuery(query);

        }

        public DataTable getListBillByDate(DateTime DateFrom, DateTime DateTo)
        {
            return DataProvider.Instance.ExecuteQuery(" exec USP_getListBillByDate @datecheckin , @datecheckout ", new object[]{DateFrom, DateTo});
        }

        public DataTable getListBillByDateAndPageNum(DateTime DateFrom, DateTime DateTo, int PageNum)
        {
            return DataProvider.Instance.ExecuteQuery("exec USP_SeachBillByPage @PageNum , @DatecheckIn , @DateCheckOut", new object[] { PageNum, DateFrom, DateTo });
        }

    }
}
