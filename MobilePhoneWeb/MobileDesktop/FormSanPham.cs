using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobileDesktop
{
    public partial class FormSanPham : Form
    {
        ServiceSanPham.ServiceSanPhamClient obj = new ServiceSanPham.ServiceSanPhamClient();
        ServiceNhaSanXuat.ServiceNhaSanXuatClient obj2 = new ServiceNhaSanXuat.ServiceNhaSanXuatClient();

        ServiceNhaSanXuat.NhaSanXuat objnsx = new ServiceNhaSanXuat.NhaSanXuat();
        
        public FormSanPham()
        {
            InitializeComponent();
            LoadNhaSanXuat();
        }

        public void showdata()
        {
            dataGridView1.DataSource = obj.SelectSanPhamTheoNSX(objnsx.MaNSX);
            dataGridView1.Columns["MaNSX"].Visible = false;
            dataGridView1.Columns["NhaSanXuat"].Visible = false;
        }

        public void LoadNhaSanXuat()
        {
            cbxLoadNSX.DataSource = obj2.SelectNhaSanXuat();
            cbxLoadNSX.DisplayMember = "TenNSX";
            cbxLoadNSX.ValueMember = "MaNSX";
            cbxLoadNSX.SelectedIndex = 0;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            FormQuanLy form = new FormQuanLy();
            this.Visible = false;
            form.Visible = true;
        }

        public void Disable()
        {
            txtTensp.Enabled = false;
            txtGia.Enabled = false;
            txtSoluong.Enabled = false;
            txtMota.Enabled = false;
            cbxNhasanxuat.Enabled = false;
            txtHinh.Enabled = false;

            txtMasp.Text = "";
            txtTensp.Text = "";
            txtGia.Text = "";
            txtSoluong.Text = "";
            txtMota.Text = "";
            cbxNhasanxuat.Text = "";
            txtHinh.Text = "";

            btnChonHinh.Enabled = false;
            btnHuy.Enabled = false;

            pictureBox1.Visible = false;
            checkBoxHinh.Visible = false;
            checkBoxHinh.Checked = false;
        }

        public void Enable()
        {
            txtTensp.Enabled = true;
            txtGia.Enabled = true;
            txtSoluong.Enabled = true;
            txtMota.Enabled = true;
            cbxNhasanxuat.Enabled = true;

            btnHuy.Enabled = true;
        }

        private string NoiLuuHinhAnh = @"..\..\..\..\MobilePhoneWeb\WebMobile\Images\Product\";

        private string NoiChonHinhAnh = "";

        private void loadHinh(String hinh)
        {
            pictureBox1.Visible = true;
            //nếu cột hinh_anh rỗng thì load hình no-img.gif
            if (hinh == "")
            {
                hinh = "no-img.gif";
            }
            //kiểm tra thư mục có hình đó hay không
            //không thì load no-img.gif
            //có thì load hình đó lên
            else
            {
                String[] ListFiles = Directory.GetFiles(NoiLuuHinhAnh, hinh);
                if (ListFiles.Length == 0)
                {
                    hinh = "no-img.gif";
                }
            }
            pictureBox1.ImageLocation = NoiLuuHinhAnh + hinh;

        }
         
        private void cbxLoadNSX_SelectedIndexChanged(object sender, EventArgs e)
        {
            objnsx = (ServiceNhaSanXuat.NhaSanXuat)cbxLoadNSX.SelectedItem;

            showdata();
            cbxNhasanxuat.DataSource = obj2.SelectNhaSanXuat();
            cbxNhasanxuat.ValueMember = "MaNSX";
            cbxNhasanxuat.DisplayMember = "TenNSX";
            cbxNhasanxuat.SelectedValue = objnsx.MaNSX;
            Disable();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ServiceSanPham.SanPham objsp = (ServiceSanPham.SanPham)dataGridView1.SelectedRows[0].DataBoundItem;
            Disable();
            Enable();
            txtMasp.Text = objsp.MaSP.ToString();
            txtTensp.Text = objsp.TenSP;
            txtHinh.Text = objsp.UrlHinh;
            txtGia.Text = objsp.Gia.ToString();
            txtMota.Text = objsp.MoTa;
            txtSoluong.Text = objsp.SoLuong.ToString();

            loadHinh(objsp.UrlHinh);

            cbxNhasanxuat.SelectedValue = objnsx.MaNSX;
            btnLuu.Text = "Sửa";
            checkBoxHinh.Visible = true;
            
        }

        private void checkBoxHinh_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHinh.Checked == true)
            {
                btnChonHinh.Enabled = true;
                txtHinh.Enabled = true;
            }
            else
            {
                btnChonHinh.Enabled = false;
                txtHinh.Enabled = false;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Disable();
            Enable();
            txtMasp.Text = "";
            txtTensp.Text = "";
            txtTensp.Focus();
            txtGia.Text = "";
            txtHinh.Text = "";
            txtSoluong.Text = "";
            txtMota.Text = "";
            btnLuu.Text = "Thêm";
            cbxNhasanxuat.SelectedValue = objnsx.MaNSX;

            btnChonHinh.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            ServiceSanPham.SanPham objsp = (ServiceSanPham.SanPham)dataGridView1.SelectedRows[0].DataBoundItem;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?!?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (obj.DeleteSanPham(objsp) == 1)
                {
                    MessageBox.Show("Xóa thành công!");
                    showdata();
                }
                else MessageBox.Show("Còn dữ liệu ràng buộc. Không thể xóa!");
            }
        }

        private void btnChonHinh_Click(object sender, EventArgs e)
        {
            OpenFileDialog op1 = new OpenFileDialog();
            op1.Filter = "Images |*.jpg;*.jpeg;*.png;*.gif";
            op1.ShowDialog();
            txtHinh.Text = op1.SafeFileName;
            NoiChonHinhAnh = op1.FileName;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            ServiceNhaSanXuat.NhaSanXuat nsx_old = (ServiceNhaSanXuat.NhaSanXuat)cbxLoadNSX.SelectedItem;
            int nsxold = nsx_old.MaNSX;

            ServiceNhaSanXuat.NhaSanXuat nsx_new = (ServiceNhaSanXuat.NhaSanXuat)cbxNhasanxuat.SelectedItem;
            int nsxnew = nsx_new.MaNSX;

            if (txtTensp.Text != "" && txtGia.Text != "" && txtSoluong.Text != "" && txtMota.Text != "")
            {
                try
                {
                    if (txtMasp.Text == "") //insert
                    {
                        ServiceSanPham.SanPham inserted = new ServiceSanPham.SanPham();
                        inserted.TenSP = txtTensp.Text;
                        inserted.Gia = int.Parse(txtGia.Text.ToString());
                        inserted.SoLuong = int.Parse(txtSoluong.Text.ToString());
                        inserted.MoTa = txtMota.Text;
                        inserted.MaNSX = nsxnew;
                        inserted.UrlHinh = txtHinh.Text;
                        try
                        {
                            File.Delete(NoiLuuHinhAnh + txtHinh.Text);
                            File.Copy(NoiChonHinhAnh, NoiLuuHinhAnh + txtHinh.Text);

                            if (obj.InsertSanPham(inserted) == 1)
                            {
                                MessageBox.Show("Thêm thành công!");
                                Disable();
                                txtMasp.Text = "";
                                showdata();
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
                        ServiceSanPham.SanPham updated = new ServiceSanPham.SanPham();
                        updated.MaSP = int.Parse(txtMasp.Text.ToString());
                        updated.TenSP = txtTensp.Text;
                        updated.Gia = int.Parse(txtGia.Text.ToString());
                        updated.SoLuong = int.Parse(txtSoluong.Text.ToString());
                        updated.MoTa = txtMota.Text;
                        updated.MaNSX = nsxnew;
                        updated.UrlHinh = txtHinh.Text;
                        if (checkBoxHinh.Checked == true) //sửa sử dụng hình mới
                        {
                            if (txtHinh.Text != "")
                            {
                                try
                                {
                                    File.Delete(NoiLuuHinhAnh + txtHinh.Text);
                                    File.Copy(NoiChonHinhAnh, NoiLuuHinhAnh + txtHinh.Text);

                                    if (obj.UpdateSanPham(updated) == 1)
                                    {
                                        MessageBox.Show("Sửa thành công!");
                                        Disable();
                                        txtMasp.Text = "";
                                        showdata();
                                    }
                                    else MessageBox.Show("Có lỗi xảy ra. Vui lòng thao tác lại!");
                                }
                                catch
                                {
                                    MessageBox.Show("Có lỗi xảy ra. Vui lòng thao tác lại!");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Chưa chọn hình!");
                            }
                        }
                        else //sửa không sử dụng hình mới
                        {
                            try
                            {
                                if (obj.UpdateSanPham(updated) == 1)
                                {
                                    MessageBox.Show("Sửa thành công!");
                                    Disable();
                                    txtMasp.Text = "";
                                    showdata();
                                }
                                else MessageBox.Show("Có lỗi xảy ra. Vui lòng thao tác lại!");
                            }
                            catch
                            {
                                MessageBox.Show("Có lỗi xảy ra. Vui lòng thao tác lại!");
                            }
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
