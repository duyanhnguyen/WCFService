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
    public partial class FormQuyenNhanVien : Form
    {
        ServiceQuyenNhanVien.ServiceQuyenNhanVienClient obj = new ServiceQuyenNhanVien.ServiceQuyenNhanVienClient();
        public FormQuyenNhanVien()
        {
            InitializeComponent();
            load();
        }

        public void load()
        {
            dataGridView1.DataSource = obj.SelectQuyenNhanVien();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            FormQuanLy form = new FormQuanLy();
            this.Visible = false;
            form.Visible = true;
        }

        private void Disable()
        {
            txtMa.Enabled = false;
            txtTen.Enabled = false;

            txtMa.Text = "";
            txtTen.Text = "";

            btnHuy.Enabled = false;
        }

        private void Enable()
        {
            txtMa.Enabled = true;
            txtTen.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Disable();
            Enable();
            ServiceQuyenNhanVien.QuyenNhanVien objqnv = (ServiceQuyenNhanVien.QuyenNhanVien)dataGridView1.SelectedRows[0].DataBoundItem;
            txtMa.Text = objqnv.MaQ.ToString();
            txtTen.Text = objqnv.TenQ.ToString();
            txtTen.Focus();
            txtMa.Enabled = false;
            txtTen.Enabled = true;
            btnHuy.Enabled = true;
            btnLuu.Text = "Sửa";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Disable();
            Enable();
            txtMa.Text = "";
            txtTen.Text = "";
            txtMa.Focus();
            btnLuu.Text = "Thêm";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            ServiceQuyenNhanVien.QuyenNhanVien objqnv = (ServiceQuyenNhanVien.QuyenNhanVien)dataGridView1.SelectedRows[0].DataBoundItem;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?!?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (obj.DeleteQuyenNhanVien(objqnv) == 1)
                {
                    MessageBox.Show("Xóa thành công!");
                    load();
                }
                else MessageBox.Show("Còn dữ liệu ràng buộc. Không thể xóa!");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMa.Text != "")
            {
                try
                {
                    if (txtMa.Enabled == true) //insert
                    {
                        ServiceQuyenNhanVien.QuyenNhanVien objqnv = new ServiceQuyenNhanVien.QuyenNhanVien();
                        objqnv.MaQ = int.Parse(txtMa.Text.ToString());
                        objqnv.TenQ = txtTen.Text;
                        try
                        {
                            if (obj.InsertQuyenNhanVien(objqnv) == 1)
                            {
                                MessageBox.Show("Thêm thành công!");
                                txtMa.Text = "";
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
                        ServiceQuyenNhanVien.QuyenNhanVien objqnv = new ServiceQuyenNhanVien.QuyenNhanVien();
                        objqnv.MaQ = int.Parse(txtMa.Text.ToString());
                        objqnv.TenQ = txtTen.Text;
                        try
                        {
                            if (obj.UpdateQuyenNhanVien(objqnv) == 1)
                            {
                                MessageBox.Show("Sửa thành công!");
                                txtMa.Text = "";
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
