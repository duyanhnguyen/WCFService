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
    public partial class FormMenuThongKe : Form
    {
        public FormMenuThongKe()
        {
            InitializeComponent();
        }

        private void btnQuaylai_Click(object sender, EventArgs e)
        {
            FormQuanLy frm = new FormQuanLy();
            this.Visible = false;
            frm.Visible = true;
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            FormThongKeSPThang frm = new FormThongKeSPThang();
            this.Visible = false;
            frm.Visible = true;
        }

        private void btnNhaSanXuat_Click(object sender, EventArgs e)
        {
            FormThongKeNVThang frm = new FormThongKeNVThang();
            this.Visible = false;
            frm.Visible = true;
        }

        private void btnTKSPNhapThang_Click(object sender, EventArgs e)
        {
            FormThongKeSPNhap frm = new FormThongKeSPNhap();
            this.Visible = false;
            frm.Visible = true;
        }
    }
}
