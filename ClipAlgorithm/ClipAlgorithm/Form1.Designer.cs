namespace ClipAlgorithm {
    partial class Form1 {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.clipBtn = new System.Windows.Forms.Button();
            this.chipLineBtn = new System.Windows.Forms.Button();
            this.inTimeLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // clipBtn
            // 
            this.clipBtn.Location = new System.Drawing.Point(621, 65);
            this.clipBtn.Name = "clipBtn";
            this.clipBtn.Size = new System.Drawing.Size(66, 23);
            this.clipBtn.TabIndex = 0;
            this.clipBtn.Text = "裁剪";
            this.clipBtn.UseVisualStyleBackColor = true;
            this.clipBtn.Click += new System.EventHandler(this.clipBtn_Click);
            // 
            // chipLineBtn
            // 
            this.chipLineBtn.Location = new System.Drawing.Point(621, 123);
            this.chipLineBtn.Name = "chipLineBtn";
            this.chipLineBtn.Size = new System.Drawing.Size(66, 23);
            this.chipLineBtn.TabIndex = 1;
            this.chipLineBtn.Text = "分线";
            this.chipLineBtn.UseVisualStyleBackColor = true;
            this.chipLineBtn.Click += new System.EventHandler(this.chipLineBtn_Click);
            // 
            // inTimeLbl
            // 
            this.inTimeLbl.AutoSize = true;
            this.inTimeLbl.Location = new System.Drawing.Point(619, 511);
            this.inTimeLbl.Name = "inTimeLbl";
            this.inTimeLbl.Size = new System.Drawing.Size(17, 12);
            this.inTimeLbl.TabIndex = 2;
            this.inTimeLbl.Text = "{}";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 521);
            this.Controls.Add(this.inTimeLbl);
            this.Controls.Add(this.chipLineBtn);
            this.Controls.Add(this.clipBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button clipBtn;
        private System.Windows.Forms.Button chipLineBtn;
        private System.Windows.Forms.Label inTimeLbl;
    }
}

