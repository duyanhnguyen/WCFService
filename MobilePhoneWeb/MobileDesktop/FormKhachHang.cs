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
    public partial class FormKhachHang : Form
    {
        ServiceKhachHang.ServiceKhachHangClient obj = new ServiceKhachHang.ServiceKhachHangClient();
        public FormKhachHang()
        {
            InitializeComponent();
            load();
        }

        public void load()
        {
            dataGridView1.DataSource = obj.SelectKhachHang();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            FormQuanLy form = new FormQuanLy();
            this.Visible = false;
            form.Visible = true;
        }

        public void Disable()
        {
            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            txtEmail.Enabled = false;
            txtHoten.Enabled = false;
            cbxGioitinh.Enabled = false; 
            dateTimePicker1.Enabled = false;
            txtDiachi.Enabled = false;
            txtDienthoai.Enabled = false;


            txtMakh.Text = "";
            txtHoten.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtEmail.Text = "";
            cbxGioitinh.SelectedIndex = 0;
            txtNgaysinh.Text = "";
            txtDiachi.Text = "";
            txtDienthoai.Text = "";
            

            btnHuy.Enabled = false;
        }

        public void Enable()
        {
            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            txtEmail.Enabled = true;
            txtHoten.Enabled = true;
            cbxGioitinh.Enabled = true;
            dateTimePicker1.Enabled = true;
            txtDiachi.Enabled = true;
            txtDienthoai.Enabled = true;

            btnHuy.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Disable();
            Enable();
            txtMakh.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtEmail.Text = "";
            txtHoten.Text = "";
            txtNgaysinh.Text = "";
            txtDiachi.Text = "";
            txtDienthoai.Text = "";
            cbxGioitinh.SelectedIndex = 0;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            ServiceKhachHang.KhachHang objkh = (ServiceKhachHang.KhachHang)dataGridView1.SelectedRows[0].DataBoundItem;
            objkh.MaKH = int.Parse(txtMakh.Text);

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?!?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (obj.DeleteKhachHang(objkh) == 1)
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
            ServiceKhachHang.KhachHang objkh = (ServiceKhachHang.KhachHang)dataGridView1.SelectedRows[0].DataBoundItem;

            txtMakh.Text = objkh.MaKH.ToString();
            txtUsername.Text = objkh.Username.ToString();
            txtPassword.Text = objkh.Password.ToString();
            txtEmail.Text = objkh.Email.ToString();
            txtHoten.Text = objkh.HoTen.ToString();
            DateTime dt = DateTime.Parse(objkh.NgaySinh.ToString());
            txtNgaysinh.Text = dt.ToShortDateString();
            txtDiachi.Text = objkh.DiaChi.ToString();
            txtDienthoai.Text = objkh.DienThoai.ToString();
             
            if (objkh.GioiTinh == "Nam")
            {
                cbxGioitinh.SelectedIndex = 0;
            }
            else
            {
                cbxGioitinh.SelectedIndex = 1;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            txtNgaysinh.Text = dateTimePicker1.Value.Day.ToString() + "/" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Year.ToString();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtHoten.Text != "")
            {
                try
                {
                    if (txtMakh.Text == "") //insert
                    {
                        ServiceKhachHang.KhachHang objkh = new ServiceKhachHang.KhachHang();
                        objkh.Username = txtUsername.Text;
                        objkh.Password = txtPassword.Text;
                        objkh.Email = txtEmail.Text;
                        objkh.HoTen = txtHoten.Text;
                        objkh.GioiTinh = cbxGioitinh.Text;
                        objkh.NgaySinh = DateTime.Parse(txtNgaysinh.Text);
                        objkh.DiaChi = txtDiachi.Text;
                        objkh.DienThoai = txtDienthoai.Text;
                        try
                        {
                            if (obj.InsertKhachHang(objkh) == 1)
                            {
                                MessageBox.Show("Thêm thành công!");
                                txtHoten.Text = "";
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
                        ServiceKhachHang.KhachHang objkh = new ServiceKhachHang.KhachHang();
                        objkh.MaKH = int.Parse(txtMakh.Text);
                        objkh.Username = txtUsername.Text;
                        objkh.Password = txtPassword.Text;
                        objkh.Email = txtEmail.Text;
                        objkh.HoTen = txtHoten.Text;
                        objkh.GioiTinh = cbxGioitinh.Text;
                        objkh.NgaySinh = DateTime.Parse(txtNgaysinh.Text);
                        objkh.DiaChi = txtDiachi.Text;
                        objkh.DienThoai = txtDienthoai.Text;
                        try
                        {
                            if (obj.UpdateKhachHang(objkh) == 1)
                            {
                                MessageBox.Show("Sửa thành công!");
                                txtMakh.Text = "";
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

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
