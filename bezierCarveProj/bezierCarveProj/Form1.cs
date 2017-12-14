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

namespace bezierCarveProj {
    public partial class bezierMainForm : Form {
        Pen rubPen = new Pen(Color.SpringGreen, 2);//橡皮筋效果用笔；rubber pen
        Pen bzPen = new Pen(Color.Red, 1);//贝塞尔曲线的画笔
        Point intimePoi; //intime point
        bool useRubber = true;
        bool movingPoint = false;
        bool isMleftD = false;
        Graphics gp, gh;
        private Bitmap bitmap = null;
        public List<PointF> poilst = new List<PointF>(); //多边形端点

        public bezierMainForm() {
            InitializeComponent();
            this.Paint += new PaintEventHandler(this.bezierMainForm_Paint); //初始化载入的像素方格
            this.MouseClick += new MouseEventHandler(this.bezierMainForm_Click); //监听点击事件
            this.MouseMove += new MouseEventHandler(this.bezierMainForm_MouseMove); //监听鼠标移动事件 
            this.MouseDown+=new MouseEventHandler(this.bezierMainForm_MouseDown);
            this.MouseUp += new MouseEventHandler(this.bezierMainForm_MouseUp);
            this.KeyUp += new KeyEventHandler(this.bezierMainForm_KeyUp);//键盘按键事件
            //激活双缓冲技术
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
        }

#region 响应事件
        private void bezierMainForm_Paint(object sender, PaintEventArgs e) {
            gh = e.Graphics;
            bitmap = new Bitmap(ClientSize.Width, ClientSize.Height);

            gp = Graphics.FromImage(bitmap);
            gp.Clear(this.BackColor);
            gp.SmoothingMode = SmoothingMode.AntiAlias;//设置抗锯齿平滑模式
            if (useRubber && intimePoi != null) {//橡皮筋在使用中
                int plct = poilst.Count;
                if (plct == 0) {//还没有点
                } else if (plct == 1) {//只存了一个点
                    gp.DrawLine(rubPen, poilst[0], intimePoi);
                    gp.FillEllipse(Brushes.Black, new RectangleF(new PointF(poilst[0].X - 2, poilst[0].Y - 2), new Size(4,4)));
                } else {//两点及以上
                    for (int i = 0; i < plct; i++) {
                        if (i == plct - 1) {
                            
                            gp.DrawLine(rubPen, poilst[i], intimePoi);
                            gp.FillEllipse(Brushes.Black, new RectangleF(new PointF(poilst[i].X - 2, poilst[i].Y - 2), new Size(4, 4)));
                        } else {
                            gp.DrawLine(rubPen, poilst[i], poilst[i + 1]);
                            gp.FillEllipse(Brushes.Black, new RectangleF(new PointF(poilst[i].X - 2, poilst[i].Y - 2), new Size(4, 4)));
                        }
                    }
                    
                    try {
                        drawBezier(poilst, gp);
                    }catch(Exception exp) {

                    }
                }
            } else if (useRubber == false && intimePoi != null) {//按下中键后
                int plct = poilst.Count;
                if (plct == 0 | plct == 1) {
                } else {//两点及以上
                    for (int i = 0; i < plct; i++) {
                        if (i == plct - 1) {
                            gp.FillEllipse(Brushes.Black, new RectangleF(new PointF(poilst[i].X - 2, poilst[i].Y - 2), new Size(4, 4)));
                        } else {
                            gp.DrawLine(rubPen, poilst[i], poilst[i + 1]);
                            gp.FillEllipse(Brushes.Black, new RectangleF(new PointF(poilst[i].X - 2, poilst[i].Y - 2), new Size(4, 4)));
                        }
                    }
                    
                    drawBezier(poilst, gp);
                }
            }
            if (poilst.Count>2) {
                drawBezier(poilst, gh);
            }
            
            gh.DrawImage(bitmap, 0, 0);//display

        }

