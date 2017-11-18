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

namespace scanLineToFillPolygon {
    public partial class fillPolyForm : Form {
        Pen scPen = new Pen(Color.Red, 1);//填充颜色用笔 scanLine pen
        Pen rubPen = new Pen(Color.SpringGreen, 2);//橡皮筋效果用笔；rubber pen
        Point readPoi; //intime point
        bool useRubber = true;
        bool isFill = false;
        bool rgKeyPrs = false;//right mousekey press
        Graphics gp,g0, g8, g3, g7;
        private Bitmap bm = null;
        private Bitmap bt;
        private Bitmap bitmap = null;//虽然可以不用怎么多的Bitmap和 Graphics
        public List<Point> poilst = new List<Point>(); //多边形端点
        
        public fillPolyForm() {
            InitializeComponent();
            this.Paint += new PaintEventHandler(this.fillPolyForm_Paint); //初始化
            this.MouseClick += new MouseEventHandler(this.fillPolyForm_Click); //监听点击事件
            this.MouseMove += new MouseEventHandler(this.fillPolyForm_MouseMove); //监听鼠标移动事件 
            this.KeyUp += new KeyEventHandler(this.fiiPolyForm_KeyUp);//键盘按键事件
            //激活双缓冲技术
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
        }

        private void fillPolyForm_Load(object sender, EventArgs e) {
            gp = this.CreateGraphics();
            g8 = CreateGraphics();
            bt = new Bitmap(ClientSize.Width, ClientSize.Height);
        }

        private void fillPolyForm_Paint(object sender, PaintEventArgs e) {
            g0 = e.Graphics;
            bitmap = new Bitmap(ClientSize.Width, ClientSize.Height);

            g3 = Graphics.FromImage(bitmap);
            g3.Clear(this.BackColor);
            g3.SmoothingMode = SmoothingMode.AntiAlias;//设置抗锯齿平滑模式
            if (useRubber && readPoi != null) {//橡皮筋在使用中
                int plct = poilst.Count;
                if (plct == 0) {//还没有点
                } else if (plct == 1) {//只存了一个点
                    g3.DrawLine(rubPen, poilst[0], readPoi);
                } else {//两点及以上
                    for (int i = 0; i < plct; i++) {
                        if (i == plct - 1) {
                            g3.DrawLine(rubPen, poilst[0], readPoi);
                            g3.DrawLine(rubPen, poilst[i], readPoi);
                        } else {
                            g3.DrawLine(rubPen, poilst[i], poilst[i + 1]);
                        }
                    }
                }
            } else if (useRubber == false && readPoi != null) {//按下中键后
                int plct = poilst.Count;
                if (plct == 0 | plct == 1) {
                } else {//两点及以上
                    for (int i = 0; i < plct; i++) {
                        if (i == plct - 1) {
                            g3.DrawLine(rubPen, poilst[0], poilst[i]);
                        } else {
                            g3.DrawLine(rubPen, poilst[i], poilst[i + 1]);
                        }
                    }
                }
            }

            if (rgKeyPrs && !isFill) {//按下右键并且没有填充过
                bt = scanLineFillAlg(0);
            } else if (isFill) {
                g8.DrawImage(bt, 0, 0);//保存入内存就不用反复画了
            }

            g0.DrawImage(bitmap, 0, 0);//display

        }

        private void fillPolyForm_Click(object sender, MouseEventArgs e) {

            if (e.Button == MouseButtons.Left) {//鼠标左击
                useRubber = true;
                Point readPoint = this.PointToClient(Control.MousePosition);//基于工作区的坐标
                readPoi = readPoint;
                intimePoiLbl.Text = readPoint.ToString();
                infoLebel.Text = "鼠标右键填充，Backspace撤销最后一个点";
                drawVertex(gp, readPoint); //画端点（顶点） 由于橡皮筋的覆盖，端点看不出来
                poilst.Add(readPoint);//加点进全局变量
                                      //rgKeyPrs = false; //原先无论有没有按右键都清空了

            } else if (e.Button == MouseButtons.Right) {  //右键
                rgKeyPrs = true;
                bt = scanLineFillAlg(0);//调用扫描线填充算法
                isFill = true;
                drawRim();//画边框
                infoLebel.Text = "中键确认选区，Del键清除所有点";
            } else if (e.Button == MouseButtons.Middle) {  //中键
                useRubber = false;
                this.Refresh();
                infoLebel.Text = "Del键可清除所有点";
                if (poilst.Count != 0)
                    g8.DrawImage(bt, 0, 0);
            }

        }

