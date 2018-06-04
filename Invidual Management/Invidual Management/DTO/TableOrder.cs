using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QuanLyQuanCafe.DTO
{
    class TableOrder
    {

        public int IDTable;
        public string TableName;
        public string SDTNguoiDat;
        public string DateOrder;
        public string TimeOrder;
        public string TableType;

        public TableOrder (DataRow row)
        {
            this.IDTable = (int)row["IdTable"];
            this.TableName = row["TableName"].ToString();
            this.SDTNguoiDat = row["SDTNguoidat"].ToString();            
            this.TimeOrder = row["TimeOrder"].ToString().Substring(0, 5);

            string datetemp = row["DateOrder"].ToString().Substring(0, 10);
            if(datetemp[1]=='/'&& datetemp[3]=='/')
                this.DateOrder = row["DateOrder"].ToString().Substring(0, 8);
            else
            {
                this.DateOrder = row["DateOrder"].ToString().Substring(0, 10);
            }
            this.TableType = row["TableOrderType"].ToString();
        }
    }
}
