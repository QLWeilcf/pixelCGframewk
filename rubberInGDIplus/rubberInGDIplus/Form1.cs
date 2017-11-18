using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/*用双缓冲技术实现GDI+下的橡皮筋效果
 * 能够很好地作为图形学以及矢量多边形小软件的框架
 * 没有用到按钮交互
 * 控件：rubberInGDIplus--主窗体；intimePoiLbl--实时坐标展示
 * 左键选点，中键删除最后一个选择的点；右键完成多边形选择；Del键可删除所有点；
 * 具体的项目可以看我的扫描线填充多边形的代码。**scanLineToFillPolygon**
 * */
namespace rubberInGDIplus {
    public partial class rubberEffectForm : Form {
        Pen rubPen = new Pen(Color.SpringGreen, 2);//橡皮筋效果用笔；rubber pen
        Point readPoi; //intime point
        bool useRubber = true;
        Graphics gp,gh;
        private Bitmap bitmap = null;//虽然可以不用怎么多的Bitmap和 Graphics
        public List<Point> poilst = new List<Point>(); //多边形端点

        public rubberEffectForm() {
            InitializeComponent();
            this.Paint += new PaintEventHandler(this.rubberEffectForm_Paint); //初始化载入的像素方格
            this.MouseClick += new MouseEventHandler(this.rubberEffectForm_Click); //监听点击事件
            this.MouseMove += new MouseEventHandler(this.rubberEffectForm_MouseMove); //监听鼠标移动事件 
            this.KeyUp += new KeyEventHandler(this.rubberEffectForm_KeyUp);//键盘按键事件
            //激活双缓冲技术
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

        }

        private void rubberEffectForm_Load(object sender, EventArgs e) {

        }
        private void rubberEffectForm_Paint(object sender, PaintEventArgs e) {
            gh = e.Graphics;
            bitmap = new Bitmap(ClientSize.Width, ClientSize.Height);

            gp = Graphics.FromImage(bitmap);
            gp.Clear(this.BackColor);
            gp.SmoothingMode = SmoothingMode.AntiAlias;//设置抗锯齿平滑模式
            if (useRubber && readPoi != null) {//橡皮筋在使用中
                int plct = poilst.Count;
                if (plct == 0) {//还没有点
                } else if (plct == 1) {//只存了一个点
                    gp.DrawLine(rubPen, poilst[0], readPoi);
                } else {//两点及以上
                    for (int i = 0; i < plct; i++) {
                        if (i == plct - 1) {
                            gp.DrawLine(rubPen, poilst[0], readPoi);
                            gp.DrawLine(rubPen, poilst[i], readPoi);
                        } else {
                            gp.DrawLine(rubPen, poilst[i], poilst[i + 1]);
                        }
                    }
                }
            } else if (useRubber == false && readPoi != null) {//按下中键后
                int plct = poilst.Count;
                if (plct == 0 | plct == 1) {
                } else {//两点及以上
                    for (int i = 0; i < plct; i++) {
                        if (i == plct - 1) {
                            gp.DrawLine(rubPen, poilst[0], poilst[i]);
                        } else {
                            gp.DrawLine(rubPen, poilst[i], poilst[i + 1]);
                        }
                    }
                }
            }
            

            gh.DrawImage(bitmap, 0, 0);//display

        }

        private void rubberEffectForm_Click(object sender, MouseEventArgs e) {

            if (e.Button == MouseButtons.Left) {//鼠标左击
                useRubber = true;
                Point readPoint = this.PointToClient(Control.MousePosition);//基于工作区的坐标
                readPoi = readPoint;
                intimePoiLbl.Text = readPoint.ToString();
                //drawVertex(gp, readPoint); //画端点（顶点） 由于橡皮筋的覆盖，端点看不出来
                poilst.Add(readPoint);//加点到list<point>里

            } else if (e.Button == MouseButtons.Right) {  //右键
                useRubber = false;
                this.Refresh();

                
                //drawRim();//画边框
            } else if (e.Button == MouseButtons.Middle) {  //中键
                int plast = poilst.Count - 1;
                poilst.RemoveAt(plast);
                useRubber = true;
                this.Refresh();
            }

        }

        private void rubberEffectForm_MouseMove(object sender, MouseEventArgs e) {
            readPoi = this.PointToClient(Control.MousePosition);//基于工作区的坐标
            Graphics gw = this.CreateGraphics();
            if (useRubber) { //在橡皮筋模式内
                gw.Clear(BackColor);
                int plct = poilst.Count;
                if (plct == 0) {// ==0： pass
                } else if (plct == 1) {
                    gw.DrawLine(rubPen, poilst[0], readPoi);
                } else {//两点及以上
                    for (int i = 0; i < plct; i++) {
                        if (i == plct - 1) {//画到最后一点了
                            gw.DrawLine(rubPen, poilst[i], readPoi);
                            gw.DrawLine(rubPen, poilst[0], readPoi);
                        } else {
                            gw.DrawLine(rubPen, poilst[i], poilst[i + 1]);
                        }
                    }
                }
                
            } else {
            }
            intimePoiLbl.Text = readPoi.ToString();
        }

        private void rubberEffectForm_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Delete) {
                poilst.Clear(); //画的点也要清除
                this.Refresh();

            } else if (e.KeyCode == Keys.Back | e.KeyCode == Keys.Escape) {
                int plast = poilst.Count - 1;
                poilst.RemoveAt(plast);
                useRubber = true;
                this.Refresh();
            }

        }




        #region 可用可不用的函数
        //画端点（顶点）
        private void drawVertex(Graphics g, Point poi) {
            Size sz = new Size(4, 4);
            g.FillEllipse(Brushes.Red, new Rectangle(poi, sz));
        }
        private void drawRim() {//画边框
            if (poilst.Count == 0)
                return;
            Point[] poi = new Point[poilst.Count];
            for (int i = 0; i < poilst.Count; i++) {
                poi[i] = poilst[i];
            }
            gp.DrawPolygon(new Pen(Color.Blue, 2), poi);
        }
        
        #endregion

    }
}
