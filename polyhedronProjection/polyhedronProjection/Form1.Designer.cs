namespace polyhedronProjection {
    partial class projZForm {
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
            this.components = new System.ComponentModel.Container();
            this.perspPrjraBtn = new System.Windows.Forms.RadioButton();
            this.parallelPrjraBtn = new System.Windows.Forms.RadioButton();
            this.viewPoiXtBox = new System.Windows.Forms.TextBox();
            this.viewPoiYtBox = new System.Windows.Forms.TextBox();
            this.viewPoiZtBox = new System.Windows.Forms.TextBox();
            this.projNowBtn = new System.Windows.Forms.Button();
            this.autoRotateBtn = new System.Windows.Forms.Button();
            this.stopRotateBtn = new System.Windows.Forms.Button();
            this.gotoStPoiBtn = new System.Windows.Forms.Button();
            this.showAxisCkBox = new System.Windows.Forms.CheckBox();
            this.viewPoiInfoLbl = new System.Windows.Forms.Label();
            this.infoXLbl = new System.Windows.Forms.Label();
            this.stepInfolabel = new System.Windows.Forms.Label();
            this.rotaSteptLentBox = new System.Windows.Forms.TextBox();
            this.rotateTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // perspPrjraBtn
            // 
            this.perspPrjraBtn.AutoSize = true;
            this.perspPrjraBtn.Checked = true;
            this.perspPrjraBtn.Location = new System.Drawing.Point(739, 28);
            this.perspPrjraBtn.Name = "perspPrjraBtn";
            this.perspPrjraBtn.Size = new System.Drawing.Size(71, 16);
            this.perspPrjraBtn.TabIndex = 0;
            this.perspPrjraBtn.TabStop = true;
            this.perspPrjraBtn.Text = "透视投影";
            this.perspPrjraBtn.UseVisualStyleBackColor = true;
            // 
            // parallelPrjraBtn
            // 
            this.parallelPrjraBtn.AutoSize = true;
            this.parallelPrjraBtn.Location = new System.Drawing.Point(739, 50);
            this.parallelPrjraBtn.Name = "parallelPrjraBtn";
            this.parallelPrjraBtn.Size = new System.Drawing.Size(71, 16);
            this.parallelPrjraBtn.TabIndex = 1;
            this.parallelPrjraBtn.Text = "平行投影";
            this.parallelPrjraBtn.UseVisualStyleBackColor = true;
            // 
            // viewPoiXtBox
            // 
            this.viewPoiXtBox.Location = new System.Drawing.Point(739, 96);
            this.viewPoiXtBox.Name = "viewPoiXtBox";
            this.viewPoiXtBox.Size = new System.Drawing.Size(75, 21);
            this.viewPoiXtBox.TabIndex = 2;
            this.viewPoiXtBox.Text = "200";
            // 
            // viewPoiYtBox
            // 
            this.viewPoiYtBox.Location = new System.Drawing.Point(739, 123);
            this.viewPoiYtBox.Name = "viewPoiYtBox";
            this.viewPoiYtBox.Size = new System.Drawing.Size(75, 21);
            this.viewPoiYtBox.TabIndex = 3;
            this.viewPoiYtBox.Text = "200";
            // 
            // viewPoiZtBox
            // 
            this.viewPoiZtBox.Location = new System.Drawing.Point(739, 150);
            this.viewPoiZtBox.Name = "viewPoiZtBox";
            this.viewPoiZtBox.Size = new System.Drawing.Size(75, 21);
            this.viewPoiZtBox.TabIndex = 4;
            this.viewPoiZtBox.Text = "200";
            // 
            // projNowBtn
            // 
            this.projNowBtn.Location = new System.Drawing.Point(739, 190);
            this.projNowBtn.Name = "projNowBtn";
            this.projNowBtn.Size = new System.Drawing.Size(75, 25);
            this.projNowBtn.TabIndex = 5;
            this.projNowBtn.Text = "投影";
            this.projNowBtn.UseVisualStyleBackColor = true;
            this.projNowBtn.Click += new System.EventHandler(this.projNowBtn_Click);
            // 
            // autoRotateBtn
            // 
            this.autoRotateBtn.Location = new System.Drawing.Point(739, 327);
            this.autoRotateBtn.Name = "autoRotateBtn";
            this.autoRotateBtn.Size = new System.Drawing.Size(75, 23);
            this.autoRotateBtn.TabIndex = 6;
            this.autoRotateBtn.Text = "自动旋转";
            this.autoRotateBtn.UseVisualStyleBackColor = true;
            this.autoRotateBtn.Click += new System.EventHandler(this.autoRotateBtn_Click);
            // 
            // stopRotateBtn
            // 
            this.stopRotateBtn.Enabled = false;
            this.stopRotateBtn.Location = new System.Drawing.Point(739, 366);
            this.stopRotateBtn.Name = "stopRotateBtn";
            this.stopRotateBtn.Size = new System.Drawing.Size(75, 23);
            this.stopRotateBtn.TabIndex = 7;
            this.stopRotateBtn.Text = "停止旋转";
            this.stopRotateBtn.UseVisualStyleBackColor = true;
            this.stopRotateBtn.Click += new System.EventHandler(this.stopRotateBtn_Click);
            // 
            // gotoStPoiBtn
            // 
            this.gotoStPoiBtn.Location = new System.Drawing.Point(739, 406);
            this.gotoStPoiBtn.Name = "gotoStPoiBtn";
            this.gotoStPoiBtn.Size = new System.Drawing.Size(75, 23);
            this.gotoStPoiBtn.TabIndex = 8;
            this.gotoStPoiBtn.Text = "初始位置";
            this.gotoStPoiBtn.UseVisualStyleBackColor = true;
            this.gotoStPoiBtn.Click += new System.EventHandler(this.gotoStPoiBtn_Click);
            // 
            // showAxisCkBox
            // 
            this.showAxisCkBox.AutoSize = true;
            this.showAxisCkBox.Checked = true;
            this.showAxisCkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showAxisCkBox.Location = new System.Drawing.Point(730, 464);
            this.showAxisCkBox.Name = "showAxisCkBox";
            this.showAxisCkBox.Size = new System.Drawing.Size(84, 16);
            this.showAxisCkBox.TabIndex = 9;
            this.showAxisCkBox.Text = "显示坐标轴";
            this.showAxisCkBox.UseVisualStyleBackColor = true;
            this.showAxisCkBox.CheckedChanged += new System.EventHandler(this.showAxisCkBox_CheckedChanged);
            // 
            // viewPoiInfoLbl
            // 
            this.viewPoiInfoLbl.AutoSize = true;
            this.viewPoiInfoLbl.Location = new System.Drawing.Point(728, 81);
            this.viewPoiInfoLbl.Name = "viewPoiInfoLbl";
            this.viewPoiInfoLbl.Size = new System.Drawing.Size(77, 12);
            this.viewPoiInfoLbl.TabIndex = 10;
            this.viewPoiInfoLbl.Text = "视点或方向：";
            // 
            // infoXLbl
            // 
            this.infoXLbl.AutoSize = true;
            this.infoXLbl.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.infoXLbl.Location = new System.Drawing.Point(715, 97);
            this.infoXLbl.Name = "infoXLbl";
            this.infoXLbl.Size = new System.Drawing.Size(21, 70);
            this.infoXLbl.TabIndex = 11;
            this.infoXLbl.Text = "x:\r\n\r\ny:\r\n\r\nz:";
            // 
            // stepInfolabel
            // 
            this.stepInfolabel.AutoSize = true;
            this.stepInfolabel.Location = new System.Drawing.Point(728, 251);
            this.stepInfolabel.Name = "stepInfolabel";
            this.stepInfolabel.Size = new System.Drawing.Size(89, 12);
            this.stepInfolabel.TabIndex = 12;
            this.stepInfolabel.Text = "旋转步长(度)：";
            // 
            // rotaSteptLentBox
            // 
            this.rotaSteptLentBox.Location = new System.Drawing.Point(739, 266);
            this.rotaSteptLentBox.Name = "rotaSteptLentBox";
            this.rotaSteptLentBox.Size = new System.Drawing.Size(75, 21);
            this.rotaSteptLentBox.TabIndex = 13;
            this.rotaSteptLentBox.Text = "2";
            // 
            // rotateTimer
            // 
            this.rotateTimer.Tick += new System.EventHandler(this.rotateTimer_Tick);
            // 
            // projZForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 588);
            this.Controls.Add(this.rotaSteptLentBox);
            this.Controls.Add(this.stepInfolabel);
            this.Controls.Add(this.infoXLbl);
            this.Controls.Add(this.viewPoiInfoLbl);
            this.Controls.Add(this.showAxisCkBox);
            this.Controls.Add(this.gotoStPoiBtn);
            this.Controls.Add(this.stopRotateBtn);
            this.Controls.Add(this.autoRotateBtn);
            this.Controls.Add(this.projNowBtn);
            this.Controls.Add(this.viewPoiZtBox);
            this.Controls.Add(this.viewPoiYtBox);
            this.Controls.Add(this.viewPoiXtBox);
            this.Controls.Add(this.parallelPrjraBtn);
            this.Controls.Add(this.perspPrjraBtn);
            this.Name = "projZForm";
            this.Text = "投影显示窗体";
            this.Load += new System.EventHandler(this.projZForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton perspPrjraBtn;
        private System.Windows.Forms.RadioButton parallelPrjraBtn;
        private System.Windows.Forms.TextBox viewPoiXtBox;
        private System.Windows.Forms.TextBox viewPoiYtBox;
        private System.Windows.Forms.TextBox viewPoiZtBox;
        private System.Windows.Forms.Button projNowBtn;
        private System.Windows.Forms.Button autoRotateBtn;
        private System.Windows.Forms.Button stopRotateBtn;
        private System.Windows.Forms.Button gotoStPoiBtn;
        private System.Windows.Forms.CheckBox showAxisCkBox;
        private System.Windows.Forms.Label viewPoiInfoLbl;
        private System.Windows.Forms.Label infoXLbl;
        private System.Windows.Forms.Label stepInfolabel;
        private System.Windows.Forms.TextBox rotaSteptLentBox;
        private System.Windows.Forms.Timer rotateTimer;
    }
}

