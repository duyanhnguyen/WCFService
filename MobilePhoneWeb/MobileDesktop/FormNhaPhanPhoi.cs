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
    public partial class FormNhaPhanPhoi : Form
    {
        ServiceNhaPhanPhoi.ServiceNhaPhanPhoiClient obj = new ServiceNhaPhanPhoi.ServiceNhaPhanPhoiClient();
        public FormNhaPhanPhoi()
        {
            InitializeComponent();
            load();
        }

        public void load()
        {
            dataGridView1.DataSource = obj.SelectNhaPhanPhoi();
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
            ServiceNhaPhanPhoi.NhaPhanPhoi objnpp = (ServiceNhaPhanPhoi.NhaPhanPhoi)dataGridView1.SelectedRows[0].DataBoundItem;
            txtMa.Text = objnpp.MaNPP.ToString();
            txtTen.Text = objnpp.TenNPP.ToString();
            txtDiaChi.Text = objnpp.DiaChi.ToString();
            txtEmail.Text = objnpp.Email.ToString();
            txtDienThoai.Text = objnpp.SoDT.ToString();
            txtTen.Focus();
            txtTen.Enabled = true;
            txtDiaChi.Enabled = true;
            txtEmail.Enabled = true;
            txtDienThoai.Enabled = true;
            btnHuy.Enabled = true;
            btnLuu.Text = "Sửa";
        }

        private void Disable()
        {
            txtTen.Enabled = false;
            txtDiaChi.Enabled = false;
            txtEmail.Enabled = false;
            txtDienThoai.Enabled = false;

            txtMa.Text = "";
            txtTen.Text = "";
            txtDiaChi.Text = "";
            txtEmail.Text = "";
            txtDienThoai.Text = "";

            btnHuy.Enabled = false;
        }

        private void Enable()
        {
            txtTen.Enabled = true;
            txtDiaChi.Enabled = true;
            txtEmail.Enabled = true;
            txtDienThoai.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Disable();
            Enable();
            txtMa.Text = "";
            txtTen.Text = "";
            txtDiaChi.Text = "";
            txtEmail.Text = "";
            txtDienThoai.Text = "";
            txtTen.Focus();
            btnLuu.Text = "Thêm";
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Disable();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            ServiceNhaPhanPhoi.NhaPhanPhoi objnpp = (ServiceNhaPhanPhoi.NhaPhanPhoi)dataGridView1.SelectedRows[0].DataBoundItem;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?!?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (obj.DeleteNhaPhanPhoi(objnpp) == 1)
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
                        ServiceNhaPhanPhoi.NhaPhanPhoi objnpp = new ServiceNhaPhanPhoi.NhaPhanPhoi();
                        objnpp.TenNPP = txtTen.Text;
                        objnpp.DiaChi = txtDiaChi.Text;
                        objnpp.Email = txtEmail.Text;
                        objnpp.SoDT = txtDienThoai.Text;
                        try
                        {
                            if (obj.InsertNhaPhanPhoi(objnpp) == 1)
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
                        ServiceNhaPhanPhoi.NhaPhanPhoi objnpp = new ServiceNhaPhanPhoi.NhaPhanPhoi();
                        objnpp.MaNPP = int.Parse(txtMa.Text.ToString());
                        objnpp.TenNPP = txtTen.Text;
                        objnpp.DiaChi = txtDiaChi.Text;
                        objnpp.Email = txtEmail.Text;
                        objnpp.SoDT = txtDienThoai.Text;
                        try
                        {
                            if (obj.UpdateNhaPhanPhoi(objnpp) == 1)
                            {
                                MessageBox.Show("Sửa thành công!");
                                txtMa.Text = "";
                                load();
                            }
                            else MessageBox.Show("Có lỗi xảy ra. Vui lòng thao tác lại!1111");
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
