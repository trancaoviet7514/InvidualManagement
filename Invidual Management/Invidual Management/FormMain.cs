using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class FormMain : Form
    {
        private Account loginAccount;
        BindingSource bdChiTiet = new BindingSource();
        BindingSource bdThu = new BindingSource();
        string g_LoaiChiTieu;
        string g_NoiDung;
        string g_SoTien;
        string g_ThoiGian;
        string g_ThoiGian_Thang_LichBieu;
        string g_NoiDungLichBieuCuoiCung;
        

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; }
        }
        public FormMain() {
            
            InitializeComponent();
            
            cbLoaiChiTieu_Seach.SelectedIndex = 0;

            dtgvChiTiet.DataSource = bdChiTiet;
            dtgvThu.DataSource = bdThu;

            loadDatePicker();
            chbNgayhomqua.Checked = true;

            btnSeach_Click(new object(), new EventArgs());
            lbThongBaoKetQuaTimKiem.Text = "";

            btnSeach_Thu_Click(new object(), new EventArgs());
            lbThongBaoTimKiem_Thu.Text = "";
            
            addBinding();

            loadIntoLichBieu();
            tabPage3.Hide();
        }

        private bool KiemTraLichBieuTheoNgay(string ThoiGian)
        {
            string query = string.Format("select * from LichBieu where ThoiGian = '{0}'", ThoiGian);
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            if(dt.Rows.Count > 0)
                return true;
            return false;
        }

        private void loadIntoLichBieu()
        {
            for(int i = 1; i < 8; i++)
            {
                Button btnThu = new Button();
                btnThu.Width = 50;
                btnThu.Height = 25;
                btnThu.Text = "Thu " + i.ToString();
                if (i == 1) btnThu.Text = "CN";
                btnThu.Location = new Point((i - 1) * 50+panelNgay.Location.X, panelNgay.Location.Y-25);
                btnThu.Enabled = false;
                tabpageLichBieu.Controls.Add(btnThu);
            }
            g_ThoiGian_Thang_LichBieu = dtpkTime_LichBieu.Value.Month.ToString();//để xem thử ngày thay đổi có phải cùng tháng với ngày trước đo

            panelNgay.Controls.Clear();
            int[] SoNgay = {0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            int ThangHienTai = dtpkTime_LichBieu.Value.Month;
            int ThangKeTiep = dtpkTime_LichBieu.Value.AddMonths(1).Month;
            int ThangTruoc = dtpkTime_LichBieu.Value.AddMonths(-1).Month;

            DateTime dauthang = new DateTime(dtpkTime_LichBieu.Value.Year, dtpkTime_LichBieu.Value.Month, 1);
            int Thu = (int)dauthang.DayOfWeek;
            
            Button btnNgay;
            int ThuTuNgay = 1;
            int SoThuTuNgaycuaThangSau = 1;
            for (int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 7; j++)
                {
                    btnNgay = new Button();
                    btnNgay.Click += btnNgay_Click;
                    if (ThuTuNgay > SoNgay[ThangHienTai])
                    {
                        btnNgay.Text = SoThuTuNgaycuaThangSau.ToString();
                        SoThuTuNgaycuaThangSau++;
                        btnNgay.Enabled = false;
                        btnNgay.BackColor = Color.FromArgb(243, 243, 243);
                        btnNgay.FlatStyle = FlatStyle.Flat;
                        btnNgay.FlatAppearance.BorderColor = Color.FromArgb(243, 243, 243);
                    }
                    else
                    {
                        if (j + i * 7 < Thu)
                        {
                            btnNgay.Text = (SoNgay[ThangTruoc] - (Thu - j) + 1).ToString();
                            btnNgay.Enabled = false;
                            btnNgay.BackColor = Color.FromArgb(243, 243, 243);
                            btnNgay.FlatStyle = FlatStyle.Flat;
                            btnNgay.FlatAppearance.BorderColor = Color.FromArgb(243, 243, 243);                           
                            btnNgay.FlatAppearance.BorderSize = 1;
                        }
                        else
                        {
                            btnNgay.Text = ThuTuNgay.ToString();
                            ThuTuNgay++;
                        }
                    }
                    
                    
                    btnNgay.Width = 50;
                    btnNgay.Height = 50;
                    btnNgay.Location = new Point(j*50, i*50);

                    if(btnNgay.Enabled == true)
                    {
                        if (KiemTraLichBieuTheoNgay(new DateTime(dtpkTime_LichBieu.Value.Year, dtpkTime_LichBieu.Value.Month, Convert.ToInt32(btnNgay.Text)).ToShortDateString()) == true)
                            btnNgay.BackColor = Color.FromArgb(255, 128, 128);
                    }

                    if(btnNgay.Text== DateTime.Now.Day.ToString()&& btnNgay.Enabled==true)
                    {
                        btnNgay.FlatStyle = FlatStyle.Flat;
                        btnNgay.FlatAppearance.BorderColor = Color.FromArgb(83,83,238);
                        btnNgay.FlatAppearance.BorderSize = 2;
                    }
                    panelNgay.Controls.Add(btnNgay);
                }
                if (ThuTuNgay > SoNgay[ThangHienTai]) break;
            }
            
            
        }
       public void btnNgay_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            dtpkTime_LichBieu.Value = new DateTime(dtpkTime_LichBieu.Value.Year, dtpkTime_LichBieu.Value.Month, Convert.ToInt32(btn.Text));
            foreach (Control item in panelNgay.Controls)
            {
                if (KiemTraLichBieuTheoNgay(new DateTime(dtpkTime_LichBieu.Value.Year, dtpkTime_LichBieu.Value.Month, Convert.ToInt32(btn.Text)).ToShortDateString()) == true)
                {
                    btn.BackColor = Color.FromArgb(255, 128, 128);
                }
                if (item.Text == dtpkTime_LichBieu.Value.Day.ToString()&& item.Enabled == true)
                {
                    item.BackColor = Color.FromArgb(172, 164, 219);
                }
            }
        }
        private void loadDatePicker()
        {
            DateTime now = DateTime.Now;
            dtpkFrom.Value = new DateTime(now.Year, now.Month, 1);
            dtpkTo.Value = dtpkFrom.Value.AddMonths(1).AddDays(-1);
            dtpkFrom_Thu.Value = new DateTime(now.Year, now.Month, 1);
            dtpkTo_Thu.Value = dtpkFrom.Value.AddMonths(1).AddDays(-1);
            dtpkTime.Value = now;
            dtpkTime_LichBieu.Value = DateTime.Now;
        }

        

        private void addBinding()
        {
            txbNoiDung_Thu.DataBindings.Add(new Binding("Text", dtgvThu.DataSource, "Nội dung", true, DataSourceUpdateMode.Never));
            txbSoTien_Thu.DataBindings.Add(new Binding("Text", dtgvThu.DataSource, "Số tiền", true, DataSourceUpdateMode.Never));
            dtpkThoiGian_Thu.DataBindings.Add(new Binding("Text", dtgvThu.DataSource, "Thời gian", true, DataSourceUpdateMode.Never));
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txbNoiDung.Text.Length == 0)
            {
                txbNoiDung.Text = "Chưa có nội dung";
                txbNoiDung.Font = new Font(this.Font, FontStyle.Italic);
                txbNoiDung.ForeColor = Color.FromArgb(0, 255, 0, 0);
                return;
            }
            if (txbSoTien.Text.Length == 0)
            {
                txbSoTien.Text = "Chưa có số tiền";
                txbSoTien.Font = new Font(this.Font, FontStyle.Italic);
                txbSoTien.ForeColor = Color.FromArgb(0, 255, 0, 0);
                return;
            }
            DataTable dt = DataProvider.Instance.ExecuteQuery(string.Format("select * from ChiTiet where NoiDung = N'{0}' and ThoiGian = '{1}'", txbNoiDung.Text, dtpkTime.Value.ToShortDateString()));
            if (dt.Rows.Count != 0) {
                MessageBox.Show("Mục đã tồn tại!", "Thông báo");
                return;
            }
            DataProvider.Instance.ExecuteQuery(string.Format("insert into ChiTiet values(N'{0}',N'{1}','{2}','{3}')", cbLoaiChiTieu_Input.Text, txbNoiDung.Text, txbSoTien.Text, dtpkTime.Value.ToShortDateString()));
            g_LoaiChiTieu = cbLoaiChiTieu_Input.Text;
            g_NoiDung = txbNoiDung.Text;
            g_SoTien = txbSoTien.Text;
            g_ThoiGian = dtpkTime.Value.ToShortDateString();
           
            btnSeach_Click(sender, e);
            ChonDongHienTai();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataProvider.Instance.ExecuteQuery(string.Format("delete from ChiTiet where LoaiChiTieu = N'{0}' and NoiDung = N'{1}'and SoTien = '{2}' and ThoiGian = '{3}'", cbLoaiChiTieu_Input.Text, txbNoiDung.Text, txbSoTien.Text, dtpkTime.Value.ToShortDateString()));
            btnSeach_Click(sender,e);
        }

        private void btnSeach_Click(object sender, EventArgs e)
        {
            if (cbLoaiChiTieu_Input.DataBindings.Count != 0)
                cbLoaiChiTieu_Input.DataBindings.RemoveAt(0);
            if (txbNoiDung.DataBindings.Count != 0)
                txbNoiDung.DataBindings.RemoveAt(0);
            if (dtpkTime.DataBindings.Count != 0)
                dtpkTime.DataBindings.RemoveAt(0);
            if (txbSoTien.DataBindings.Count != 0)
                txbSoTien.DataBindings.RemoveAt(0);
            dtgvChiTiet.DataSource = 0;

            string gb_LoaiChiTieu = "0";
            string gb_Noidung = "0";
            if (chbLoaiChiTieu.Checked) gb_LoaiChiTieu = "1";
           if (chbNoiDung.Checked) gb_Noidung = "1";

            string query = string.Format("exec usp_Seach @LoaiChiTieu  = N'{0}', @NoiDung = N'{1}' , @From = '{2}' , @To = '{3}' , @Groupby = N'{4}' , @GB_LoaiChiTieu = '{5}', @GB_NoiDung = '{6}'", cbLoaiChiTieu_Seach.Text, txbNoiDung_Seach.Text, dtpkFrom.Value.ToShortDateString(), dtpkTo.Value.ToShortDateString(), cbTimeGroupby.Text, gb_LoaiChiTieu, gb_Noidung);
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            

            if (dt.Rows.Count == 0) lbThongBaoKetQuaTimKiem.Text = "Không tìm thấy!";
            else lbThongBaoKetQuaTimKiem.Text = "";

            int TongTien = 0;
            int SoTienTrungBinh = 0;
            foreach (DataRow item in dt.Rows)
            {
                TongTien += Convert.ToInt32(item["Số tiền"].ToString());
            }
            if(dt.Rows.Count > 0)
                SoTienTrungBinh = TongTien / dt.Rows.Count;
            CultureInfo culture = new CultureInfo("vi-VN");
            Thread.CurrentThread.CurrentCulture = culture;
            txbTongTien.Text = TongTien.ToString("c");
            txbSoTienTrungBinh.Text = SoTienTrungBinh.ToString("c");
            CultureInfo culture2 = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture2;

            dtgvChiTiet.DataSource = dt;
            //dtgvChiTiet.Rows[0].Selected = true;

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == "Loại chi tiêu")
                    cbLoaiChiTieu_Input.DataBindings.Add(new Binding("Text", dtgvChiTiet.DataSource, "Loại chi tiêu", true, DataSourceUpdateMode.Never));
                if (dt.Columns[i].ColumnName == "Nội dung")
                    txbNoiDung.DataBindings.Add(new Binding("Text", dtgvChiTiet.DataSource, "Nội dung", true, DataSourceUpdateMode.Never));
                if (dt.Columns[i].ColumnName == "Ngày")
                    dtpkTime.DataBindings.Add(new Binding("Text", dtgvChiTiet.DataSource, "Ngày", true, DataSourceUpdateMode.Never));
                if (dt.Columns[i].ColumnName == "Số tiền")
                    txbSoTien.DataBindings.Add(new Binding("Text", dtgvChiTiet.DataSource, "Số tiền", true, DataSourceUpdateMode.Never));
            }
            
        }

        private void chbNgayhomqua_CheckStateChanged(object sender, EventArgs e)
        {
            if (chbNgayhomqua.Checked)
            {
                dtpkFrom.Enabled = false;
                dtpkTo.Enabled = false;
                if (chbNgayhomnay.Checked)
                {
                    dtpkFrom.Value = DateTime.Now.AddDays(-1);
                    dtpkTo.Value = DateTime.Now;
                }
                else
                {
                    dtpkFrom.Value = dtpkTo.Value = DateTime.Now.AddDays(-1);
                }
            }
            else
            {
                if (chbNgayhomnay.Checked)
                {
                    dtpkFrom.Value = dtpkTo.Value = DateTime.Now;
                }
                else
                {
                    dtpkFrom.Enabled = true;
                    dtpkTo.Enabled = true;
                    loadDatePicker();
                }
            }
        }

        private void chbNgayhomnay_CheckedChanged(object sender, EventArgs e)
        {
            if (chbNgayhomnay.Checked)
            {
                dtpkFrom.Enabled = false;
                dtpkTo.Enabled = false;
                if (chbNgayhomqua.Checked)
                {
                    dtpkFrom.Value = DateTime.Now.AddDays(-1);
                    dtpkTo.Value = DateTime.Now;
                }
                else
                {
                    dtpkFrom.Value = dtpkTo.Value = DateTime.Now;
                }
            }
            else
            {
                if (chbNgayhomqua.Checked)
                {
                    dtpkFrom.Value = dtpkTo.Value = DateTime.Now.AddDays(-1);
                }
                else
                {
                    dtpkFrom.Enabled = true;
                    dtpkTo.Enabled = true;
                    loadDatePicker();
                }
            }
        }

        

        private void btnSeach_Thu_Click(object sender, EventArgs e)
        {
            dtgvThu.DataSource = 0;
            string query = string.Format("select Noidung as N'Nội dung', Sotien as N'Số tiền', Thoigian as N'Thời gian' from Thu where ThoiGian >= '{0}' and ThoiGian <= '{1}'", dtpkFrom_Thu.Value.ToShortDateString(), dtpkTo_Thu.Value.ToShortDateString());
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            if (dt.Rows.Count == 0) lbThongBaoTimKiem_Thu.Text = "Không tìm thấy!";
            else lbThongBaoTimKiem_Thu.Text = "";

            int TongTien = 0;
            foreach (DataRow item in dt.Rows)
            {
                TongTien += Convert.ToInt32(item[1].ToString());
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            Thread.CurrentThread.CurrentCulture = culture;
            txbTongTien_Thu.Text = TongTien.ToString("c");
            CultureInfo culture2 = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture2;
            //bdThu.DataSource = dt;
            dtgvThu.DataSource = dt;

        }

        private void btnThem_Thu_Click(object sender, EventArgs e)
        {
           
            if (txbNoiDung_Thu.Text.Length == 0 || txbSoTien_Thu.Text.Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập vào Nội dung hoặc số tiền");
                return;
            }
            DataTable dt = DataProvider.Instance.ExecuteQuery(string.Format("select * from Thu where NoiDung = N'{0}' and ThoiGian <= '{1}'", txbNoiDung_Thu.Text, dtpkThoiGian_Thu.Value.ToShortDateString()));
            if (dt.Rows.Count != 0)
            {
                MessageBox.Show("Mục đã tồn tại!", "Thông báo");
                return;
            }
            DataProvider.Instance.ExecuteQuery(string.Format("insert into Thu values(N'{0}',N'{1}','{2}')", txbNoiDung_Thu.Text, txbSoTien_Thu.Text, dtpkThoiGian_Thu.Value.ToShortDateString()));

            btnSeach_Thu_Click(sender, e);
        }

        private void chbTimeGroupby_CheckedChanged(object sender, EventArgs e)
        {
            if (chbTimeGroupby.Checked)
            {
                cbTimeGroupby.Enabled = true;
                cbTimeGroupby.SelectedIndex = 0;
            }
            else
            {
                cbTimeGroupby.Enabled = false;
                cbTimeGroupby.SelectedIndex = -1;
            }
        }

        private void dtgvChiTiet_SelectionChanged(object sender, EventArgs e)
        {
            g_LoaiChiTieu = cbLoaiChiTieu_Input.Text;
            g_NoiDung = txbNoiDung.Text;
            g_SoTien = txbSoTien.Text;
            g_ThoiGian = dtpkTime.Value.ToShortDateString();
        }

        private void btnUpdate_ChiTiet_Click(object sender, EventArgs e)
        {
            string query = string.Format("delete from ChiTiet where LoaiChiTieu = N'{0}' and NoiDung = N'{1}' and SoTien = '{2}' and ThoiGian = '{3}'", g_LoaiChiTieu, g_NoiDung, g_SoTien, g_ThoiGian);
            DataProvider.Instance.ExecuteQuery(query);
            query = string.Format("insert into ChiTiet values ( N'{0}',N'{1}','{2}','{3}')", cbLoaiChiTieu_Input.Text, txbNoiDung.Text, txbSoTien.Text, dtpkTime.Value.ToShortDateString());
            DataProvider.Instance.ExecuteQuery(query);
            
            g_LoaiChiTieu = cbLoaiChiTieu_Input.Text;
            g_NoiDung = txbNoiDung.Text;
            g_SoTien = txbSoTien.Text;
            g_ThoiGian = dtpkTime.Value.ToShortDateString();
            btnSeach_Click(sender, e);
            ChonDongHienTai();
            
        }
        void ChonDongHienTai()
        {
            DataGridViewRow item;
            for (int i = 0; i < dtgvChiTiet.Rows.Count-1; i++)
            {
                item = dtgvChiTiet.Rows[i];
                string thoigiantamp = item.Cells[0].Value.ToString().Substring(0, 10);
                if (thoigiantamp[1] == '/' && thoigiantamp[3] == '/') thoigiantamp = thoigiantamp.Substring(0, 8);
                if(thoigiantamp[1] == '/' && thoigiantamp[3] != '/') thoigiantamp = thoigiantamp.Substring(0, 9);
                if (thoigiantamp[1] != '/' && thoigiantamp[3] == '/') thoigiantamp = thoigiantamp.Substring(0, 9);
                if ((thoigiantamp == g_ThoiGian) && (item.Cells[1].Value.ToString() == g_LoaiChiTieu) && (item.Cells[2].Value.ToString() == g_NoiDung) && (item.Cells[3].Value.ToString() == g_SoTien)) {
                    dtgvChiTiet.Rows[0].Selected = false;
                    dtgvChiTiet.Rows[i].Selected = true;
                    dtgvChiTiet.FirstDisplayedScrollingRowIndex = i;
                    
                    break;
                }
                
            }
            
        }

        private void btnXoa_Thu_Click(object sender, EventArgs e)
        {
            string query = string.Format("delete from Thu where NoiDung = N'{0}' and ThoiGian = '{1}'", txbNoiDung_Thu.Text, dtpkThoiGian_Thu.Value.ToShortDateString());
            DataProvider.Instance.ExecuteQuery(query);
            btnSeach_Thu_Click(sender, e);
        }

        private void dtpkTime_LichBieu_ValueChanged(object sender, EventArgs e)
        {
            lbThang.Text = "Tháng " + dtpkTime_LichBieu.Value.Month.ToString() + "/" + dtpkTime_LichBieu.Value.Year.ToString();
            if (dtpkTime_LichBieu.Value.Month.ToString() != g_ThoiGian_Thang_LichBieu)
            {
                loadIntoLichBieu();
            }
            foreach (Control item in panelNgay.Controls)
            {
                if(item.BackColor == Color.FromArgb(172, 164, 219))
                {
                    item.BackColor = Color.FromArgb(225, 225, 225);
                }
                if (item.Text == dtpkTime_LichBieu.Value.Day.ToString()&& item.Enabled == true)
                {
                    item.BackColor = Color.FromArgb(172, 164, 219);
                }
            }
            loadLichBieu();
        }

        private void loadLichBieu()
        {
            string query = string.Format("select NoiDung as N'Nội dung' from LichBieu where ThoiGian = '{0}'", dtpkTime_LichBieu.Value.ToShortDateString());
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            //lvCongViec.Items.Clear();
            panelNote.Controls.Clear();
            int vitriY = 0;
            foreach (DataRow item in dt.Rows)
            {
                Label lable = new Label();
                lable.Text = item["Nội dung"].ToString();
                lable.BorderStyle = BorderStyle.FixedSingle;
                lable.BackColor = Color.FromArgb(142,234,166);
                lable.Width = panelNote.Width;
                lable.RightToLeft  = RightToLeft.No;
                lable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                lable.Location = new Point(0, vitriY);
                vitriY = lable.Location.Y + lable.Height;
                panelNote.Controls.Add(lable);
                lable.MouseEnter += ReChuotVaoLableLichBieu;
                lable.MouseLeave += ThoatReChuotVaoLableLichBieu;
                lable.MouseClick += lableLichBieu_MouseClick;
                
                //ListViewItem lvi = new ListViewItem(item["Nội dung"].ToString());
                //lvCongViec.Items.Add(lvi);
            }
        }
        
        private void ReChuotVaoLableLichBieu(object sender, EventArgs e)
        {
            (sender as Label).BackColor = Color.FromArgb(45, 153, 61);
            g_NoiDungLichBieuCuoiCung = (sender as Label).Text;
        }

        private void ThoatReChuotVaoLableLichBieu(object sender, EventArgs e)
        {

            (sender as Label).BackColor = Color.FromArgb(142, 234, 166);
        }
        private void btnThem_LichBieu_Click(object sender, EventArgs e)
        {
            string ThoiGian = dtpkTime_LichBieu.Value.ToShortDateString();
            string NoiDung = txbNoiDung_LichBieu.Text;
            if (NoiDung == "")
            {
                MessageBox.Show("Vui lòng nhập nội dung!");
                return;
            }

            
            string query = string.Format("select * from LichBieu where ThoiGian = '{0}' and NoiDung = '{1}'", ThoiGian, NoiDung);
            DataTable dt =  DataProvider.Instance.ExecuteQuery(query);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Nội dung đã tồn tại");
                return;
            }

            query = string.Format("insert into LichBieu values('{0}',N'+ {1}')", ThoiGian, NoiDung);
            DataProvider.Instance.ExecuteQuery(query);

            loadLichBieu();
            foreach(Control item in panelNgay.Controls)
            {
                if (item.Text == dtpkTime_LichBieu.Value.Day.ToString() && item.Enabled == true)
                {
                    item.BackColor = Color.FromArgb(255,128,128);
                }
            }
        }

        private void lableLichBieu_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip menu = new ContextMenuStrip();
                menu.Items.Add("Xóa");
                menu.Items[0].Click += btnDeleteLichBieu_Click;
                menu.Show(Cursor.Position);

            }
        }
        private void btnDeleteLichBieu_Click(object sender, EventArgs e)
        {
            string NoiDung = g_NoiDungLichBieuCuoiCung;
                     
            string ThoiGian = dtpkTime_LichBieu.Value.ToShortDateString();
            string query = string.Format("delete from LichBieu where ThoiGian = '{0}' and NoiDung = N'{1}'", ThoiGian, NoiDung);
            DataProvider.Instance.ExecuteQuery(query);
            loadLichBieu();

            foreach(Control item in panelNgay.Controls)
            {
                if (item.Enabled == true && KiemTraLichBieuTheoNgay(new DateTime(dtpkTime_LichBieu.Value.Year, dtpkTime_LichBieu.Value.Month, Convert.ToInt32(item.Text)).ToShortDateString()) == true )
                    (item as Button).FlatAppearance.BorderColor = Color.FromArgb(243, 243, 243);
                else (item as Button).FlatAppearance.BorderColor = Color.FromArgb(243, 243, 243);
            }
        }

        private void btnThangTiepTheo_Click(object sender, EventArgs e)
        {
            dtpkTime_LichBieu.Value = dtpkTime_LichBieu.Value.AddMonths(1);
         }

        private void btnThangTruocDo_Click(object sender, EventArgs e)
        {
            dtpkTime_LichBieu.Value = dtpkTime_LichBieu.Value.AddMonths(-1);
        }

        private void btnBieuDo_Click(object sender, EventArgs e)
        {
            
            gbChinhSua.Visible = false;
            lableSTTB.Visible = false;
            lableTongCong.Visible = false;
            txbTongTien.Visible = false;
            txbSoTienTrungBinh.Visible = false;
            dtgvChiTiet.Visible = false;
            gbSearch_Chi.Visible = false;            
            lbBieuDo.Location = new Point(0, 30);
            lbBieuDo.Visible = true;
            Button comeback = new Button();
            comeback.Text = "Quay về";
            comeback.Click += comeback_Click;
            comeback.Location = new Point(730, 355);
            tabPage1.Controls.Add(comeback);
        }

        private void comeback_Click(object sender, EventArgs e)
        {
            gbChinhSua.Visible = true;
            lableSTTB.Visible = true;
            lableTongCong.Visible = true;
            txbTongTien.Visible = true;
            txbSoTienTrungBinh.Visible = true;
            dtgvChiTiet.Visible = true;
            gbSearch_Chi.Visible = true;
            lbBieuDo.Visible = false;
            (sender as Button).Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void txbNoiDung_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbNoiDung_Click(object sender, EventArgs e)
        {
            if(txbNoiDung.Text == "Chưa có nội dung")
            {
                txbNoiDung.Text = "";
                txbNoiDung.Font = new Font(this.Font, FontStyle.Regular);
                txbNoiDung.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void txbSoTien_Click(object sender, EventArgs e)
        {
            if (txbSoTien.Text == "Chưa có số tiền")
            {
                txbSoTien.Text = "";
                txbSoTien.Font = new Font(this.Font, FontStyle.Regular);
                txbSoTien.ForeColor = Color.FromArgb(0, 0, 0);
            }
            
        }

        private void txbSoTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void btnThemNote_Click(object sender, EventArgs e)
        {
            gbThemNote.Visible = true;
        }

        private void btnHuyGbThemNote_Click(object sender, EventArgs e)
        {
            gbThemNote.Visible = false;
        }

        private void btnThemNote_gbThemNote_Click(object sender, EventArgs e)
        {
            string query = string.Format("select * from GhiChu where GhiChu = N'{0}'", txbNoiDung_GhiChu.Text);
            DataTable dt =  DataProvider.Instance.ExecuteQuery(query);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Loi");
                return;
            }
            query = string.Format("insert into GhiChu values (N'{0}')", txbNoiDung_GhiChu.Text);            
            DataProvider.Instance.ExecuteQuery(query);
        }
    }
}

