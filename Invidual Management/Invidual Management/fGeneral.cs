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

    public partial class fGeneral : Form
    {
        Account account;
        List<Form> listForm = new List<Form>();

        public fGeneral()
        {
            
            InitializeComponent();           
            this.IsMdiContainer = true;
            this.Show();

            fLogin f = new fLogin();
            fLogin.myevent += updateAccount;
            f.ShowDialog();

            

        }
        public void updateAccount(Account acc)
        {
            menuOption.Location = new Point(790 - acc.DisplayName.Length*6,menuOption.Location.Y);
            
            account = acc;
           
            menuAccName.Text = acc.DisplayName;

            menuQuanLiChi.Visible = true;
            menuInfo.Visible = true;
            menuLogout.Visible = true;
            menuThoat.Visible = true;

            FormMain f1 = new FormMain();
            listForm.Add(f1);
            f1.MdiParent = this;
            f1.Show();
        }

        private void menuAdmin_Click(object sender, EventArgs e)
        {
            if (listForm.Count != 0)
            {
                listForm[listForm.Count - 1].Close();
                listForm.RemoveAt(listForm.Count - 1);
            }
            fAdmin f = new fAdmin();
            f.MdiParent = this;
            f.Show();

        }

        private void menuInfo_Click(object sender, EventArgs e)
        {
            if (listForm.Count != 0)
            {
                listForm[listForm.Count - 1].Close();
                listForm.RemoveAt(listForm.Count - 1);
            }
            fAccountProfile f = new fAccountProfile(account);
            listForm.Add(f);
            f.MdiParent = this;
            f.Show();
        }

        private void menuLogout_Click(object sender, EventArgs e)
        {
            menuQuanLiChi.Visible = false;
            menuLogout.Visible = false;
            menuThoat.Visible = false;
            menuInfo.Visible = false;

            account = null;
            menuAccName.Text = "Đăng nhập";

            listForm[listForm.Count - 1].Close();
            listForm.RemoveAt(listForm.Count - 1);
            fLogin f = new fLogin();
            f.ShowDialog();
        }

        private void menuTableManager_Click(object sender, EventArgs e)
        {
            FormMain f = new FormMain();
            f.MdiParent = this;
            f.Show();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            
        }

        private void btnTablemanager_Click(object sender, EventArgs e)
        {
            
        }

        private void menuAdmin_Click_1(object sender, EventArgs e)
        {
            if (listForm.Count != 0)
            {
                listForm[listForm.Count - 1].Close();
                listForm.RemoveAt(listForm.Count - 1);
            }
            fAdmin f = new fAdmin();
            listForm.Add(f);
            f.MdiParent = this;
            f.Show();
        }

        private void menuTableManager_Click_1(object sender, EventArgs e)
        {
            if (listForm.Count != 0)
            {
                listForm[listForm.Count - 1].Close();
                listForm.RemoveAt(listForm.Count - 1);
            }
            FormMain f = new FormMain();
            listForm.Add(f);
            f.MdiParent = this;
            f.Show();
        }

        private void menuAccName_Click(object sender, EventArgs e)
        {
            if(account == null)
            {
                
                fLogin f = new fLogin();
                f.ShowDialog();
            }
            
        }

        private void menuThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                Application.Exit();
            }
        }
    }
}
