using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }

        public bool Login(string username, string password) {
            
            string query = string.Format("select * from account where username = '{0}' and password = '{1}'",username,password);
           
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result.Rows.Count > 0;
        }

        public Account getAccountByUserName(string userName)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from Account where userName = '" + userName + "'");

            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }

        public bool UpdateAccount(string userName, string displayName, string pass, string newpass)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("exec USP_UpdateAccount @userName , @displayName , @pass , @newpass", new object[]{userName,displayName,pass,newpass});
            return result > 0;
        }

        public DataTable getListAccount()
        {
            return DataProvider.Instance.ExecuteQuery("select userName, displayName, type from Account");
        }

        public bool insertAccount(string userName, string displayName, int type)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from account where userName = '" + userName + "'");

            if (data.Rows.Count > 0)
                return false;
            int i = 0;
            
                i = DataProvider.Instance.ExecuteNonQuery(string.Format("insert Account (userName, displayName, type) values (N'{0}', N'{1}', {2})", userName, displayName, type));
            
            return i > 0;
        }

        public bool updateAccount(string userName, string displayName, int type)
        {
            int i = DataProvider.Instance.ExecuteNonQuery(string.Format("update Account set displayName = N'{0}', type = {1} where userName = N'{2}'", displayName, type, userName));
            return i > 0;
        }

        public bool deleteAccount(string userName)
        {
            int i = DataProvider.Instance.ExecuteNonQuery(string.Format("delete Account where userName = N'{0}'", userName));
            return i > 0;
        }

        public bool resetPassword(string userName)
        {
            int i = DataProvider.Instance.ExecuteNonQuery("update Account set password = '123456789' where userName = '" + userName + "'");
            return i > 0;
        }

    }
}