        private void bezierMainForm_Click(object sender, MouseEventArgs e) {

            if (e.Button == MouseButtons.Left) {//鼠标左击
                if (!movingPoint) {//不移动点时
                    useRubber = true;
                
                    Point readPoint = this.PointToClient    (Control.MousePosition);//基于工作区的坐标
                    intimePoi = readPoint;
                    intimePoiLbl.Text = readPoint.ToString();
                    poilst.Add(readPoint);//加点到list<point>里
                    infoLabel.Text = "左键选点；右键结束；中键可撤销选的最后一点";
                }
            } else if (e.Button == MouseButtons.Right && poilst.Count != 0) {  //右键
                 useRubber = false;
                 this.Refresh();
                 infoLabel.Text = "可以移动点，点击重绘按钮重新绘制";
            } else if (e.Button == MouseButtons.Middle) {  //中键
                int plast = poilst.Count - 1;
                if (plast >= 0) {
                    poilst.RemoveAt(plast);
                    useRubber = true;
                }
                this.Refresh();
            }

        }

        private void bezierMainForm_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left && !useRubber) {//鼠标左击
                isMleftD = true;
                if (!movingPoint) {//不移动点时
                    useRubber = true;
                }
            }
        }
        private void bezierMainForm_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {//鼠标左击
                isMleftD = false;
            }

        }
        //键盘事件，但是不知为何触发不了。
        private void bezierMainForm_KeyUp(object sender, KeyEventArgs e) {
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
        
        private void bezierMainForm_MouseMove(object sender, MouseEventArgs e) {
            intimePoi = this.PointToClient(Control.MousePosition);//基于工作区的坐标
            Graphics gw = this.CreateGraphics();
            if (useRubber) { //在橡皮筋模式内
                gw.Clear(BackColor);
                int plct = poilst.Count;
                if (plct == 0) {// ==0： pass
                } else if (plct == 1) {
                    gw.DrawLine(rubPen, poilst[0], intimePoi);
                    //gp.FillEllipse(Brushes.Black, new RectangleF(new PointF(poilst[0].X - 2, poilst[0].Y - 2), new Size(4, 4)));
                } else {//两点及以上
                    drawTwoPois(plct, gw);
                    if (plct > 2) {
                        drawBezier(poilst, gw);
                    }
                }
            } else {//结束选点，可以移动点

                movingPoint = true;
                int zoom_index = zoomIndex();
                if (zoom_index >= 0) {
                    this.Cursor = Cursors.SizeAll;//改变鼠标指针样式

                    if (isMleftD) {//鼠标一直按着时
                        poilst[zoom_index] = intimePoi;
                        this.Refresh();
                    }
                } else {
                    this.Cursor = Cursors.Cross;
                }

            }
            intimePoiLbl.Text = intimePoi.ToString();
        }
#endregion 

        //poilst有两点以上鼠标移动时会调用的函数
        private void drawTwoPois(int plct, Graphics gp) {
            for (int i = 0; i < plct; i++) {
                if (i == plct - 1) {//画到最后一点了
                    gp.DrawLine(rubPen, poilst[i], intimePoi);
                    gp.FillEllipse(Brushes.Black, new RectangleF(new PointF(poilst[i].X - 2, poilst[i].Y - 2), new Size(4, 4)));
                } else {
                    gp.DrawLine(rubPen, poilst[i], poilst[i + 1]);
                    gp.FillEllipse(Brushes.Black, new RectangleF(new PointF(poilst[i].X - 2, poilst[i].Y - 2), new Size(4, 4)));
                }
            }
        }
#region 贝塞尔曲线绘制核心函数
        
        //画贝塞尔曲线的核心函数
        private void drawBezier(List<PointF> plst,Graphics gc) {
            List<PointF> qlist=new List<PointF>();
            List<PointF> rlist= new List<PointF>();
            List<PointF> p_list =listCopy(plst);
            Stack<List<PointF>> stk=new Stack<List<PointF>>();
            //p_list = listCopy(plst);
            stk.Push(p_list);
            while (stk.Count != 0) {
                p_list = stk.Pop(); //plst'
                if (maxDisPoint(p_list) <= 1) {
                    gc.DrawLine(bzPen, p_list[0], p_list[p_list.Count-1]);
                } else {
                    curveSplit(p_list,out qlist,out rlist);
                    stk.Push(qlist);
                    stk.Push(rlist);
                }

            }
            
        }
        //中点离散
        private void curveSplit(List<PointF> plst, out List<PointF> qlst, out List<PointF> rlst) {
            int n = plst.Count - 1;
            qlst = listCopy(plst);
            rlst = listCopy(plst);//赋值，不能是引用，否则会出问题,不能让修改rlst的操作影响到plst
            for (int i = 1; i <= n; i++) {
                rlst[n + 1 - i] = qlst[n];
                for (int j = n; j >= i; j--) {
                    float qjx = (qlst[j - 1].X + qlst[j].X) / 2.0f;
                    float qjy = (qlst[j - 1].Y + qlst[j].Y) / 2.0f;
                    qlst[j] = new PointF(qjx, qjy);
                }
            }
            rlst[0] = qlst[n];
        }

        //求最大距离，并且用的是距离的平方
        private double maxDisPoint(List<PointF> plst) {
            //这个函数并不会对plst进行改变
            PointF sttPoi = plst[0];
            PointF endPoi = plst[plst.Count-1];

            PointF pPoint = plst[1];
            double maxDist = 0;//这个是最终要返回的值
            double maxdis = 0;
            double dTwo = (endPoi.X - sttPoi.X) * (endPoi.X - sttPoi.X) + (endPoi.Y - sttPoi.Y) * (endPoi.Y - sttPoi.Y);
            for (int i = 1; i < plst.Count-1; i++) {
                pPoint = plst[i];
                double abApr = (endPoi.X - sttPoi.X) * (pPoint.X - sttPoi.X) + (endPoi.Y - sttPoi.Y) * (pPoint.Y - sttPoi.Y);
                if (abApr <= 0) {
                    maxdis = (pPoint.X - sttPoi.X) * (pPoint.X - sttPoi.X) + (pPoint.Y - sttPoi.Y) * (pPoint.Y - sttPoi.Y);
                } else if (abApr >= dTwo) {
                    maxdis = (pPoint.X - endPoi.X) * (pPoint.X - endPoi.X) + (pPoint.Y - endPoi.Y) * (pPoint.Y - endPoi.Y);
                } else {
                    double r = abApr / dTwo;
                    double px = sttPoi.X + (endPoi.X - sttPoi.X) * r;
                    double py = sttPoi.Y + (endPoi.Y - sttPoi.Y) * r;
                    maxdis = (pPoint.X - px) * (pPoint.X - px) + (py - pPoint.Y) * (py - pPoint.Y);
                }

                if (maxdis > maxDist) {
                    maxDist = maxdis;
                }
            }


            return maxDist;
        }
        //对引用类数组进行深拷贝
        private List<PointF> listCopy(List<PointF> oldLst) {
            List<PointF> newLst = new List<PointF>();
            for(int i = 0; i < oldLst.Count; i++) {
                newLst.Add(oldLst[i]);
            }
            return newLst;
        }

#endregion
        
        private PointF[] listToPoiF() {
            PointF [] pls= new PointF[poilst.Count];
            for (int i = 0; i < poilst.Count; i++) {
                pls[i] = poilst[i];
            }

            return pls;
        }
        
        private int zoomIndex() {
            int plct = poilst.Count;
            for (int i = 0; i < plct; i++) {
                
                if (Math.Abs(intimePoi.X - poilst[i].X) < 20 && Math.Abs(intimePoi.Y - poilst[i].Y)<20) {
                    return i;
                }
            }
                return -1;
        }
        
        //画端点（顶点）
        private void drawVertex(Graphics g, Point poi) {
            Size sz = new Size(4, 4);
            g.FillEllipse(Brushes.Red, new Rectangle(poi, sz));
        }
 
#region 选曲线颜色系列点击事件
        private void redColorLbl_Click(object sender, EventArgs e) {
            bzPen.Color = redColorLbl.BackColor;
        }

        private void blueColorLbl_Click(object sender, EventArgs e) {
            bzPen.Color = blueColorLbl.BackColor;
        }

        private void aquaColorLbl_Click(object sender, EventArgs e) {
            bzPen.Color = aquaColorLbl.BackColor;
        }

        private void goldColorLbl_Click(object sender, EventArgs e) {
            bzPen.Color = goldColorLbl.BackColor;

        }

        private void seagColorLbl_Click(object sender, EventArgs e) {
            bzPen.Color = seagColorLbl.BackColor;
        }

        private void moreColorLbl_Click(object sender, EventArgs e) {
            ColorDialog moreClr = new ColorDialog();
            if (moreClr.ShowDialog() == DialogResult.OK) {
                bzPen.Color = moreClr.Color;
            }
        }

        #endregion



        //重新绘制
        private void repaintBtn_Click(object sender, EventArgs e) {
            poilst.Clear(); //画的点也要清除
            movingPoint = false;
            isMleftD = false;
            useRubber = true;
            infoLabel.Text = "点击画点";
            this.Refresh();
        }
    }
}
