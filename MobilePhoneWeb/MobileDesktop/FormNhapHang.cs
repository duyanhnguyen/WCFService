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
    public partial class FormNhapHang : Form
    {
        ServicePhieuNhap.ServicePhieuNhapClient obj = new ServicePhieuNhap.ServicePhieuNhapClient();
        
        public FormNhapHang()
        {
            InitializeComponent();
            load();
            btnChiTiet.Enabled = false;
        }

        public void load()
        {
            dataGridView1.DataSource = obj.SelectPhieuNhap();
            dataGridView1.Columns["NhanVien"].Visible = false;
            dataGridView1.Columns["NhaPhanPhoi"].Visible = false;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            FormQuanLy form = new FormQuanLy();
            this.Visible = false;
            form.Visible = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            FormChiTietPhieuNhap formchitiet = new FormChiTietPhieuNhap();
            this.Visible = false;
            formchitiet.Visible = true;
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ServicePhieuNhap.PhieuNhap objpn = (ServicePhieuNhap.PhieuNhap)dataGridView1.SelectedRows[0].DataBoundItem;
            btnChiTiet.Enabled = true;
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            ServicePhieuNhap.PhieuNhap phieunhap = (ServicePhieuNhap.PhieuNhap)dataGridView1.SelectedRows[0].DataBoundItem;
            FormChiTietPhieuNhap formchitiet = new FormChiTietPhieuNhap(phieunhap);
            this.Visible = false;
            formchitiet.Visible = true;
        }
    }
}
