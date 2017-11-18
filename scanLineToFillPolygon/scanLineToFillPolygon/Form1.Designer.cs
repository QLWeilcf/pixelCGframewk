namespace scanLineToFillPolygon {
    partial class fillPolyForm {
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
            this.infoLebel = new System.Windows.Forms.Label();
            this.intimePoiLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // infoLebel
            // 
            this.infoLebel.AutoSize = true;
            this.infoLebel.Location = new System.Drawing.Point(2, 530);
            this.infoLebel.Name = "infoLebel";
            this.infoLebel.Size = new System.Drawing.Size(77, 12);
            this.infoLebel.TabIndex = 0;
            this.infoLebel.Text = "鼠标左击选点";
            // 
            // intimePoiLbl
            // 
            this.intimePoiLbl.AutoSize = true;
            this.intimePoiLbl.Location = new System.Drawing.Point(574, 523);
            this.intimePoiLbl.Name = "intimePoiLbl";
            this.intimePoiLbl.Size = new System.Drawing.Size(35, 12);
            this.intimePoiLbl.TabIndex = 1;
            this.intimePoiLbl.Text = "{0,0}";
            // 
            // fillPolyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 551);
            this.Controls.Add(this.intimePoiLbl);
            this.Controls.Add(this.infoLebel);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.Name = "fillPolyForm";
            this.Text = "扫描填充";
            this.Load += new System.EventHandler(this.fillPolyForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label infoLebel;
        private System.Windows.Forms.Label intimePoiLbl;
    }
}

