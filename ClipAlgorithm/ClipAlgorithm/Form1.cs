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

namespace ClipAlgorithm {
    public partial class Form1 : Form {

        public Form1() {
            InitializeComponent();
            this.Paint += new PaintEventHandler(this.Form1_Paint); //初始化载入的像素方格
            this.MouseClick += new MouseEventHandler(this.Form1_Click); //监听点击事件
            this.MouseMove += new MouseEventHandler(this.Form1_MouseMove); //监听鼠标移动事件 
        }
        public Graphics gp;
        public Graphics g;
        private int cliRight=0;
        private int wsize;
        private int hsize;
        private int rotateHgt = 520;//矩阵变换高度
        private int deciPoiIn = 0; //决策点的赋值用
        private Point lineP_Transit;
        private Point lineP_start;
        private Point lineP_end;
        private Point li_ns;//next start
        private Point li_ne;
        private Point[] poiLst;
        Pen pen = new Pen(Color.Blue, 1);
        

        private void Form1_Paint(object sender, PaintEventArgs e) {
            gp = e.Graphics;
            //if (matUse == 0) { }
            Matrix mat = new Matrix(1, 0, 0, -1, 0, 0);//沿X轴翻转
            gp.Transform = mat;
            gp.TranslateTransform(0, -rotateHgt);
            //matUse = 1;
            drawZoom(gp);
            /*
            if (cliRight == 1) {
                cipLineSuc(gp, lineP_start, lineP_end);
                this.Refresh();
                cliRight = 0;
            }  */
        }


        private void Form1_Load(object sender, EventArgs e) {
            g = this.CreateGraphics(); ;
            //if (matUse == 0) { }
            Matrix mat = new Matrix(1, 0, 0, -1, 0, 0);//沿X轴翻转
            g.Transform = mat;
            g.TranslateTransform(0, -rotateHgt);
            poiLst = new Point[10];
        }

        //监听鼠标点击事件，获取坐标
        private void Form1_Click(object sender, MouseEventArgs e) {

            if (e.Button == MouseButtons.Left) {  //鼠标左击
                Point readPoint = this.PointToClient(Control.MousePosition);//基于工作区的坐标
                inTimeLbl.Text = mouseToGrap(readPoint).ToString();//显示到窗体上
                
                if (deciPoiIn == 0) {  //没有选择过起点时
                    lineP_start = mouseToGrap(readPoint);//鼠标坐标转换为xy下的坐标
                    drawPixelsPoint(g, lineP_start);//显示点，画像素点
                    poiLst[deciPoiIn] = lineP_start;
                    deciPoiIn += 1;
                } else if (deciPoiIn == 1) {
                    lineP_end = mouseToGrap(readPoint);
                    drawPixelsPoint(g, lineP_end);
                    poiLst[deciPoiIn] = lineP_start;
                    deciPoiIn += 1;
                } else {
                    lineP_start = lineP_end;
                    lineP_end = mouseToGrap(readPoint);
                    drawPixelsPoint(g, lineP_end);

                    poiLst[2] = lineP_start;
                }
                
                
            } else if (e.Button == MouseButtons.Right) {//drawline
                
                g.DrawLine(pen,lineP_start,lineP_end);
                //cliRight = 1;
                //cipLineSuc(g, lineP_start, lineP_end);

            } else if (e.Button == MouseButtons.Middle) {//clip
                //inTimeLbl.Text = "23";
                cipLineSuc(g, lineP_start, lineP_end);
            }
        }

