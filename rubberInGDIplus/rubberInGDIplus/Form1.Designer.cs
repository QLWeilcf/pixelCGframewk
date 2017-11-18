namespace rubberInGDIplus {
    partial class rubberEffectForm {
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
            this.intimePoiLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // intimePoiLbl
            // 
            this.intimePoiLbl.AutoSize = true;
            this.intimePoiLbl.Location = new System.Drawing.Point(472, 460);
            this.intimePoiLbl.Name = "intimePoiLbl";
            this.intimePoiLbl.Size = new System.Drawing.Size(35, 12);
            this.intimePoiLbl.TabIndex = 0;
            this.intimePoiLbl.Text = "{0,0}";
            // 
            // rubberEffectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 481);
            this.Controls.Add(this.intimePoiLbl);
            this.Name = "rubberEffectForm";
            this.Text = "橡皮筋技术与画多边形";
            this.Load += new System.EventHandler(this.rubberEffectForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label intimePoiLbl;
    }
}

