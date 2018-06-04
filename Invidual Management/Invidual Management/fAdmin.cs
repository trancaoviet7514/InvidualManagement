using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class fAdmin : Form
    {

        BindingSource listfood = new BindingSource();
        BindingSource listAccount = new BindingSource();
        BindingSource listCategory = new BindingSource();
        BindingSource listTable = new BindingSource();

        #region method
        
        public fAdmin()
        {
            

            InitializeComponent();

            dtgvFood.DataSource = listfood;
            dtgvShowAccount.DataSource = listAccount;
            dtgvCategoryShow.DataSource = listCategory;
            dtgvTableShow.DataSource = listTable;

            loadIntoAccount();
            loadIntoCategory();
            loadIntoFood();
            loadIntoSales();
            loadIntoTable();

            TinhLoiNhuan();
            
            addFoodBinding();
            addAccountBinding();
            addCategoryBinding();
            addTableBinding();
            dtgvBill.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgvCategoryShow.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgvFood.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgvShowAccount.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgvTableShow.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgvChiTieu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void TinhLoiNhuan()
        {
            string query = "select totalPrice from bill where timecheckin >= '" + dtpkFrom_Thu.Value.ToString()+"' and timecheckout <= '"+ dtpkTo_Thu.Value.ToString() + "'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            long TongThu = 0;
            foreach(DataRow item in dt.Rows)
            {
                TongThu += Convert.ToInt64(item["totalPrice"]);
            }

            query ="select sotien from ChiTieu where Ngay >= '" + dtpkFrom_Thu.Value.ToString() + "' and Ngay <= '" + dtpkTo_Thu.Value.ToString() + "'";
            dt = DataProvider.Instance.ExecuteQuery(query);
            long TongChi = 0;
            foreach (DataRow item in dt.Rows)
            {
                TongChi += Convert.ToInt64(item["sotien"]);
            }
            long LoiNhuan = TongThu - TongChi;
            CultureInfo culture = new CultureInfo("vi-VN");
            Thread.CurrentThread.CurrentCulture = culture;
            txbLoiNhuan.Text = LoiNhuan.ToString("c");
            CultureInfo culture2 = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture2;

        }

        void loadIntoSales()
        {            
            LoadDateTime();
            loadDoanhThuOrderByCategory();
            loadChiTieu(); 
        }
        void loadChiTieu()
        {
            DataTable dt = DataProvider.Instance.ExecuteQuery("exec USP_ChiTieu @Danhmuc , @datefrom , @dateto", new object[] { cbDanhmucChi.Text, dtpkFrom_Chi.Value, dtpkTo_Chi.Value });
            dtgvChiTieu.DataSource = dt;

            long TongChi = 0;
            foreach(DataRow row in dt.Rows)
            {
                TongChi += Convert.ToInt64(row["sotien"]);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            Thread.CurrentThread.CurrentCulture = culture;
            txbTongChi.Text = TongChi.ToString("c");
            CultureInfo culture2 = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture2;
        }

        void loadIntoFood()
        {
            LoadListFood();
        }

        void loadIntoCategory()
        {
            loadListCategory();
            LoadListCategory();
        }

        void loadIntoAccount()
        {
            loadListAccount();
            //loadListAccountType();
        }

        void loadIntoTable()
        {
            loadListTable();
        }

        void LoadListBillByDate(DateTime DateFrom, DateTime DateTo)
        {
            dtgvBill.DataSource = BillDAO.Instance.getListBillByDate(DateFrom, DateTo);
            string query = "select sum(totalPrice) as Price from bill where timecheckin >= '" + dtpkFrom_Thu.Value + "' and timecheckout<='" + dtpkTo_Thu.Value + "'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in dt.Rows)
            {
                double temp = Convert.ToDouble(item["Price"].ToString());
                
                CultureInfo culture = new CultureInfo("vi-VN");
                Thread.CurrentThread.CurrentCulture = culture;
                txbToTalPrice_Thu.Text = temp.ToString("c");
                CultureInfo culture2 = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentCulture = culture2;
                
            }
        }

        void LoadListBillByDateAndPageNum(DateTime DateFrom, DateTime DateTo, int PageNum)
        {
            dtgvBill.DataSource = BillDAO.Instance.getListBillByDateAndPageNum(DateFrom, DateTo, PageNum);
        }

        void LoadDateTime()
        {
            DateTime today = DateTime.Now;
            dtpkFrom_Thu.Value = new DateTime(today.Year, today.Month, 1);
            dtpkTo_Thu.Value = dtpkFrom_Thu.Value.AddMonths(1).AddDays(-1);
            dtpkFrom_Chi.Value = new DateTime(today.Year, today.Month, 1);
            dtpkTo_Chi.Value = dtpkFrom_Thu.Value.AddMonths(1).AddDays(-1);
        }

        void LoadListFood()
        {
            listfood.DataSource = FoodDAO.Instance.getListFood();


            
        }

        void LoadListCategory()
        {
            cbCategory.DataSource = CategoryDAO.Instance.getListCategory();
            cbCategory.DisplayMember = "name";

            cbCategory2.DataSource = CategoryDAO.Instance.getListCategory();
            cbCategory2.DisplayMember = "name";
            cbCategory2.SelectedIndex = -1;

            cbCategorySeach_DoanhThu.DataSource = CategoryDAO.Instance.getListCategory();
            cbCategorySeach_DoanhThu.DisplayMember = "name";
            cbCategorySeach_DoanhThu.SelectedIndex = -1;
        }

        void addFoodBinding()
        {
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "id", true, DataSourceUpdateMode.Never));
            txbFoodName.DataBindings.Add(new Binding("text", dtgvFood.DataSource, "name", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("value", dtgvFood.DataSource, "price", true, DataSourceUpdateMode.Never));
            cbCategory.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "category", true, DataSourceUpdateMode.Never));
        }

        void addAccountBinding()
        {
            txbUserName.DataBindings.Add(new Binding("text", dtgvShowAccount.DataSource, "userName", true, DataSourceUpdateMode.Never));
            txbUserNameShow.DataBindings.Add(new Binding("text", dtgvShowAccount.DataSource, "displayName", true, DataSourceUpdateMode.Never));
            
        }

        void addCategoryBinding()
        {
            txbCategoryID.DataBindings.Add(new Binding("text", dtgvCategoryShow.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txbCategoryName.DataBindings.Add(new Binding("text", dtgvCategoryShow.DataSource, "Name", true, DataSourceUpdateMode.Never));
                
        }

        void addTableBinding()
        {
            txbTableID.DataBindings.Add(new Binding("text", dtgvTableShow.DataSource, "id", true, DataSourceUpdateMode.Never));
            txbTableName.DataBindings.Add(new Binding("text", dtgvTableShow.DataSource, "name", true, DataSourceUpdateMode.Never));
            cbtableType.DataBindings.Add(new Binding("text", dtgvTableShow.DataSource, "tabletype", true, DataSourceUpdateMode.Never));
            cbStatusTable.DataBindings.Add(new Binding("text", dtgvTableShow.DataSource, "status", true, DataSourceUpdateMode.Never));

        }

        List<Food> findFoodByName(string foodName)
        {
            return FoodDAO.Instance.findListFoodByName(foodName);
        }

        #region Category methods

            void loadListCategory()
        {
            listCategory.DataSource = CategoryDAO.Instance.getListCategory();
        }
            
            bool insertCategory(string name)
            {
                return CategoryDAO.Instance.insertCategory(name);
            }

            bool updateCategory(int id, string name)
        {
            return CategoryDAO.Instance.updateCategory(id, name);
        }

            bool deleteCategory(int id)
        {
            return CategoryDAO.Instance.deleteCategory(id);
        }
        
        #endregion

        #region Account methods

            void loadListAccountType()
            {
                DataTable data = new DataTable();
                data.Columns.Add("AccountType", typeof(string));

                DataRow row1 = data.NewRow();
                row1["AccountType"] = "staff";
                data.Rows.Add(row1);
                
                DataRow row2 = data.NewRow();
                row2["AccountType"] = "Admin";
                data.Rows.Add(row2);

                cbAccountType.DataSource = data;
                cbAccountType.DisplayMember = "AccountType";
            }
           
            void loadListAccount()
        {
            listAccount.DataSource = AccountDAO.Instance.getListAccount();
        }

            bool insertAccount(string userName, string displayName, int type)
            {
                return AccountDAO.Instance.insertAccount(userName, displayName, type);

            }

            bool updateAccount(string userName, string displayName, int type)
        {
            return AccountDAO.Instance.updateAccount(userName, displayName, type);
        }

            bool deleteAccount(string userName)
        {
            return AccountDAO.Instance.deleteAccount(userName);
        }

            bool resetPassword(string userName)
            {
                return AccountDAO.Instance.resetPassword(userName);
            }

        #endregion

        #region Food Method
       
            bool InsertFood(string foodName, int idCategory, float price)
        {
            return FoodDAO.Instance.insertFood(foodName, idCategory, price);
        }

            bool updateFood(int id, string foodName, int idCategory, float price)
        {
            return FoodDAO.Instance.updateFood(id, foodName, idCategory, price);
        }

            bool deleteFood(int id)
        {
            return FoodDAO.Instance.deleteFood(id);
        }

        #endregion

        #region TableMethods
        
            void loadListTable()
        {
            listTable.DataSource = TableDAO.Instance.loadListTable();
        }

            bool insertTable(string name,string tableType)
        {
            return TableDAO.Instance.insertTable(name, tableType);
        }

            bool updateTable(int id, string name, string status,string tabletype)
        {
            return TableDAO.Instance.updateTable(id, name, status,tabletype);
        }

            bool deleteTable(int id)
        {
            return TableDAO.Instance.deleteTable(id);
        }
        
        #endregion

        #region Bill Methods
            
        #endregion

        #endregion

        #region event

            

            #region Food event

                private void btnViewFood_Click(object sender, EventArgs e)
                {
                    LoadListFood();
                }

                private void txbFoodID_TextChanged(object sender, EventArgs e)
                {
                    

                    //int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["CategoryId"].Value;

                    //Category category = CategoryDAO.Instance.getCategoryByid(id);

                    

                    //foreach (Category item in cbCategory.Items)
                    //{
                    //    if (item.Id == category.Id)
                    //    {
                    //        cbCategory.SelectedItem = item;
                    //        break;
                    //    }

                    //}

                }

                private void btnAddFood_Click(object sender, EventArgs e)
                {
                    string foodName = txbFoodName.Text;

                    Food food = FoodDAO.Instance.findFoodByName(foodName);

                    if (food!=null)
                    {
                        MessageBox.Show("Món ăn đã tồn tại");
                        return;
                    }

                    int idCategory = (cbCategory.SelectedItem as Category).Id;
                    float price = (float)nmFoodPrice.Value;

                    if (InsertFood(foodName, idCategory, price))
                    {
                        MessageBox.Show("Thêm món thành công");
                        LoadListFood();
                    }
                    else
                        MessageBox.Show("Đã xảy ra lỗi");
                }

                private void btnEditFood_Click(object sender, EventArgs e)
                {
                    string foodName = txbFoodName.Text;
                    int idCategory = (cbCategory.SelectedItem as Category).Id;
                    float price = (float)nmFoodPrice.Value;
                    int id = Convert.ToInt32(txbFoodID.Text);

                    if (updateFood(id, foodName, idCategory, price))
                    {
                        MessageBox.Show("Cập nhật thành công");
                        LoadListFood();
                    }
                    else
                        MessageBox.Show("Đã xảy ra lỗi");
                }

                private void btnDeleteFood_Click(object sender, EventArgs e)
                {

                    int id = Convert.ToInt32(txbFoodID.Text);

                    if (deleteFood(id))
                    {
                        MessageBox.Show("Xóa món thành công");
                        LoadListFood();
                    }
                    else
                        MessageBox.Show("Đã xảy ra lỗi");
                }

                private void btnSeachFood_Click(object sender, EventArgs e)
                {
                    listfood.DataSource = findFoodByName(txbSeachFoodName.Text);
                }

                private void btnViewFood_Click_1(object sender, EventArgs e)
                {
                    int id = (cbCategory2.SelectedItem as Category).Id;

                    listfood.DataSource = FoodDAO.Instance.getListFoodByCategoryID(id);
                }
                
                private void btnViewAllFood_Click(object sender, EventArgs e)
                {
                    LoadListFood();
                }

            #endregion

            #region Account event

                private void btnAccountShow_Click(object sender, EventArgs e)
                {
                    loadListAccount();
                }

                private void btnAddAccount_Click(object sender, EventArgs e)
                {
                    string userName = txbUserName.Text;
                    string displayName = txbUserNameShow.Text;

                    int type = 1-cbAccountType.SelectedIndex;

                    if (insertAccount(userName, displayName, type))
                    {
                        MessageBox.Show("Thêm tài khoản thành công");
                        loadListAccount();
                    }
                    else
                    {
                        MessageBox.Show("Tên tài khoản đã tồn tại");
                    }
                }

                private void btnDeleteAccount_Click(object sender, EventArgs e)
                {
                    string userName = txbUserName.Text;

                    if (deleteAccount(userName))
                    {
                        MessageBox.Show("Xóa tài khoản thành công");
                        loadListAccount();
                    }
                    else
                    {
                        MessageBox.Show("Đã xảy ra lỗi");
                    }
                }

                private void btnEditAccount_Click(object sender, EventArgs e)
                {
                    string userName = txbUserName.Text;
                    string displayName = txbUserNameShow.Text;
                    int type = 1-cbAccountType.SelectedIndex;

                    if (updateAccount(userName, displayName, type))
                    {
                        MessageBox.Show("Cập nhật thành công");
                        loadListAccount();
                    }
                    else
                    {
                        MessageBox.Show("Đã xảy ra lỗi");
                    }
                }

                private void btnResetPassword_Click(object sender, EventArgs e)
                {
                    string userName = txbUserName.Text;
                    if (resetPassword(userName))
                        MessageBox.Show("Đặt lại mật khẩu thành công");
                    else
                        MessageBox.Show("Đã xảy ra lỗi");

                }

                private void txbUserName_TextChanged(object sender, EventArgs e)
                {
                    

                    if (dtgvShowAccount.SelectedCells.Count > 0)
                    {
                        try
                        {
                            int AccountType = (int)dtgvShowAccount.SelectedCells[0].OwningRow.Cells["type"].Value;

                            if (AccountType == 1)
                            {
                                cbAccountType.SelectedIndex = 1;
                            }
                            else
                            {
                                cbAccountType.SelectedIndex = 0;
                            }

                        }
                        catch {
                            cbAccountType.Text = "";
                        }
                    }
                }


            #endregion

            #region Category event

                private void btnCategoryView_Click(object sender, EventArgs e)
                {
                    loadListCategory();
                }

                private void btnCategoryEdit_Click(object sender, EventArgs e)
                {
                    int id = Convert.ToInt32(txbCategoryID.Text);
                    string name = txbCategoryName.Text;

                    if (updateCategory(id, name))
                    {
                        MessageBox.Show("Cập nhật danh mục thức ăn thành công!");
                        loadListCategory();
                    }
                    else
                        MessageBox.Show("Đã có lỗi!");
                }

                private void btnCategoryDelete_Click(object sender, EventArgs e)
                {
                    int id = Convert.ToInt32(txbCategoryID.Text);

                    if (deleteCategory(id))
                    {
                        MessageBox.Show("Xóa danh mục thức ăn thành công!");
                        loadListCategory();
                    }
                    else
                        MessageBox.Show("Đã có lỗi!");
                }

                private void btnAddCategory_Click(object sender, EventArgs e)
                {
                    string name = txbCategoryName.Text;

                    if (insertCategory(name))
                    {
                        MessageBox.Show("Thêm danh mục thức ăn thành công!");
                        loadListCategory();
                    }
                    else
                        MessageBox.Show("Đã có lỗi!");
                }

            #endregion

            #region Table event

                private void txbTableID_TextChanged(object sender, EventArgs e)
                {
                    string status = (string)dtgvTableShow.SelectedCells[0].OwningRow.Cells["status"].Value;

                    if (status == "Trống")
                        cbStatusTable.SelectedIndex = 1;
                    else
                        cbStatusTable.SelectedIndex = 0;

                }

                private void btnViewTable_Click(object sender, EventArgs e)
                {
                    loadListTable();
                }

                private void btnAddTable_Click(object sender, EventArgs e)
                {

                    string name = txbTableName.Text;
                    string tableType;
                    int temp = cbtableType.SelectedIndex;
                    if (temp == 0) tableType = "VIP"; else tableType = "Normal";

                    if (TableDAO.Instance.TableIsExisted(name))
                    {
                        MessageBox.Show("Tên bàn đã tồn tại");
                        return;
                    }

                    if (insertTable(name, tableType))
                    {
                        MessageBox.Show("Thêm bàn thành công!");
                        loadListTable();
                    }
                    else
                        MessageBox.Show("Đã có lỗi!");
                }

                private void btnDeleteTable_Click(object sender, EventArgs e)
                {
                    int id = Convert.ToInt32(txbTableID.Text);

                    if (deleteTable(id))
                    {
                        MessageBox.Show("Xóa thành công!");
                        loadListTable();
                    }
                    else
                        MessageBox.Show("Đã có lỗi!");
                }

                private void btnEditTable_Click(object sender, EventArgs e)
                {
                    int id = Convert.ToInt32(txbTableID.Text);
                    string status = cbStatusTable.Text;
                    string tabletype = cbtableType.SelectedItem.ToString();
                    string name = txbTableName.Text;

                    if (updateTable(id, name, status,tabletype))
                    {
                        MessageBox.Show("Cập nhật bàn thành công!");
                        loadListTable();
                    }
                    else
                        MessageBox.Show("Đã có lỗi!");
                }

            #endregion

            #region Bill event

                private void btnReport_Click(object sender, EventArgs e)
                {
                    loadDoanhThuOrderByCategory();
                }
        void loadDoanhThuOrderByCategory()
        {
            string categoryName = cbCategorySeach_DoanhThu.Text;
            string TableType = cbTableTypeSeach_DoanhThu.Text;
            
            string datefrom = dtpkFrom_Thu.Value.ToString();
            string dateto = dtpkTo_Thu.Value.ToString();
            string query = "exec USP_ReportThu @DateFrom , @DateTo , @Category , @FoodName , @TableType ";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { datefrom, dateto, categoryName,"", TableType });
            dtgvBill.DataSource = dt;

            //Hiển thị ở ô tổng cộng
            long TongCong = 0;
            foreach(DataRow item in dt.Rows)
            {
                TongCong += Convert.ToInt64(item["Tổng tiền"]);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            Thread.CurrentThread.CurrentCulture = culture;
            txbToTalPrice_Thu.Text = TongCong.ToString("c");
            CultureInfo culture2 = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture2;
            
            
        }

        #endregion

        #endregion
        

        private void btnAdd_Chi_Click(object sender, EventArgs e)
        {
            string danhmuc = cbDanhmucChi.Text;
            if (danhmuc == "") danhmuc = "Khác";
            string Sotien = txbSoTien.Text;
            if (Sotien == "")
            {
                MessageBox.Show("Bạn chưa nhập số tiền");
                return;
            }
            DataProvider.Instance.ExecuteQuery(string.Format("insert into ChiTieu (category, info, ngay, sotien) values(N'{0}',N'{1}', getdate() , {2} )", danhmuc, txbNoiDungChi.Text, Sotien));
            loadChiTieu();
        }

        private void txbSoTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnView_Chi_Click(object sender, EventArgs e)
        {
            loadChiTieu();
        }
        

        private void dtgvShowAccount_SelectionChanged(object sender, EventArgs e)
        {
            try {
                int acctype = Convert.ToInt32(dtgvShowAccount.CurrentRow.Cells[2].Value.ToString());
                cbAccountType.SelectedIndex = 1 - acctype;
            }
            catch(Exception E) { } 
        }

        private void dtgvFood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
