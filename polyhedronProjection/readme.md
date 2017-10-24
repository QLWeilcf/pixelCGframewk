lcf--2017-10-24 update

工程说明：
## 1.整体运行逻辑
改变坐标系，初始化一些默认值，画坐标轴，初始化一个长方体，用默认的视点投影该长方体。交互上包括单选框、输入框和按钮；单选框和输入框都有初始值，代码会对输入值进行检查，单选框的值改变代码会应答，能直接看到效果，改变时传入的值为改变前的空间点坐标；按钮可以直接点击。输入框的值改变在点击按钮时生效。

## 2.主要命名说明
projZForm--投影的主窗体；
perspPrjraBtn--透视投影单选框perspective project radioButton
viewPoiXtBox--视点x坐标输入框，view Point X textBox 类似地，viewPoiYtBox viewPoiZtBox
rotaSteptLentBox--旋转步长输入框
projNowBtn--投影按钮

自定义类：
- thPoint 
三维点，使用方式和内置的Point基本相同。重写了ToString方法，和Point的ToString()相似，用于debug，检查三维点坐标

- Cube
长方体，包含4个属性，离坐标原点最近的点corethPoi以及长宽高，其他点采用四个属性算出来，有3种构造方式；方法isInCube判断一个三维点是否在长方体内。
默认视点：viewtPoi = new thPoint(200, 200, 200);
默认长方体数据：deftCube = new Cube(new thPoint(20, 30, 40), 80, 50, 40);
默认步长：2；
其他控件以及变量名称的意义请参见代码注释。
## 3.主要函数说明
-  usingProject 对一个三维点进行投影，返回投影后的二维点，是投影的细节实现部分。
- projOnebyOne 对三维点序列一个个投影，调用usingProject ，目前只处理8个点的情况，不是8个点会影响下一步。
- projection() 投影多面体并画出，更新点序列，点击投影按钮主要调用的函数。这个函数调用了projOnebyOne和drawDmrCubEdge
- drawDmrCubEdge 画出投影后的点，要求输入的三维点序列为8个点的
- rotateOnce 旋转的细节实现；传入三维点，传出旋转后的三维点坐标 
- rotateDmrCub 对点序列进行旋转，画出旋转后的边，并传出旋转后的点
- checkVtcdtIput 检查视点的输入，返回true或false

## 4.一些备注
-  点击投影按钮时采用的是默认的空间多面体，因此采用的点坐标不是在自动旋转中以及停止时的点，不会展示多面体转到某个位置时改变视点的效果。这种效果可以在函数projection()中改写rtList = deftCube.PoiList;句实现
- 
## 5.目前的一些问题
- [ ] 目前的效果还是有些闪。
- [ ] 代码的拓展性比较差，目前不能方便地把Cube改为其他多面体，因为点的关系和边的联结性在画图时直接用了，没怎么封装。在函数projOnebyOne、drawDmrCubEdge、rotateDmrCub都只处理8个点的情况
- [ ] 旋转时采用的点都是Point，整数值，存在一定的误差累积，目前没有采用PointF。
- [ ] 全局变量用得有些多
