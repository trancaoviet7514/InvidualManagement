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
    public delegate void Eventdelegate(Account acc);
    public partial class fLogin : Form
    {
        
        public static event Eventdelegate myevent; 
        

        #region methods
        
        public fLogin(Account _acc)
        {
            InitializeComponent();
            //acc = _acc;
        }
        
        public fLogin()
        {
            InitializeComponent(); 
        }

        bool Login(string username, string password)
        {
            return AccountDAO.Instance.Login(username, password);
        }

        #endregion

        #region event

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                Application.Exit();
            }
            
        }
        
        public void btnDangNhap_Click(object sender, EventArgs e)
        {
            string userName = txbLoginName.Text;
            string Password = txbPassWord.Text;

            if (Login(userName, Password))
            {
                this.Hide();
                lbThongBao.Text = "";
                Account loginAccount = AccountDAO.Instance.getAccountByUserName(userName);
                
                //chạy bên form kia
                if (myevent!=null)
                    myevent(loginAccount);
                
            }
            else
            {
                lbThongBao.Text = "Tên đăng nhập hoặc mật khẩu không đúng !";
            }

        }

        #endregion        
        
    }
}
