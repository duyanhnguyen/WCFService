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
    public partial class FormNhaSanXuat : Form
    {
        ServiceNhaSanXuat.ServiceNhaSanXuatClient obj = new ServiceNhaSanXuat.ServiceNhaSanXuatClient();
        public FormNhaSanXuat()
        {
            InitializeComponent();
            load();
        }

        public void load()
        {
            dataGridView1.DataSource = obj.SelectNhaSanXuat();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            FormQuanLy form = new FormQuanLy();
            this.Visible = false;
            form.Visible = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Disable();
            Enable();
            ServiceNhaSanXuat.NhaSanXuat objnsx = (ServiceNhaSanXuat.NhaSanXuat)dataGridView1.SelectedRows[0].DataBoundItem;
            txtMa.Text = objnsx.MaNSX.ToString();
            txtTen.Text = objnsx.TenNSX.ToString();
            txtTen.Focus();
            txtTen.Enabled = true;
            btnHuy.Enabled = true;
            btnLuu.Text = "Sửa";
        }

        private void Disable()
        {
            txtTen.Enabled = false;

            txtMa.Text = "";
            txtTen.Text = "";

            btnHuy.Enabled = false;
        }

        private void Enable()
        {
            txtTen.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Disable();
            Enable();
            txtMa.Text = "";
            txtTen.Text = "";
            txtTen.Focus();
            btnLuu.Text = "Thêm";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            ServiceNhaSanXuat.NhaSanXuat objnsx = (ServiceNhaSanXuat.NhaSanXuat)dataGridView1.SelectedRows[0].DataBoundItem;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?!?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (obj.DeleteNhaSanXuat(objnsx) == 1)
                {
                    MessageBox.Show("Xóa thành công!");
                    load();
                }
                else MessageBox.Show("Còn dữ liệu ràng buộc. Không thể xóa!");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtTen.Text != "")
            {
                try
                {
                    if (txtMa.Text == "") //insert
                    {
                        ServiceNhaSanXuat.NhaSanXuat objnsx = new ServiceNhaSanXuat.NhaSanXuat();
                        objnsx.TenNSX = txtTen.Text;
                        try
                        {
                            if (obj.InsertNhaSanXuat(objnsx) == 1)
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
                        ServiceNhaSanXuat.NhaSanXuat objnsx = new ServiceNhaSanXuat.NhaSanXuat();
                        objnsx.MaNSX = int.Parse(txtMa.Text.ToString());
                        objnsx.TenNSX = txtTen.Text;
                        try
                        {
                            if (obj.UpdateNhaSanXuat(objnsx) == 1)
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
