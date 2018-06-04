using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class fAccountProfile : Form
    {

        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangAccount(loginAccount); }
        }


        #region methods

        public fAccountProfile(Account acc)
        {
            InitializeComponent();

            this.LoginAccount = acc;
        }

        public fAccountProfile()
        {
            InitializeComponent();           
        }

        void ChangAccount(Account acc)
        {
            txbLoginName.Text = acc.UserName;
            txbDisplayName.Text = acc.DisplayName;
        }

        bool UpdateAccount(string userName, string displayName, string pass, string newpass)
        {
            return AccountDAO.Instance.UpdateAccount(userName, displayName, pass, newpass);
        }
       
        #endregion

        #region event

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string userName = txbLoginName.Text;
            string displayName = txbDisplayName.Text;
            string pass = txbPass.Text;
            string newpass = txbNewPassword.Text;
            string reenterpass = txbReenterPassword.Text;

            if (!newpass.Equals(reenterpass))
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!");
            }
            else
            {
                if (UpdateAccount(userName, displayName, pass, newpass))
                {
                    MessageBox.Show("Thay đổi thành công!");
                }
                else
                    MessageBox.Show("Mật khẩu hiện tại không đúng!");
            }
            
        }

        
    }
}
