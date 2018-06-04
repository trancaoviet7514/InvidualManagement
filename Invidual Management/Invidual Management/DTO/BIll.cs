using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Bill
    {
        public Bill(int id, DateTime? datecheckin, DateTime? datecheckout, int status, int discount = 0)
        {
            this.id = id;
            this.DateCheckIn = datecheckin;
            this.DateCheckOut = datecheckout;
            this.status = status;
            this.Discount = discount;

        }

        public Bill(DataRow row){
            this.id = (int)row["id"];
            this.DateCheckIn = (DateTime?)row["timecheckin"];

            var datecheckouttemp = row["timecheckout"];
            if (datecheckouttemp.ToString() != "")
                this.DateCheckOut = (DateTime?)datecheckouttemp;
           
            this.status = (int)row["status"];
            //this.Discount = (int)row["discount"];
            this.Discount = (int)Convert.ToInt64(row["discount"]);
        }

        private int discount;

        public int Discount
        {
            get { return discount; }
            set { discount = value; }
        }


        private int status; 

        private DateTime? DateCheckOut;

        public DateTime? DateCheckOut1
        {
            get { return DateCheckOut; }
            set { DateCheckOut = value; }
        }
        private DateTime? DateCheckIn;

        public DateTime? DateCheckIn1
        {
            get { return DateCheckIn; }
            set { DateCheckIn = value; }
        }
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}
