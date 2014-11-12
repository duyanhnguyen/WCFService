namespace MobileDesktop
{
    partial class FormMenuThongKe
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnNhaSanXuat = new System.Windows.Forms.Button();
            this.btnKhachHang = new System.Windows.Forms.Button();
            this.btnDonHang = new System.Windows.Forms.Button();
            this.btnSanPham = new System.Windows.Forms.Button();
            this.btnQuaylai = new System.Windows.Forms.Button();
            this.btnTKSPNhapThang = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnNhaSanXuat
            // 
            this.btnNhaSanXuat.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNhaSanXuat.Location = new System.Drawing.Point(12, 47);
            this.btnNhaSanXuat.Name = "btnNhaSanXuat";
            this.btnNhaSanXuat.Size = new System.Drawing.Size(236, 28);
            this.btnNhaSanXuat.TabIndex = 14;
            this.btnNhaSanXuat.Text = "Thống kê SP NV Tháng";
            this.btnNhaSanXuat.UseVisualStyleBackColor = true;
            this.btnNhaSanXuat.Click += new System.EventHandler(this.btnNhaSanXuat_Click);
            // 
            // btnKhachHang
            // 
            this.btnKhachHang.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKhachHang.Location = new System.Drawing.Point(12, 13);
            this.btnKhachHang.Name = "btnKhachHang";
            this.btnKhachHang.Size = new System.Drawing.Size(236, 28);
            this.btnKhachHang.TabIndex = 12;
            this.btnKhachHang.Text = "Thống kê SP theo tháng";
            this.btnKhachHang.UseVisualStyleBackColor = true;
            this.btnKhachHang.Click += new System.EventHandler(this.btnKhachHang_Click);
            // 
            // btnDonHang
            // 
            this.btnDonHang.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDonHang.Location = new System.Drawing.Point(90, -52);
            this.btnDonHang.Name = "btnDonHang";
            this.btnDonHang.Size = new System.Drawing.Size(115, 50);
            this.btnDonHang.TabIndex = 11;
            this.btnDonHang.Text = "Đơn hàng";
            this.btnDonHang.UseVisualStyleBackColor = true;
            // 
            // btnSanPham
            // 
            this.btnSanPham.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSanPham.Location = new System.Drawing.Point(-84, -52);
            this.btnSanPham.Name = "btnSanPham";
            this.btnSanPham.Size = new System.Drawing.Size(115, 50);
            this.btnSanPham.TabIndex = 9;
            this.btnSanPham.Text = "Sản phẩm";
            this.btnSanPham.UseVisualStyleBackColor = true;
            // 
            // btnQuaylai
            // 
            this.btnQuaylai.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuaylai.Location = new System.Drawing.Point(133, 115);
            this.btnQuaylai.Name = "btnQuaylai";
            this.btnQuaylai.Size = new System.Drawing.Size(115, 29);
            this.btnQuaylai.TabIndex = 15;
            this.btnQuaylai.Text = "Quay lại";
            this.btnQuaylai.UseVisualStyleBackColor = true;
            this.btnQuaylai.Click += new System.EventHandler(this.btnQuaylai_Click);
            // 
            // btnTKSPNhapThang
            // 
            this.btnTKSPNhapThang.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTKSPNhapThang.Location = new System.Drawing.Point(12, 81);
            this.btnTKSPNhapThang.Name = "btnTKSPNhapThang";
            this.btnTKSPNhapThang.Size = new System.Drawing.Size(236, 28);
            this.btnTKSPNhapThang.TabIndex = 16;
            this.btnTKSPNhapThang.Text = "Thống kê SP nhập tháng";
            this.btnTKSPNhapThang.UseVisualStyleBackColor = true;
            this.btnTKSPNhapThang.Click += new System.EventHandler(this.btnTKSPNhapThang_Click);
            // 
            // FormMenuThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 159);
            this.Controls.Add(this.btnTKSPNhapThang);
            this.Controls.Add(this.btnQuaylai);
            this.Controls.Add(this.btnNhaSanXuat);
            this.Controls.Add(this.btnKhachHang);
            this.Controls.Add(this.btnDonHang);
            this.Controls.Add(this.btnSanPham);
            this.Name = "FormMenuThongKe";
            this.Text = "Thống kê - Báo cáo";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNhaSanXuat;
        private System.Windows.Forms.Button btnKhachHang;
        private System.Windows.Forms.Button btnDonHang;
        private System.Windows.Forms.Button btnSanPham;
        private System.Windows.Forms.Button btnQuaylai;
        private System.Windows.Forms.Button btnTKSPNhapThang;
    }
}