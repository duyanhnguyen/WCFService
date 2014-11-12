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
    public partial class FormDangNhap : Form
    {
        ServiceNhanVien.ServiceNhanVienClient nv = new ServiceNhanVien.ServiceNhanVienClient();

        public FormDangNhap()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public static string strtendn, strmatkhaudn;

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            strtendn = txtUsername.Text;
            strmatkhaudn = txtPassword.Text;

            if (nv.Login(strtendn, strmatkhaudn))
            {
                Program.username = strtendn;
                MessageBox.Show("Bạn đã đăng nhập thành công vào hệ thống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormQuanLy form = new FormQuanLy();
                this.Visible = false;
                form.Visible = true;
            }
            else
            {
                MessageBox.Show("Bạn đã nhập không đúng dữ liệu, hãy chắc chắn tên sử dụng và mật khẩu là chính xác", "Đăng nhập");
            }
        }
    }
}
