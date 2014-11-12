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
    public partial class FormDonHang : Form
    {
        static int maDH;
        //static int maKH;
        //static int maNV;

        ServiceDonHang.ServiceDonHangClient dh = new ServiceDonHang.ServiceDonHangClient();
        ServiceNhanVien.ServiceNhanVienClient nv = new ServiceNhanVien.ServiceNhanVienClient();

        public FormDonHang()
        {
            InitializeComponent();
            showdata();
        }

        public void showdata()
        {
            dtgDonHang.DataSource = dh.SelectDonHangKhachHang();

            //txtNgay.Text = Program.username;
        }

        private void dtgDonHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ServiceDonHang.DonHangKhachHang objdh = (ServiceDonHang.DonHangKhachHang)dtgDonHang.SelectedRows[0].DataBoundItem;

            dataGridViewChiTiet.DataSource = dh.SelectCT_DonHangByDonHang(objdh.MaDH);
            //txtMadh.Text = objdh.MaDH.ToString();
            DateTime dt = DateTime.Parse(objdh.Ngay.ToString());
            txtNgay.Text = dt.ToShortDateString();
            txtHoten.Text = objdh.HoTen.ToString();
            txtSdt.Text = objdh.DienThoai.ToString();
            txtDiachi.Text = objdh.DiaChi.ToString();
            txtTongtien.Text = objdh.Trigia.ToString();
            maDH = objdh.MaDH;
            //maKH = dh.SelectMakhByMadh(objdh.MaDH);

            if (objdh.Tinhtrang == "Chưa giao")
            {
                cboTinhtrang.SelectedIndex = 0;
            }
            if (objdh.Tinhtrang == "Đã giao")
            {
                cboTinhtrang.SelectedIndex = 1;
            }
            if (objdh.Tinhtrang == "Đã hủy")
            {
                cboTinhtrang.SelectedIndex = 2;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int manv = nv.SelectIdByUsername(Program.username);
            ServiceDonHang.DonHang objdh = new ServiceDonHang.DonHang();
            objdh.MaDH = maDH;
            objdh.TinhTrang = (int)cboTinhtrang.SelectedIndex;
            objdh.MaNV = manv;

            if (dh.UpdateTinhTrangDonHang(objdh) == 1)
            {
                if (cboTinhtrang.SelectedIndex == 1)
                {
                    dh.UpdateSoLuongSanPham(objdh);
                }
                MessageBox.Show("Cập nhật thành công!");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!");
            }
            showdata();
        }

        private void btn_Quaylai_Click(object sender, EventArgs e)
        {
            FormQuanLy frm = new FormQuanLy();
            this.Visible = false;
            frm.Visible = true;
        }
    }
}
