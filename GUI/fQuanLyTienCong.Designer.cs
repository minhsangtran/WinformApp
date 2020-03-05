namespace QuanLyGaraOto.GUI
{
    partial class fQuanLyTienCong
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dtgvAllWageInfo = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dtgvWageInfor = new System.Windows.Forms.DataGridView();
            this.lbNumAcceptFormGot = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txbWageValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnWageUpdate = new System.Windows.Forms.Button();
            this.btnDeleteWage = new System.Windows.Forms.Button();
            this.ckbAddNewWage = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txbWageName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel9.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvAllWageInfo)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvWageInfor)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(34, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1061, 529);
            this.panel1.TabIndex = 0;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.groupBox6);
            this.panel9.Location = new System.Drawing.Point(469, 3);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(585, 514);
            this.panel9.TabIndex = 2;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dtgvAllWageInfo);
            this.groupBox6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(3, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(576, 508);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Danh sách tiền công";
            this.groupBox6.Enter += new System.EventHandler(this.groupBox6_Enter);
            // 
            // dtgvAllWageInfo
            // 
            this.dtgvAllWageInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvAllWageInfo.Location = new System.Drawing.Point(7, 25);
            this.dtgvAllWageInfo.Name = "dtgvAllWageInfo";
            this.dtgvAllWageInfo.Size = new System.Drawing.Size(563, 475);
            this.dtgvAllWageInfo.TabIndex = 0;
            this.dtgvAllWageInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgvAllWageInfo_CellClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Controls.Add(this.lbNumAcceptFormGot);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Location = new System.Drawing.Point(4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(428, 514);
            this.panel2.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dtgvWageInfor);
            this.groupBox4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(3, 278);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(420, 233);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Thông tin tiền công";
            // 
            // dtgvWageInfor
            // 
            this.dtgvWageInfor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvWageInfor.Location = new System.Drawing.Point(7, 20);
            this.dtgvWageInfor.Name = "dtgvWageInfor";
            this.dtgvWageInfor.Size = new System.Drawing.Size(407, 202);
            this.dtgvWageInfor.TabIndex = 0;
            this.dtgvWageInfor.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgvWageInfor_CellClick);
            // 
            // lbNumAcceptFormGot
            // 
            this.lbNumAcceptFormGot.AutoSize = true;
            this.lbNumAcceptFormGot.Location = new System.Drawing.Point(130, 487);
            this.lbNumAcceptFormGot.Name = "lbNumAcceptFormGot";
            this.lbNumAcceptFormGot.Size = new System.Drawing.Size(0, 13);
            this.lbNumAcceptFormGot.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel5);
            this.groupBox1.Controls.Add(this.btnWageUpdate);
            this.groupBox1.Controls.Add(this.btnDeleteWage);
            this.groupBox1.Controls.Add(this.ckbAddNewWage);
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 269);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin tiền công";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.txbWageValue);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(9, 106);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(405, 42);
            this.panel5.TabIndex = 0;
            // 
            // txbWageValue
            // 
            this.txbWageValue.Location = new System.Drawing.Point(159, 8);
            this.txbWageValue.Name = "txbWageValue";
            this.txbWageValue.Size = new System.Drawing.Size(240, 26);
            this.txbWageValue.TabIndex = 3;
            this.txbWageValue.TextChanged += new System.EventHandler(this.TxbWageValue_TextChanged);
            this.txbWageValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxbWageValue_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 19);
            this.label4.TabIndex = 0;
            this.label4.Text = "Trị giá:";
            // 
            // btnWageUpdate
            // 
            this.btnWageUpdate.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWageUpdate.Location = new System.Drawing.Point(168, 193);
            this.btnWageUpdate.Name = "btnWageUpdate";
            this.btnWageUpdate.Size = new System.Drawing.Size(110, 43);
            this.btnWageUpdate.TabIndex = 10;
            this.btnWageUpdate.Text = "Cập nhật";
            this.btnWageUpdate.UseVisualStyleBackColor = true;
            this.btnWageUpdate.Click += new System.EventHandler(this.BtnWageUpdate_Click);
            // 
            // btnDeleteWage
            // 
            this.btnDeleteWage.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteWage.Location = new System.Drawing.Point(298, 193);
            this.btnDeleteWage.Name = "btnDeleteWage";
            this.btnDeleteWage.Size = new System.Drawing.Size(110, 43);
            this.btnDeleteWage.TabIndex = 10;
            this.btnDeleteWage.Text = "Xóa";
            this.btnDeleteWage.UseVisualStyleBackColor = true;
            this.btnDeleteWage.Click += new System.EventHandler(this.BtnDeleteWage_Click);
            // 
            // ckbAddNewWage
            // 
            this.ckbAddNewWage.AutoSize = true;
            this.ckbAddNewWage.Checked = true;
            this.ckbAddNewWage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbAddNewWage.Location = new System.Drawing.Point(9, 204);
            this.ckbAddNewWage.Name = "ckbAddNewWage";
            this.ckbAddNewWage.Size = new System.Drawing.Size(104, 23);
            this.ckbAddNewWage.TabIndex = 8;
            this.ckbAddNewWage.Text = "Thêm mới";
            this.ckbAddNewWage.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txbWageName);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(9, 39);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(405, 42);
            this.panel4.TabIndex = 0;
            // 
            // txbWageName
            // 
            this.txbWageName.Location = new System.Drawing.Point(159, 8);
            this.txbWageName.Name = "txbWageName";
            this.txbWageName.Size = new System.Drawing.Size(240, 26);
            this.txbWageName.TabIndex = 2;
            this.txbWageName.TextChanged += new System.EventHandler(this.TxbWageName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "Tên loại tiền công:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // fQuanLyTienCong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 602);
            this.Controls.Add(this.panel1);
            this.Name = "fQuanLyTienCong";
            this.Text = "fQuanLyTienCong";
            this.panel1.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvAllWageInfo)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvWageInfor)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbNumAcceptFormGot;
        private System.Windows.Forms.CheckBox ckbAddNewWage;
        private System.Windows.Forms.Button btnDeleteWage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox txbWageValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txbWageName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dtgvAllWageInfo;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dtgvWageInfor;
        private System.Windows.Forms.Button btnWageUpdate;
    }
}