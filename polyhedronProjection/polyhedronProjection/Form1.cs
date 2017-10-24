using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace polyhedronProjection {
    public partial class projZForm : Form {
        public projZForm() {
            InitializeComponent();
            this.Paint += new PaintEventHandler(this.projZForm_Paint); //初始化
            this.perspPrjraBtn.CheckedChanged += new EventHandler(this.radBtn_CheckedChange);//监听单选框的改变，下同。perspPrj：透视投影；parallelPrj：平行投影
            this.parallelPrjraBtn.CheckedChanged += new EventHandler(this.radBtn_CheckedChange);//

        }
        private bool isRotate = false;//是否在旋转中
        private bool showAxis = true;  //是否显示坐标轴
        private int projWay = 0; //投影方式：0-透视；1-平行；
        private int mtxTrfw = 300; //坐标原点相对于窗体坐标系的移动，x方向上
        private int mtxTrfh = 260; //y方向上
        private int stepLen = -2;//旋转步长,旋转方向原因，采用负数
        
        private Point coordtPoi = new Point(300, 260);//转换后坐标点
        private Point[] dimPlst; //降维后的点序列
        private thPoint viewtPoi;//视点坐标或方向 view coordinate threePoint
        private thPoint deftPoi; //默认三维点 default thPoint
        private thPoint[] rtList; //旋转时迭代用的三维点序列
        private Cube deftCube; //默认长方体
        private Graphics gpat;
        private Graphics g;
        private Pen pen = new Pen(Color.Blue, 2); //画投影后边的笔
        
        private void projZForm_Paint(object sender, PaintEventArgs eag) {
            gpat = eag.Graphics;
            if (showAxis) { //写坐标轴的x和y
                gpat.DrawString("x", new Font("微软雅黑", 15), Brushes.Black, new Point(mtxTrfw + 155, mtxTrfh - 2));
                gpat.DrawString("y", new Font("微软雅黑", 15), Brushes.Black, new Point(mtxTrfw - 20, mtxTrfh - 156));
            }
            Matrix mat = new Matrix(1, 0, 0, -1, 0, 0);//沿X轴翻转
            gpat.Transform = mat;
            gpat.TranslateTransform(mtxTrfw, -mtxTrfh);
            if (showAxis) { drawAxis(gpat); //画坐标轴
            }
            if (!isRotate) {
                projection();
            } else {
                drawDmrCubEdge(gpat, dimPlst);
            }
        }
        
        private void projZForm_Load(object sender, EventArgs e) {
            g = CreateGraphics();
            Matrix mat = new Matrix(1, 0, 0, -1, 0, 0);//沿X轴翻转
            g.Transform = mat;
            g.TranslateTransform(300, -260);
            //初始化一些默认值
            viewtPoi = new thPoint(200, 200, 200);
            deftPoi = new thPoint(20, 30, 40);
            deftCube = new Cube(new thPoint(20, 30, 40), 80, 50, 40);
            rtList = deftCube.PoiList;
        }

        //画坐标轴
        public void drawAxis(Graphics g) { 
            Pen axisPen = new Pen(Color.Black, 1);
            g.DrawLine(axisPen, 0, 0, 150, 0);
            g.DrawLine(axisPen, 143, 4, 150, 0);
            g.DrawLine(axisPen, 143, -4, 150, 0);
            g.DrawLine(axisPen, 0, 0, 0, 150);
            g.DrawLine(axisPen, 4, 143, 0, 150);
            g.DrawLine(axisPen, -4, 143, 0, 150);
        }

#region 投影和旋转的核心代码和封装代码
        // 投影
        private Point usingProject(thPoint pjtPoi) {
            int xp, yp;//viewtPoi:视点坐标或者平行投影的方向
            try {
                if (viewtPoi.Z == pjtPoi.Z) {
                    pjtPoi.Z = pjtPoi.Z - 1;
                } 
                //参考了课本58页的式子
                if (projWay == 0) { //透视投影  
                        xp =Convert.ToInt32( viewtPoi.X + (pjtPoi.X - viewtPoi.X) * viewtPoi.Z / (viewtPoi.Z - pjtPoi.Z+0.0));
                        yp = Convert.ToInt32(viewtPoi.Y + (pjtPoi.Y - viewtPoi.Y) * viewtPoi.Z / (viewtPoi.Z - pjtPoi.Z+0.0));
                        //double xd = viewtPoi.X + (pjtPoi.X - viewtPoi.X) * viewtPoi.Z / (viewtPoi.Z- pjtPoi.Z + 0.0);//double yd = viewtPoi.Y+ (pjtPoi.Y - viewtPoi.Y) * viewtPoi.Z/ (viewtPoi.Z- pjtPoi.Z + 0.0);
                        return new Point(xp, yp);
                } else if (projWay == 1) { //平行投影
                        xp = Convert.ToInt32(pjtPoi.X - pjtPoi.Z * viewtPoi.X / (viewtPoi.Z+0.0));
                        yp = Convert.ToInt32(pjtPoi.Y - pjtPoi.Z * viewtPoi.Y / (viewtPoi.Z+0.0));
                        return new Point(xp, yp);
                } else {
                        MessageBox.Show("投影方式有错");
                        return new Point(0, 0);
                }
            }catch(Exception ecp) {
                MessageBox.Show(ecp.ToString());
                return new Point(0, 0);
            }
            
        }
       
        
        //对三维点序列一个个投影
        private Point[] projOnebyOne(thPoint[] thws) {
            Point[] afterProj = new Point[8];
            if (thws.Length == 8) {
                for (int i = 0; i < 8; i++) {
                    afterProj[i] = usingProject(thws[i]);
                }
                return afterProj;
            }
			//对非8个顶点的多面体没有增加处理
            return afterProj;
        }

        public void projection() { //更新点序列,采用默认cube
            rtList = deftCube.PoiList;
            dimPlst = projOnebyOne(rtList);//降维后的点序列
            drawDmrCubEdge(g, dimPlst);//画出降维后的多面体的边
        }

        private void drawDmrCubEdge(Graphics g, Point[] ap) {//画出投影后的点
            try {
                if (ap.Length == 8) {
                    //if (ap[0]=NULL{}
                    Point[] tenPoi = { ap[0], ap[1], ap[4], ap[2], ap[0], ap[3], ap[5], ap[7], ap[6], ap[3] };
                    g.DrawLines(pen, tenPoi);//10 points
                    g.DrawLine(pen, ap[1], ap[5]);
                    g.DrawLine(pen, ap[4], ap[7]);
                    g.DrawLine(pen, ap[2], ap[6]);
                }
            }catch(Exception exp) {
                MessageBox.Show(exp.ToString());
            }
        }
        

        //旋转；传入三维点，传出三维点   之后考虑float
        private thPoint rotateOnce(thPoint iput) {// rotate y
            int alpha = stepLen; //参考课本52页 关于y轴旋转的式子
            int outPox = Convert.ToInt32(iput.X * Math.Cos(alpha / 180.0 * Math.PI) + iput.Z * Math.Sin(alpha / 180.0 * Math.PI));
            int outPoz = Convert.ToInt32(-iput.X * Math.Sin(alpha / 180.0 * Math.PI) + iput.Z * Math.Cos(alpha / 180.0 * Math.PI));
			//将Math.Cos(alpha / 180.0 * Math.PI)用全局变量记录，在传入步长变化时再算可以减少计算量
			//旋转部分可以由点进行迭代，有一定误差，也可以递增alpha，后者计算量更大，
            return new thPoint(outPox, iput.Y, outPoz);
        }
        
        //对点序列进行旋转，画出旋转后的边，并传出旋转后的点
        private thPoint[] rotateDmrCub(thPoint[] dcLst) {
            thPoint[] rdcLst = new thPoint[8];
            for (int i = 0; i < 8; i++) {
                rdcLst[i] = rotateOnce(dcLst[i]);
            }
            dimPlst = projOnebyOne(rdcLst);//降维后的点序列
            
            return rdcLst;
        }
#endregion       
        
        //timer事件
        private void rotateTimer_Tick(object sender, EventArgs e) {
            this.Refresh();
            rtList =rotateDmrCub(rtList);
            //Thread.Sleep(100);
        }

# region 按钮交互部分
        //投影按钮
        private void projNowBtn_Click(object sender, EventArgs e) {
            //检查初始化情况，得到投影降维后的点序列，按顺序画出 
            if (checkVtcdtIput()) { //检查视点输入
                if (isRotate) {//还在自动旋转或者没有恢复初始位置时
                    isRotate = false;
                    rotateTimer.Stop();
                }
                this.Refresh();
                projection();// 投影
            }
        }

        //自动旋转 按钮
        private void autoRotateBtn_Click(object sender, EventArgs e){
            if (int.TryParse(rotaSteptLentBox.Text, out stepLen)) {
                stepLen = -stepLen;//在旋转中改变rotaSteptLentBox框的值不会改变旋转步长，需要再次点击旋转才行，本工程默认左手系旋转（因为老师的演示代码是右手系），故采用负数，如需右手系，在输入框输入负数。
                isRotate = true;
                rotateTimer.Start();
                stopRotateBtn.Enabled = true;
            } else {
                MessageBox.Show("步长输入不符合，请输入正整数");
            }
            
        }
        //停止旋转 按钮
        private void stopRotateBtn_Click(object sender, EventArgs e) {
            rotateTimer.Stop();
            
        }
        //初始位置 按钮
        private void gotoStPoiBtn_Click(object sender, EventArgs e) {
            rotateTimer.Stop();
            g.Clear(this.BackColor);//用背景色填充
            if (checkVtcdtIput()) {
                projection();
                isRotate = false;
                this.Refresh();
            }
        }
        #endregion

        public bool checkVtcdtIput() { //检查视点的输入
            int vx, vy, vz;
            if (int.TryParse(viewPoiXtBox.Text, out vx)) {
                viewtPoi.X = vx;
            } else {
                MessageBox.Show("视点（投影方向）的x值输入有误，请重新输入！");
                return false;
            }
            if (int.TryParse(viewPoiYtBox.Text, out vy)) {
                viewtPoi.Y = vy;
            } else {
                MessageBox.Show("视点（投影方向）的y值输入有误，请重新输入！");
                return false;
            }
            if (int.TryParse(viewPoiZtBox.Text, out vz)) {
                viewtPoi.Z = vz;
            } else {
                MessageBox.Show("视点（投影方向）的z值输入有误，请重新输入！");
                return false;
            }
            if (deftCube.isInZoom(viewtPoi)) {
                MessageBox.Show("视点（投影方向）在多面体与坐标原点区域内，请重新输入坐标！","提示",MessageBoxButtons.OK);
                viewtPoi.X = 200;
                viewtPoi.Y = 200;
                viewtPoi.Z = 200;
                viewPoiXtBox.Text = "200";
                viewPoiYtBox.Text = "200";
                viewPoiZtBox.Text = "200";
                return false;
            }
            return true;
        }

        //监控radioButton选项改变的事件
        public void radBtn_CheckedChange(object sender, EventArgs e) {
            if (!((RadioButton)sender).Checked) {
                return;
            }
            if (((RadioButton)sender).Text.ToString() == "平行投影")
                projWay = 1;
            else
                projWay = 0;
            
            dimPlst = projOnebyOne(rtList);//降维后的点序列
            this.Refresh();
            
        }
        
        //监测复选框[显示坐标轴]的变化
        private void showAxisCkBox_CheckedChanged(object sender, EventArgs e) {
            if (showAxisCkBox.Checked) {
                showAxis = true;
            } else {
                showAxis = false;
            }
            this.Refresh();
        }
    }

#region 自定义类
    //三维点
    class thPoint { 
        private int Xt;
        private int Yt;
        private int Zt;

        public thPoint() {
            this.Xt = 0;//构造方式
            this.Yt = 0;
            this.Zt = 0;
        }
        public thPoint(int x,int y,int z) {
            this.Xt = x;//构造方式
            this.Yt = y; 
            this.Zt = z;
        }
        public int X {
            get { return this.Xt; }
            set { this.Xt = value; }
        }
        public int Y {
            get { return this.Yt; }
            set { this.Yt = value; }
        }
        public int Z {
            get { return this.Zt; }
            set { this.Zt = value; }
        }
        public override string ToString() {
            return "{" + Xt.ToString() + "," + Yt.ToString() + "," + Zt.ToString() + "}";
        }
    }
    //二维边;三维边 在我的工程下不是很方便调用就删了
    
    //长方体
    class Cube {
        private thPoint corethPoi; //离坐标原点最近的点
        private int cubeL; //长-对应x
        private int cubeW; //宽-对应z
        private int cubeH; //高-对应y
        public Cube() {
            this.corethPoi = new thPoint(0, 0, 0);
            this.cubeL = 50;//长   x
            this.cubeW = 50;//宽   z
            this.cubeH = 50;//高   y
        }
        public Cube(thPoint cPoi,int cL,int cW,int cH) {
            this.corethPoi =cPoi;
            this.cubeL = cL;
            this.cubeW = cW;
            this.cubeH = cH;
        }
        public Cube(int xt,int yt,int zt, int cL, int cW, int cH) {
            this.corethPoi =new thPoint(xt,yt,zt);
            this.cubeL = cL;
            this.cubeW = cW;
            this.cubeH = cH;
        }
        public thPoint CorePoint {//获取和设置 点corethPoi
            get { return this.corethPoi; }
            set { this.corethPoi = value; }
        }
        public thPoint[] PoiList {  //获取顶点序列
            get {
                thPoint[] poiLst = { this.corethPoi, new thPoint(corethPoi.X + cubeL, corethPoi.Y,corethPoi.Z),
                    new thPoint(corethPoi.X, corethPoi.Y + cubeH, corethPoi.Z),
                    new thPoint(corethPoi.X, corethPoi.Y, corethPoi.Z + cubeW),
                    new thPoint(corethPoi.X + cubeL, corethPoi.Y + cubeH, corethPoi.Z),
                    new thPoint(corethPoi.X + cubeL, corethPoi.Y, corethPoi.Z + cubeW),
                    new thPoint(corethPoi.X, corethPoi.Y + cubeH, corethPoi.Z + cubeW),
                    new thPoint(corethPoi.X + cubeL, corethPoi.Y + cubeH, corethPoi.Z + cubeW) };

                return poiLst;
            }
        }

        public bool isInCube(thPoint tpi) {
            //get{//在长方体内
			if (tpi.X<=corethPoi.X+cubeL && corethPoi.X<=tpi.X ){
					if (tpi.Y<=corethPoi.Y+cubeH && corethPoi.Y<=tpi.Y ){
                        if (tpi.Z <= corethPoi.Z + cubeW && corethPoi.Z <= tpi.Z)
                            return true;
					}
				}
			return false;	
				
		}
		public bool isInZoom(thPoint tpi) {//在长方体区域内
            if (tpi.X<=corethPoi.X+cubeL && tpi.X>=0){
					if (tpi.Y<=corethPoi.Y+cubeH && tpi.Y >= 0 ){
                        if (tpi.Z <= corethPoi.Z + cubeW && tpi.Z >= 0)
                            return true;
					}
				}
			return false;	
			
		}
	}

#endregion  
}
