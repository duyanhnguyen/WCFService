using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobileDesktop
{
    public partial class FormNhanVien : Form
    {
        ServiceNhanVien.ServiceNhanVienClient obj = new ServiceNhanVien.ServiceNhanVienClient();
        ServiceQuyenNhanVien.ServiceQuyenNhanVienClient obj2 = new ServiceQuyenNhanVien.ServiceQuyenNhanVienClient();

        public FormNhanVien()
        {
            InitializeComponent();
            load();
        }

        public void load()
        {
            dataGridView1.DataSource = obj.SelectNhanVien();
            dataGridView1.Columns["QuyenNV"].Visible = false;
            dataGridView1.Columns["QuyenNhanVien"].Visible = false;

            cbxQuyen.DataSource = obj2.SelectQuyenNhanVien();
            cbxQuyen.ValueMember = "MaQ";
            cbxQuyen.DisplayMember = "TenQ";
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            FormQuanLy form = new FormQuanLy();
            this.Visible = false;
            form.Visible = true;
        }

        public void Disable()
        {
            txtTenNV.Enabled = false;
            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            txtEmail.Enabled = false;


            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtEmail.Text = "";

            btnHuy.Enabled = false;
        }

        public void Enable()
        {
            txtTenNV.Enabled = true;
            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            txtEmail.Enabled = true;

            btnHuy.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Disable();
            Enable();
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtEmail.Text = "";
            txtTenNV.Focus();

            cbxQuyen.SelectedIndex = 0;
            btnLuu.Text = "Thêm";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            ServiceNhanVien.NhanVien objnv = (ServiceNhanVien.NhanVien)dataGridView1.SelectedRows[0].DataBoundItem;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?!?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (obj.DeleteNhanVien(objnv) == 1)
                {
                    MessageBox.Show("Xóa thành công!");
                    load();
                }
                else MessageBox.Show("Còn dữ liệu ràng buộc. Không thể xóa!");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Disable();
            Enable();
            ServiceNhanVien.NhanVien objnv = (ServiceNhanVien.NhanVien)dataGridView1.SelectedRows[0].DataBoundItem;
            txtMaNV.Text = objnv.MaNV.ToString();
            txtTenNV.Text = objnv.TenNV.ToString();
            txtUsername.Text = objnv.Username.ToString();
            txtPassword.Text = objnv.Password.ToString();
            txtEmail.Text = objnv.Email.ToString();

            cbxQuyen.SelectedValue = objnv.QuyenNV;
            btnLuu.Text = "Sửa";
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtTenNV.Text != "")
            {
                try
                {
                    if (txtMaNV.Text == "") //insert
                    {
                        ServiceNhanVien.NhanVien objnv = new ServiceNhanVien.NhanVien();
                        ServiceQuyenNhanVien.QuyenNhanVien objqnv = (ServiceQuyenNhanVien.QuyenNhanVien)cbxQuyen.SelectedItem;
                        objnv.TenNV = txtTenNV.Text;
                        objnv.Username = txtUsername.Text;
                        objnv.Password = txtPassword.Text;
                        objnv.Email = txtEmail.Text;
                        objnv.QuyenNV = objqnv.MaQ;
                        try
                        {
                            if (obj.InsertNhanVien(objnv) == 1)
                            {
                                MessageBox.Show("Thêm thành công!");
                                txtMaNV.Text = "";
                                load();
                            }
                            else MessageBox.Show("Có lỗi xảy ra. Vui lòng thao tác lại!");
                        }
                        catch
                        {
                            MessageBox.Show("Có lỗi xảy ra. Vui lòng thao tác lại!");
                        }
                    }
                    else //update
                    {
                        ServiceNhanVien.NhanVien objnv = new ServiceNhanVien.NhanVien();
                        ServiceQuyenNhanVien.QuyenNhanVien objqnv = (ServiceQuyenNhanVien.QuyenNhanVien)cbxQuyen.SelectedItem;
                        objnv.MaNV = int.Parse(txtMaNV.Text.ToString());
                        objnv.TenNV = txtTenNV.Text;
                        objnv.Username = txtUsername.Text;
                        objnv.Password = txtPassword.Text;
                        objnv.Email = txtEmail.Text;
                        objnv.QuyenNV = objqnv.MaQ;
                        try
                        {
                            if (obj.UpdateNhanVien(objnv) == 1)
                            {
                                MessageBox.Show("Sửa thành công!");
                                txtMaNV.Text = "";
                                load();
                            }
                            else MessageBox.Show("Có lỗi xảy ra. Vui lòng thao tác lại!");
                        }
                        catch
                        {
                            MessageBox.Show("Có lỗi xảy ra. Vui lòng thao tác lại!");
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Có lỗi xảy ra. Vui lòng thao tác lại!");
                }
            }
            else
            {
                MessageBox.Show("Chưa hoàn tất thông tin!");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Disable();
        }
    }
}