        //裁剪算法
        public void cipLineSuc(Graphics gh,Point stp,Point etp) {
            int[] d;
            int[] c1 = clipcode(stp.X, stp.Y);
            int[] c2 = clipcode(etp.X, etp.Y);
            int[] c3 ={ 0, 0, 0, 0 };
            int x, y,xl, xr, yt, yb;
            xl = 180;//t b r l
            xr = 500;
            yb = 160;
            yt = 340;
            x = y = 0;
            while (codeNotEqu(c1,c3) | codeNotEqu(c2,c3)) {
                //不满足while的条件是c1，c2都为c3，也就是完全可见
                if (ctimetwo(c1, c2)) {
                    return;//显然完全不可见
                }
                d = c1;
                if (!codeNotEqu(d,c3)) {
                    d = c2;
                }
                if (d[3] == 1) {// left
                    y = (xl - stp.X) * (etp.Y - stp.Y) / (etp.X - stp.X) + stp.Y;//y=kx+b
                    x = xl;
                } else if (d[2] == 1) {  // right
                    y = (xr - stp.X) * (etp.Y - stp.Y) / (etp.X - stp.X) + stp.Y;
                    x = xr;
                } else if (d[1] == 1) {// botton
                    x = (yb - stp.Y) * (etp.X - stp.X) / (etp.Y - stp.Y) + stp.X;//y=kx+b
                    y = yb;
                } else if (d[0] == 1) {// top
                    x = (yt - stp.Y) * (etp.X - stp.X) / (etp.Y - stp.Y) + stp.X;
                    y = yt;
                }

                if (! codeNotEqu(d,c1)) {
                    stp = new Point(x, y);
                    c1 = clipcode(x, y);
                } else {
                    etp = new Point(x, y);
                    c2 = clipcode(x, y);
                }
                
            }//  end y

            gh.DrawLine(new Pen(Color.Red, 2),stp,etp);

        }

        public int[] clipcode(int x,int y) {
            int xl, xr, yt, yb;
            int[] c = { 0, 0, 0, 0 };
            xl = 180;//t b r l
            xr = 500;
            yb = 160;
            yt = 340;
            if (x < xl) {
                c[3] = 1;
            }
            else if (x > xr) {
                c[2] = 1;
            }
            if (y < yb) {
                c[1] = 1;
            } else if (y > yt) {
                c[0] = 1;
            }

            return c;
        }

        public bool ctimetwo(int[] c1,int[] c2) {
            //对应代码 c1*c2<>0  
            for(int i = 0; i < 4; i++) {
                int k = c1[i] * c2[i];
                if (k != 0) {
                    return true;
                }
            }

            return false;
        }

        public bool codeNotEqu(int[]c1,int[] c2) {
            //判断c1是否等于c2居然不能够直接由c1==c2判断，sad，记得，以后估计得做笔记
            for (int i = 0; i<4; i++){
                if (c1[i] != c2[i])
                    return true;
            }//注意这里反向了，不等于时返回true，等于返回FALSE
            return false;
        }
        private void drawZoom(Graphics g) {
            Pen zp = new Pen(Color.Black, 1);
            Rectangle rtg = new Rectangle(180, 160, 320, 180);

            g.DrawRectangle(zp,rtg);
        }


        private void drawPixelsPoint(Graphics g,Point p) {
            Size sz = new Size(3, 3);
            //g.DrawEllipse(new Pen(Color.Red, 1), new Rectangle(p, sz));
            g.FillEllipse(Brushes.Red, new Rectangle(p, sz));
        }


        //显示实时点，
        private void Form1_MouseMove(object sender, MouseEventArgs e) {
            
            Point intimePoi = this.PointToClient(Control.MousePosition);
            Point inTimepo = mouseToGrap(intimePoi);
            inTimeLbl.Text = inTimepo.ToString();
            
        }
        //鼠标坐标转换为坐标系下的坐标 mouse to Graphics
        public Point mouseToGrap(Point readPoi) {
            int poiy = (rotateHgt - readPoi.Y);
            return new Point(readPoi.X, poiy);
        }

        private void clipBtn_Click(object sender, EventArgs e) {
            cipLineSuc(g, lineP_start, lineP_end);
        }
        //分线按钮
        private void chipLineBtn_Click(object sender, EventArgs e) {

        }
    }
}
