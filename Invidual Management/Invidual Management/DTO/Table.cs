using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Table
    {
        private string tabletype;

        public string TableType
        {
            get { return tabletype; }
            set { tabletype = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        
        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private Table(int id, string name, string Status,string _tabletype)
        {
            iD = id;
            Name = name;
            status = Status;
            tabletype = _tabletype;
        }

        public Table(DataRow row)
        {
            this.iD = (int)row["id"];
            this.Name = row["name"].ToString();
            this.status = row["status"].ToString();
            this.TableType = row["tabletype"].ToString();
        }
    }
}
