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
    public partial class FormChiTietPhieuNhap : Form
    {
        public string Them_Sua;
        ServicePhieuNhap.PhieuNhap phieunhap;
        ServicePhieuNhap.ServicePhieuNhapClient obj_client_pn = new ServicePhieuNhap.ServicePhieuNhapClient();
        ServiceNhaSanXuat.ServiceNhaSanXuatClient obj_client_nsx = new ServiceNhaSanXuat.ServiceNhaSanXuatClient();
        ServiceSanPham.ServiceSanPhamClient obj_client_sp = new ServiceSanPham.ServiceSanPhamClient();
        ServiceNhaPhanPhoi.ServiceNhaPhanPhoiClient obj_client_npp = new ServiceNhaPhanPhoi.ServiceNhaPhanPhoiClient();
        ServiceNhanVien.ServiceNhanVienClient obj_client_nv = new ServiceNhanVien.ServiceNhanVienClient();

        ServiceNhaSanXuat.NhaSanXuat objnsx = new ServiceNhaSanXuat.NhaSanXuat();

        public FormChiTietPhieuNhap()
        {
            InitializeComponent();
            Them_Sua = "Them";
            LoadChiTietPhieuNhap();
        }

        public FormChiTietPhieuNhap(ServicePhieuNhap.PhieuNhap phieunhap)
        {
            Them_Sua = "Sua";
            InitializeComponent();
            this.phieunhap = phieunhap;
            LoadChiTietPhieuNhap();
        }

        public void LoadComboBox()
        {
            cbxNSX.DataSource = obj_client_nsx.SelectNhaSanXuat();
            cbxNSX.ValueMember = "MaNSX";
            cbxNSX.DisplayMember = "TenNSX";
            cbxNSX.SelectedIndex = 0;

            cbxNPP.DataSource = obj_client_npp.SelectNhaPhanPhoi();
            cbxNPP.ValueMember = "MaNPP";
            cbxNPP.DisplayMember = "TenNPP";

            cbxNhanVien.DataSource = obj_client_nv.SelectNhanVien();
            cbxNhanVien.ValueMember = "MaNV";
            cbxNhanVien.DisplayMember = "TenNV";
        }

        public void LoadChiTietPhieuNhap()
        {
            try
            {
                New();
                LoadComboBox();

                if (Them_Sua == "Sua")
                {
                    txtMaPN.Text = phieunhap.MaPN.ToString();
                    cbxNPP.SelectedValue = phieunhap.MaNPP;
                    dateTimePicker1.Value = (DateTime)phieunhap.Ngay;
                    txtNgay.Text = dateTimePicker1.Value.Day.ToString() + "/" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Year.ToString();
                    cbxNhanVien.SelectedValue = phieunhap.MaNV;

                    //lấy danh sách sản phẩm của đơn hàng từ CSDL lên
                    dataGridView1.DataSource = obj_client_pn.SelectCTPN_ByMaPN(phieunhap.MaPN);
                    //add danh sách sản phẩm của đơn hàng từ dataGridView1 => dataGridViewChiTiet
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        dataGridViewChiTiet.Rows.Add(
                            dataGridView1.Rows[i].Cells["Column9"].Value.ToString(),
                            dataGridView1.Rows[i].Cells["Column10"].Value.ToString(),
                            dataGridView1.Rows[i].Cells["Column11"].Value.ToString(),
                            dataGridView1.Rows[i].Cells["Column12"].Value.ToString()
                            );
                    }

                    TongTien();
                }
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra. Vui lòng thao tác lại!");
            }
        }

        public void showdanhsachsanpham()
        {
            dataGridViewSanPham.DataSource = obj_client_sp.SelectSanPhamTheoNSX(objnsx.MaNSX);
            dataGridViewSanPham.Columns["MaNSX"].Visible = false;
            dataGridViewSanPham.Columns["MoTa"].Visible = false;
            dataGridViewSanPham.Columns["NhaSanXuat"].Visible = false;
            dataGridViewSanPham.Columns["UrlHinh"].Visible = false;
        }

        private void New()
        {
            int NewID = obj_client_pn.GetNewIDPhieuNhap() + 1;
            txtMaPN.Text = NewID.ToString();
            dateTimePicker1.Value = DateTime.Now;
            txtNgay.Text = dateTimePicker1.Value.Day.ToString() + "/" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Year.ToString();
            lblTongTien.Text = "0";

            if (dataGridViewChiTiet.RowCount >= 2)
            {
                int RowCount = dataGridViewChiTiet.RowCount;
                for (int i = RowCount - 2; i >= 0; i--)
                {
                    dataGridViewChiTiet.Rows.RemoveAt(i);
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            FormNhapHang form = new FormNhapHang();
            this.Visible = false;
            form.Visible = true;
        }

        //private void btnLuu_Click(object sender, EventArgs e)
        //{
        //    if (txtMaSP.Text != "")
        //    {
        //        try
        //        {
        //            if (txtMaPN.Text == "") //insert
        //            {
        //                ServicePhieuNhap.CT_PhieuNhap objctpn = new ServicePhieuNhap.CT_PhieuNhap();

        //                objctpn.MaPN = int.Parse(txtMaPN.Text.ToString());
        //                objctpn.MaSP = int.Parse(txtMaSP.Text);
        //                objctpn.SoLuong = int.Parse(txtSoLuong.Text);
        //                objctpn.Gia = int.Parse(txtGia.Text);
        //                try
        //                {
        //                    if (obj.InsertCTPN(objctpn) == 1)
        //                    {
        //                        MessageBox.Show("Thêm thành công!");
        //                        txtMaPN.Text = "";
        //                        load();
        //                    }
        //                    else MessageBox.Show("Có lỗi xảy ra. Vui lòng thao tác lại!");
        //                }
        //                catch
        //                {
        //                    MessageBox.Show("Có lỗi xảy ra. Vui lòng thao tác lại!");
        //                }
        //            }
        //            else //update
        //            {
        //                ServicePhieuNhap.CT_PhieuNhap objctpn = new ServicePhieuNhap.CT_PhieuNhap();

        //                objctpn.MaPN = int.Parse(txtMaPN.Text.ToString());
        //                objctpn.MaSP = int.Parse(txtMaSP.Text);
        //                objctpn.SoLuong = int.Parse(txtSoLuong.Text);
        //                objctpn.Gia = int.Parse(txtGia.Text);
        //                try
        //                {
        //                    if (obj.UpdateCTPN(objctpn) == 1)
        //                    {
        //                        MessageBox.Show("Sửa thành công!");
        //                        txtMaPN.Text = "";
        //                        load();
        //                    }
        //                    else MessageBox.Show("Có lỗi xảy ra. Vui lòng thao tác lại!");
        //                }
        //                catch
        //                {
        //                    MessageBox.Show("Có lỗi xảy ra. Vui lòng thao tác lại!");
        //                }
        //            }
        //        }
        //        catch
        //        {
        //            MessageBox.Show("Có lỗi xảy ra. Vui lòng thao tác lại!");
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Chưa hoàn tất thông tin!");
        //    }
        //}

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TongTien()
        {
            try
            {
                int sum = 0;
                for (int i = 0; i < dataGridViewChiTiet.RowCount - 1; i++)
                {
                    sum = sum + int.Parse(dataGridViewChiTiet.Rows[i].Cells["Column3"].Value.ToString()) * int.Parse(dataGridViewChiTiet.Rows[i].Cells["Column4"].Value.ToString());
                }
                lblTongTien.Text = sum.ToString();
            }
            catch
            {
                lblTongTien.Text = "";
            }
        } 

        private void dataGridViewChiTiet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            TongTien();
        }

        private void cbxNSX_SelectedValueChanged(object sender, EventArgs e)
        {
            objnsx = (ServiceNhaSanXuat.NhaSanXuat)cbxNSX.SelectedItem;
            showdanhsachsanpham();
        }

        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            try
            {
                int check = 0;
                for (int i = 0; i < dataGridViewChiTiet.RowCount - 1; i++)
                {
                    if (dataGridViewSanPham.SelectedRows[0].Cells["Column5"].Value.ToString() == dataGridViewChiTiet.Rows[i].Cells["Column1"].Value.ToString())
                    {
                        check = 1;
                        break;
                    }
                }
                if (check == 1)
                {
                    MessageBox.Show("Đã có sản phẩm này!");
                }
                else
                {
                    dataGridViewChiTiet.Rows.Add(
                            dataGridViewSanPham.SelectedRows[0].Cells["Column5"].Value.ToString(),
                            dataGridViewSanPham.SelectedRows[0].Cells["Column6"].Value.ToString(),
                            "1",
                            dataGridViewSanPham.SelectedRows[0].Cells["Column8"].Value.ToString());
                    TongTien();
                }
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra. Vui lòng thao tác lại!");
            }
        }

        private void btnXoaSanPham_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewChiTiet.Rows.Remove(dataGridViewChiTiet.SelectedRows[0]);
                TongTien();
            }
            catch
            {
                //nếu danh sách đã rỗng hoặc chọn dòng cuối
                //dòng cuối là dòng rỗng, không có dữ liệu
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            txtNgay.Text = dateTimePicker1.Value.Day.ToString() + "/" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Year.ToString();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dataGridViewChiTiet.RowCount >= 2)
            {
                try
                {
                    ServicePhieuNhap.PhieuNhap pn = new ServicePhieuNhap.PhieuNhap();
                    if (Them_Sua == "Sua")
                        pn.MaPN = int.Parse(txtMaPN.Text.ToString());
                    pn.MaNPP = (int)cbxNPP.SelectedValue;
                    pn.Ngay = DateTime.Parse(txtNgay.Text);
                    pn.MaNV = (int)cbxNhanVien.SelectedValue;
                    pn.Trigia = int.Parse(lblTongTien.Text.ToString());

                    if (Them_Sua == "Them")
                        obj_client_pn.InsertPhieuNhap(pn);
                    else
                    {
                        obj_client_pn.UpdatePhieuNhap(pn);
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            TruSoLuong(int.Parse(dataGridView1.Rows[i].Cells["Column9"].Value.ToString()), int.Parse(dataGridView1.Rows[i].Cells["Column11"].Value.ToString()));
                        }
                        obj_client_pn.DeleteCTPN(int.Parse(txtMaPN.Text));
                    }

                    for (int i = 0; i < dataGridViewChiTiet.RowCount - 1; i++)
                    {
                        ServicePhieuNhap.CT_PhieuNhap ctpn = new ServicePhieuNhap.CT_PhieuNhap();
                        ctpn.MaPN = int.Parse(txtMaPN.Text.ToString());
                        ctpn.MaSP = int.Parse(dataGridViewChiTiet.Rows[i].Cells["Column1"].Value.ToString());
                        ctpn.SoLuong = int.Parse(dataGridViewChiTiet.Rows[i].Cells["Column3"].Value.ToString());
                        ctpn.Gia = int.Parse(dataGridViewChiTiet.Rows[i].Cells["Column4"].Value.ToString());

                        obj_client_pn.InsertCTPN(ctpn);
                        CongSoLuong(int.Parse(ctpn.MaSP.ToString()), int.Parse(ctpn.SoLuong.ToString()));
                    }
                    MessageBox.Show("Lưu thành công!");
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Có lỗi xảy ra. Vui lòng thao tác lại!");
                }
            }
            else MessageBox.Show("Chưa có sản phẩm nào được chọn!");
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            LoadChiTietPhieuNhap();
        }

        public void CongSoLuong(int masp, int soluongcong)
        {
            dataGridView2.DataSource = obj_client_sp.SelectSanPhamByID(masp);
            ServiceSanPham.SanPham sp = (ServiceSanPham.SanPham)dataGridView2.Rows[0].DataBoundItem;

            obj_client_pn.UpdateSoLuongSanPham(masp, sp.SoLuong + soluongcong);
        }

        public void TruSoLuong(int masp, int soluongtru)
        {
            dataGridView2.DataSource = obj_client_sp.SelectSanPhamByID(masp);
            ServiceSanPham.SanPham sp = (ServiceSanPham.SanPham)dataGridView2.Rows[0].DataBoundItem;

            obj_client_pn.UpdateSoLuongSanPham(masp, sp.SoLuong - soluongtru);
        }
    }
}
