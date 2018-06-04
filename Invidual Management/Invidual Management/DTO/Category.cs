using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Category
    {

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Category(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public Category(DataRow row)
        {
            this.id = (int)row["CategoryID"];
            this.name = row["CategoryName"].ToString();
        }

    }
}