        private void fillPolyForm_MouseMove(object sender, MouseEventArgs e) {
            readPoi = this.PointToClient(Control.MousePosition);//基于工作区的坐标

            Graphics g5 = this.CreateGraphics();
            if (useRubber) {
                g5.Clear(BackColor);
                int plct = poilst.Count;
                if (plct == 0) {// ==0： pass
                } else if (plct == 1) {
                    g5.DrawLine(rubPen, poilst[0], readPoi);
                } else {//两点及以上
                    for (int i = 0; i < plct; i++) {
                        if (i == plct - 1) {//画到最后一点了
                            g5.DrawLine(rubPen, poilst[i], readPoi);
                            g5.DrawLine(rubPen, poilst[0], readPoi);
                        } else {
                            g5.DrawLine(rubPen, poilst[i], poilst[i + 1]);
                        }
                    }
                }

                if (rgKeyPrs) { //填充部分
                    g8.DrawImage(bt, 0, 0);
                }
            } else {
                if (isFill) {
                    g8.DrawImage(bt, 0, 0);
                }
            }
            intimePoiLbl.Text = readPoi.ToString();
        }

        private void fiiPolyForm_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Delete) {
                poilst.Clear();
                isFill = false;
                rgKeyPrs = false;
                //画的点也要清除
                this.Refresh();

            } else if (e.KeyCode == Keys.Back | e.KeyCode == Keys.Escape) {
                int plast = poilst.Count - 1;
                poilst.RemoveAt(plast);
                isFill = false;
                useRubber = true;
                this.Refresh();
            }

        }

        //扫描线填充核心代码
        private Bitmap scanLineFillAlg(int op) {
            bm = new Bitmap(ClientSize.Width, ClientSize.Height);
            g7 = Graphics.FromImage(bm);
            List<Point> plst = poilst;
            if (poilst.Count <= 1)
                return bm;//没有或一个点时

            //由点（序列）生成边表
            List<poEdge> cedges = new List<poEdge>();

            int pcount = plst.Count;
            for (int i = 0; i < pcount; i++) {
                if (i == pcount - 1) {
                    cedges.Add(new poEdge(plst[i], plst[0]));
                } else {
                    cedges.Add(new poEdge(plst[i], plst[i + 1]));
                }
            }
            List<int> ymls = new List<int>();
            foreach (poEdge pe in cedges) {
                int nymax = pe.ymax;
                if (!ymls.Contains(nymax)) {
                    ymls.Add(nymax);
                }
            }
            ymls.Sort();

            //构建了ET表
            edgeTable etv = new edgeTable(cedges);
            int yminh = etv.getYmin();
            int ymaxh = ymls[ymls.Count - 1];
            //int ymaxh = getPoiYmax(plst);
            //List<poEdge> ael = new List<poEdge>();
            edgeClass ael = new edgeClass();
            //第三步
            for (int yf = yminh; yf < ymaxh; yf++) {
                // if et[y]!=null: ael=et[y]
                if (etv.isCoutains(yf)) {
                    ael.AddEC(etv.getECinY(yf));
                }//3--1 第三步第1小步
                if (ael.notNull()) {
                    //两两配对，drawline
                    int ect = ael.eCount();
                    List<double> wb = new List<double>();//排序一下
                    for (int k = 0; k < ect; k++) {
                        wb.Add(ael.getEdgeX(k));
                    }
                    wb.Sort();//目前看来直接按照边配对需要较多的if语句，所以重新进行了排序

                    for (int j = 0; j < ect;) {//画线，

                        float x1 = Convert.ToSingle(wb[j]);
                        float x2 = Convert.ToSingle(wb[j + 1]);
                        PointF pt1 = new PointF(x1, yf);
                        PointF pt2 = new PointF(x2, yf);
                        g7.DrawLine(scPen, pt1, pt2);
                        j += 2;

                    }

                    if (ymls.Contains(yf + 1)) {
                        ael.delEdgeYm(yf);//删除ymax==y+1的点
                    }
                    ael.addEachX(); //x=x+dx

                }//3--2

            }
            return bm;
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
            gp.DrawPolygon(new Pen(Color.Blue,2), poi);
        }
        //取得所有y的最大值
        public int getPoiYmax(List<Point> plsts) {
            int pty_max = int.MinValue;
            foreach (Point pt in plsts) {
                if (pt.Y > pty_max)
                    pty_max = pt.Y;
            }
            return pty_max;
        }
		#endregion
    }

    

    #region 自定义类

    class poEdge {
        private int y_max;//y上端最大值
        private double xval;//x的值
        private double dxrv;//斜率倒数

        private int x_ups;//线段x的上界
        private int x_belw;//xmin
        private int y_belw;//ymin

        public poEdge() {

        }
        public poEdge(Point a, Point b) {
            if (a.X <= b.X) {//a.x比b.x小
                this.x_ups = b.X;
                this.x_belw = a.X;
            } else {
                this.x_ups = a.X;
                this.x_belw = b.X;
            }
            if (a.Y < b.Y) {//a.y比b.y小
                this.y_max = b.Y;
                this.y_belw = a.Y;
                this.xval = a.X;
            } else if (a.Y == b.Y) {
                //水平线的处理

            } else {
                this.y_max = a.Y;
                this.y_belw = b.Y;
                this.xval = b.X;
            }
            this.dxrv = (b.X - a.X) / (b.Y - a.Y + 0.0);


        }
        public poEdge(Point upside, Point below, int c) {//已知两端点各坐标的关系时，目前用不到
            this.y_max = upside.Y;
            this.xval = below.X;
            if (upside.X == below.X) {//水平线
            } else {
                this.dxrv = (upside.X - below.X) / (upside.Y - below.Y + 0.0);
            }
        }

        public int ymax {
            get { return this.y_max; }
            set { this.y_max = value; }
        }
        public double x { //double using
            get { return this.xval; }
            set { this.xval = value; }
        }
        public double dx {
            get { return this.dxrv; }
            set { this.dxrv = value; }
        }

        public int x_up {
            get { return this.x_ups; }
            set { this.x_ups = value; }
        }
        public int x_blw {
            get { return this.x_belw; }
            set { this.x_belw = value; }
        }
        public int y_blw {
            get { return this.y_belw; }
            set { this.y_belw = value; }
        }

    }

    class edgeClass {//多条边的类
        private int _id;
        private List<poEdge> edgs = new List<poEdge>();//edges list
        public edgeClass() {
            //很关键在于对边的操作上
        }
        public edgeClass(poEdge edg) {
            this.edgs.Add(edg);

        }
        public edgeClass(int idx, poEdge edg) {
            this._id = idx;//带id的边
            this.edgs.Add(edg);
        }

        public List<poEdge> Edges {
            get { return this.edgs; }
            set { this.edgs = value; }
        }
        public void Adde(poEdge ap) {
            this.edgs.Add(ap);
        }
        public void AddEC(edgeClass ecs) {//加一个edgeclass进来
            //加入是否有顺序问题？排序之后就没有了
            foreach (poEdge pe in ecs.Edges) {
                this.edgs.Add(pe);
            }
            this.sortEC();//调用自家的排序
        }
        public bool notNull() {
            if (this.edgs.Count == 0) {
                return false;//空
            }
            return true;//非空
        }
        public int eCount() {//len（）
            return this.edgs.Count;
        }
        public poEdge Index(int ix) {
            return this.edgs[ix];//在这里不处理ix索引越界的问题
        }
        public double getEdgeX(int ix) {
            if (ix >= edgs.Count) {  //外界输入时就控制不能超了
                return this.edgs[0].x;//出现这种情况是要处理跳的，这里只是看效果
            }
            return this.edgs[ix].x;
        }
        public void setEdgeX(int ix, double x_val) {
            this.edgs[ix].x = x_val;

        }
        public void addEachX() {
            foreach (poEdge pe in this.edgs) {
                pe.x = pe.x + pe.dx;
            }
        }
        public void delEdgeYm(int yf) {//删除边，AEL表用
            int k = 0;
            while (k < this.edgs.Count) {
                if (this.edgs[k].ymax == yf + 1) {
                    this.edgs.RemoveAt(k);
                    //k=k;
                } else {
                    k++;
                }
            }
        }
        public void sortEC() {//多重排序
            this.edgs = this.edgs.OrderBy(poEdge => poEdge.x).ThenBy(poEdge => poEdge.dx).ToList();
            //用了LINQ下的语法
        }

        //目前该方法用不到了
        public void inYmax(int yf) {//这一步需要反复调用呀，值得优化
            foreach (poEdge pe in this.edgs) {
                if (pe.ymax == yf + 1) {
                    this.edgs.Remove(pe);
                }//则AEL中满足ymax =y＋1且上端点为非极值点的边及ymax = y的上端点为极值点的边删去
            }
        }

    }

    //构建ET是个难点呀，et的数据结构也影响了edge的数据结构，因为为了更方便地构建ET，edge也需要调整。
    class edgeTable { 
        
        private List<int> indyEC=new List<int>();//对应扫描线的y
        private List<edgeClass> sEC=new List<edgeClass>();//每个indyEC的index下对应的edgeClass
        public edgeTable() {
        }
        public edgeTable(List<poEdge> pelsts) {
            foreach (poEdge pet in pelsts) {
                int y_bw = pet.y_blw;
                if (indyEC == null) {//开始为空时
                    this.indyEC.Add(y_bw);
                    sEC.Add(new edgeClass(y_bw, pet));
                }
                else if (indyEC.Contains(y_bw)) {//已经有元素了;
                    int k = indyEC.IndexOf(y_bw);
                    sEC[k].Adde(pet);
                } else {
                    indyEC.Add(y_bw);
                    sEC.Add(new edgeClass(y_bw, pet));
                }
            }

            foreach (edgeClass ecl in sEC) {
                ecl.sortEC();
            }//填充进去后再排序

        }
        public int getYmin() {//indEC为空时，j不会有值，直接跳过foreach，km=maxvalue
            int km = int.MaxValue;
            foreach (int j in indyEC) {
                if (j < km)
                    km = j;
            }
            return km;
        }
        public int getYmax() {
            int kmi = int.MinValue;
            foreach (int j in indyEC) {
                if (j > kmi)
                    kmi = j;
            }
            return kmi;
        }
        public bool isCoutains(int yf) {//notNull
            if (indyEC.Contains(yf)) {
                return true;
            } else {
                return false;
            }
        }
        public edgeClass getECinY(int yf) {
            int ecIndex = indyEC.IndexOf(yf);
            return sEC[ecIndex];
        }

    }

    #endregion


}
