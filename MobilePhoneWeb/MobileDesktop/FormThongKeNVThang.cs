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
    public partial class FormThongKeNVThang : Form
    {
        ServiceNhanVien.ServiceNhanVienClient obj = new ServiceNhanVien.ServiceNhanVienClient();
        ServiceDonHang.ServiceDonHangClient dh = new ServiceDonHang.ServiceDonHangClient();
        public FormThongKeNVThang()
        {
            InitializeComponent();
            showdata();
//            loadproduct();
        }

        public void showdata()
        {
//            DateTime dt = DateTime.Now;
//            txtThang.Text = dt.ToShortDateString();
            txtMa.Text = "0";
            comboBox1.Text = "01";
            comboBox2.Text = "2014";
            txtThang.Text = comboBox1.Text + "/" + comboBox2.Text;
            comboBox2.Items.Clear();
            for (int i = 1990; i <= DateTime.Now.Year; i++ )
            {
                comboBox2.Items.Add(i);
            }
                dataGridView1.DataSource = obj.SelectNhanVien();
            //dataGridView1.Columns["Username"].Visible = false;
            //dataGridView1.Columns["Password"].Visible = false;
            //dataGridView1.Columns["Email"].Visible = false;
//            dataGridView1.Columns["Quyen"].Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ServiceNhanVien.NhanVien nv = (ServiceNhanVien.NhanVien)dataGridView1.SelectedRows[0].DataBoundItem;
            txtMa.Text = nv.MaNV.ToString();
            txtTen.Text = nv.TenNV;  
        }

        public void loadproduct()
        {
//            DateTime Thag = DateTime.Parse(txtThang.Text);
//            String _thg = Thag.ToShortDateString();
//            string _thg = txtThang.Text;
            int thang = int.Parse(comboBox1.Text);
            int nam = int.Parse(comboBox2.Text);
            int manv = int.Parse(txtMa.Text);
            //string id = txtMa.Text;
            //int manv = Int32.Parse(id);          
            dataGridView2.DataSource = dh.ThongkeNVThang(manv, thang, nam);
        }

        private void txtMa_TextChanged(object sender, EventArgs e)
        {
            if(txtMa.Text!= null)
            {
                loadproduct();
            }
        }

        private void txtThang_TextChanged(object sender, EventArgs e)
        {
            if(txtThang.Text!=null)
            {
                loadproduct();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtThang.Text = comboBox1.Text + "/" +  comboBox2.Text;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtThang.Text = comboBox1.Text + "/" + comboBox2.Text;
        }


        //---Báo cáo-----------------------------------------------------------------------------------------------------------------
        public void Export(DataTable dt, String sheetName, String title, string nv)
        {
            //Tạo các đối tượng Excel
            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks oBooks;
            Microsoft.Office.Interop.Excel.Sheets oSheets;
            Microsoft.Office.Interop.Excel.Workbook oBook;
            Microsoft.Office.Interop.Excel.Worksheet oSheet;

            //Tạo mới một Excel WorkBook
            oExcel.Visible = true;
            oExcel.DisplayAlerts = false;
            oExcel.Application.SheetsInNewWorkbook = 1;
            oBooks = oExcel.Workbooks;

            oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));
            oSheets = oBook.Worksheets;
            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);
            oSheet.Name = sheetName;

            // Tạo phần đầu nếu muốn
            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "E1");
            head.MergeCells = true;
            head.Value2 = title;
            head.Font.Bold = true;
            head.Font.Name = "Times New Roman";
            head.Font.Size = "18";
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo tiêu đề cột
            Microsoft.Office.Interop.Excel.Range cl0 = oSheet.get_Range("A2", "A2");
            cl0.Value2 = "Nhân viên: " + nv;
            cl0.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");
            cl1.Value2 = "Đơn giá";
            cl1.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");
            cl2.Value2 = "Mã SP";
            cl2.ColumnWidth = 10.0;

            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");
            cl3.Value2 = "Số lượng";
            cl3.ColumnWidth = 10.0;

            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");
            cl4.Value2 = "Tên SP";
            cl4.ColumnWidth = 30.0;

            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");
            cl5.Value2 = "Thành tiền";
            cl5.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "E3");
            rowHead.Font.Bold = true;

            // Kẻ viền
            rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
            // Thiết lập màu nền
            rowHead.Interior.ColorIndex = 15;
            rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


            // Tạo mảng đối tượng để lưu dữ toàn bồ dữ liệu trong DataTable,
            // vì dữ liệu được được gán vào các Cell trong Excel phải thông qua object thuần.
            object[,] arr = new object[dt.Rows.Count, dt.Columns.Count];

            //Chuyển dữ liệu từ DataTable vào mảng đối tượng
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                DataRow dr = dt.Rows[r];
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    arr[r, c] = dr[c];
                }
            }

//            arr[dt.Rows.Count, dt.Columns.Count - 2] = "Nhân viên" + txtTen.Text; //lb_TongTien.Text
            //Thiết lập vùng điền dữ liệu
            int rowStart = 4;
            int columnStart = 1;

            int rowEnd = rowStart + dt.Rows.Count -1;
            int columnEnd = dt.Columns.Count;

            //định dạng dấu "," ở cột Đơn giá, Thành tiền và Tổng tiền
            Microsoft.Office.Interop.Excel.Range DonGia = oSheet.get_Range("C4", "C" + rowEnd);
            Microsoft.Office.Interop.Excel.Range ThanhTien = oSheet.get_Range("E4", "E" + rowEnd);
            DonGia.NumberFormat = "#,###";
            ThanhTien.NumberFormat = "#,###";


            // Ô bắt đầu điền dữ liệu
            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];
            // Ô kết thúc điền dữ liệu
            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];
            // Lấy về vùng điền dữ liệu
            Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);

            //Điền dữ liệu vào vùng đã thiết lập
            range.Value2 = arr;

            // Kẻ viền
            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
            // Căn giữa cột STT
            Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnStart];
            Microsoft.Office.Interop.Excel.Range c4 = oSheet.get_Range(c1, c3);
            oSheet.get_Range(c3, c4).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

        }
        private DataTable GetDataTableFromDGV(DataGridView dgv)
        {
            var dt = new DataTable();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column.Visible)
                {
                    dt.Columns.Add();
                }
            }

            object[] cellValues = new object[dgv.Columns.Count];
            foreach (DataGridViewRow row in dgv.Rows)
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    cellValues[i] = row.Cells[i].Value;
                }
                dt.Rows.Add(cellValues);
            }

            return dt;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
////                    DataTable dt = new DataTable();
                FormThongKeNVThang excel = new FormThongKeNVThang();
                DataTable dt = GetDataTableFromDGV(dataGridView2);
//                    DataTable dt = GetDataTableFromDGV(dataGridView1);
                    excel.Export(dt, "Bao cao", "BÁO CÁO BÁN HÀNG THÁNG: "+ txtThang.Text, txtTen.Text);
                
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra. Vui lòng thao tác lại!");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            FormMenuThongKe frm = new FormMenuThongKe();
            this.Visible = false;
            frm.Visible = true;
        }

    }
}
