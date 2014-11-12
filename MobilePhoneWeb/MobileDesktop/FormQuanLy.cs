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
    public partial class FormQuanLy : Form
    {
        public FormQuanLy()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            FormSanPham form = new FormSanPham();
            this.Visible = false;
            form.Visible = true;
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            FormNhanVien form = new FormNhanVien();
            this.Visible = false;
            form.Visible = true;
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            FormKhachHang form = new FormKhachHang();
            this.Visible = false;
            form.Visible = true;
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            FormDangNhap form = new FormDangNhap();
            this.Visible = false;
            form.Visible = true;
        }

        private void btnNhaSanXuat_Click(object sender, EventArgs e)
        {
            FormNhaSanXuat form = new FormNhaSanXuat();
            this.Visible = false;
            form.Visible = true;
        }

        private void btnNhaPhanPhoi_Click(object sender, EventArgs e)
        {
            FormNhaPhanPhoi form = new FormNhaPhanPhoi();
            this.Visible = false;
            form.Visible = true;
        }

        private void btnDonHang_Click(object sender, EventArgs e)
        {
            FormDonHang form = new FormDonHang();
            this.Visible = false;
            form.Visible = true;
        }

        private void btnQuyenNhanVien_Click(object sender, EventArgs e)
        {
            FormQuyenNhanVien form = new FormQuyenNhanVien();
            this.Visible = false;
            form.Visible = true;
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            FormMenuThongKe form = new FormMenuThongKe();
            this.Visible = false;
            form.Visible = true;
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            FormNhapHang frm = new FormNhapHang();
            this.Visible = false;
            frm.Visible = true;
        }
    }
}
