namespace bezierCarveProj {
    partial class bezierMainForm {
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
            this.repaintBtn = new System.Windows.Forms.Button();
            this.infoLabel = new System.Windows.Forms.Label();
            this.intimePoiLbl = new System.Windows.Forms.Label();
            this.redColorLbl = new System.Windows.Forms.Label();
            this.carveColLbl = new System.Windows.Forms.Label();
            this.blueColorLbl = new System.Windows.Forms.Label();
            this.aquaColorLbl = new System.Windows.Forms.Label();
            this.goldColorLbl = new System.Windows.Forms.Label();
            this.seagColorLbl = new System.Windows.Forms.Label();
            this.moreColorLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // repaintBtn
            // 
            this.repaintBtn.Location = new System.Drawing.Point(522, 372);
            this.repaintBtn.Name = "repaintBtn";
            this.repaintBtn.Size = new System.Drawing.Size(75, 23);
            this.repaintBtn.TabIndex = 0;
            this.repaintBtn.Text = "重绘";
            this.repaintBtn.UseVisualStyleBackColor = true;
            this.repaintBtn.Click += new System.EventHandler(this.repaintBtn_Click);
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(2, 427);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(53, 12);
            this.infoLabel.TabIndex = 1;
            this.infoLabel.Text = "点击画点";
            // 
            // intimePoiLbl
            // 
            this.intimePoiLbl.AutoSize = true;
            this.intimePoiLbl.Location = new System.Drawing.Point(406, 426);
            this.intimePoiLbl.Name = "intimePoiLbl";
            this.intimePoiLbl.Size = new System.Drawing.Size(59, 12);
            this.intimePoiLbl.TabIndex = 2;
            this.intimePoiLbl.Text = "{   ,   }";
            // 
            // redColorLbl
            // 
            this.redColorLbl.AutoSize = true;
            this.redColorLbl.BackColor = System.Drawing.Color.Red;
            this.redColorLbl.Location = new System.Drawing.Point(504, 45);
            this.redColorLbl.Name = "redColorLbl";
            this.redColorLbl.Size = new System.Drawing.Size(29, 12);
            this.redColorLbl.TabIndex = 3;
            this.redColorLbl.Text = "    ";
            this.redColorLbl.Click += new System.EventHandler(this.redColorLbl_Click);
            // 
            // carveColLbl
            // 
            this.carveColLbl.AutoSize = true;
            this.carveColLbl.Location = new System.Drawing.Point(497, 18);
            this.carveColLbl.Name = "carveColLbl";
            this.carveColLbl.Size = new System.Drawing.Size(65, 12);
            this.carveColLbl.TabIndex = 4;
            this.carveColLbl.Text = "曲线颜色：";
            // 
            // blueColorLbl
            // 
            this.blueColorLbl.AutoSize = true;
            this.blueColorLbl.BackColor = System.Drawing.Color.Blue;
            this.blueColorLbl.Location = new System.Drawing.Point(539, 45);
            this.blueColorLbl.Name = "blueColorLbl";
            this.blueColorLbl.Size = new System.Drawing.Size(29, 12);
            this.blueColorLbl.TabIndex = 5;
            this.blueColorLbl.Text = "    ";
            this.blueColorLbl.Click += new System.EventHandler(this.blueColorLbl_Click);
            // 
            // aquaColorLbl
            // 
            this.aquaColorLbl.AutoSize = true;
            this.aquaColorLbl.BackColor = System.Drawing.Color.Aqua;
            this.aquaColorLbl.Location = new System.Drawing.Point(574, 45);
            this.aquaColorLbl.Name = "aquaColorLbl";
            this.aquaColorLbl.Size = new System.Drawing.Size(29, 12);
            this.aquaColorLbl.TabIndex = 6;
            this.aquaColorLbl.Text = "    ";
            this.aquaColorLbl.Click += new System.EventHandler(this.aquaColorLbl_Click);
            // 
            // goldColorLbl
            // 
            this.goldColorLbl.AutoSize = true;
            this.goldColorLbl.BackColor = System.Drawing.Color.Gold;
            this.goldColorLbl.Location = new System.Drawing.Point(504, 72);
            this.goldColorLbl.Name = "goldColorLbl";
            this.goldColorLbl.Size = new System.Drawing.Size(29, 12);
            this.goldColorLbl.TabIndex = 7;
            this.goldColorLbl.Text = "    ";
            this.goldColorLbl.Click += new System.EventHandler(this.goldColorLbl_Click);
            // 
            // seagColorLbl
            // 
            this.seagColorLbl.AutoSize = true;
            this.seagColorLbl.BackColor = System.Drawing.Color.SeaGreen;
            this.seagColorLbl.Location = new System.Drawing.Point(539, 72);
            this.seagColorLbl.Name = "seagColorLbl";
            this.seagColorLbl.Size = new System.Drawing.Size(29, 12);
            this.seagColorLbl.TabIndex = 8;
            this.seagColorLbl.Text = "    ";
            this.seagColorLbl.Click += new System.EventHandler(this.seagColorLbl_Click);
            // 
            // moreColorLbl
            // 
            this.moreColorLbl.AutoSize = true;
            this.moreColorLbl.BackColor = System.Drawing.Color.Silver;
            this.moreColorLbl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.moreColorLbl.Location = new System.Drawing.Point(574, 72);
            this.moreColorLbl.Name = "moreColorLbl";
            this.moreColorLbl.Size = new System.Drawing.Size(29, 12);
            this.moreColorLbl.TabIndex = 9;
            this.moreColorLbl.Text = "更多";
            this.moreColorLbl.Click += new System.EventHandler(this.moreColorLbl_Click);
            // 
            // bezierMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 451);
            this.Controls.Add(this.moreColorLbl);
            this.Controls.Add(this.seagColorLbl);
            this.Controls.Add(this.goldColorLbl);
            this.Controls.Add(this.aquaColorLbl);
            this.Controls.Add(this.blueColorLbl);
            this.Controls.Add(this.carveColLbl);
            this.Controls.Add(this.redColorLbl);
            this.Controls.Add(this.intimePoiLbl);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.repaintBtn);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.Name = "bezierMainForm";
            this.Text = "贝塞尔曲线生成";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button repaintBtn;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Label intimePoiLbl;
        private System.Windows.Forms.Label redColorLbl;
        private System.Windows.Forms.Label carveColLbl;
        private System.Windows.Forms.Label blueColorLbl;
        private System.Windows.Forms.Label aquaColorLbl;
        private System.Windows.Forms.Label goldColorLbl;
        private System.Windows.Forms.Label seagColorLbl;
        private System.Windows.Forms.Label moreColorLbl;
    }
}

