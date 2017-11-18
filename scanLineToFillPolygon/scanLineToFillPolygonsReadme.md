**scanLineToFillPolygon**

## 功能：
- [x] 鼠标选点，有橡皮筋跟随效果
- [x] 扫描线算法填充多边形

点击鼠标左键选鼠标所在点作为多边形的一个端点，鼠标移动时有橡皮筋跟随效果；右键填充多边形；中键确认多边形形状，点击中键后移动鼠标橡皮筋不会再跟随，相当于ArcMap中已经完成了一个多边形的创建。Backspace退格键和Esc键可撤销最后一个点的选取。Del键删除所有已经选择的点。

---
## 窗体控件说明

- fillPolyForm  主窗体
- infoLebel  显示提示信息的文本标签；information lebel
- intimePoiLbl  显示鼠标实时位置的标签；没有进行坐标转换，直接用窗体坐标系统。

界面模仿了老师演示程序的界面，感觉这样的布局很好.在用pictureBox的时候在鼠标点击和移动的监听方面遇到了问题，于是没有用pictBox控件。直接在窗体上画图。
因为鼠标移动时且使用橡皮筋时刷新频率较高，填充时闪烁严重，因此按中键才能较好地保存填充结果。代码中没怎么用普通数组，基本上都用可变数组List。填充色和橡皮筋颜色可以在代码14、15行改。

## 参考资料

[C#中的结构体与类的区别 ](http://www.cnblogs.com/to-creat/p/5268729.html)

[C#键盘事件处理](http://www.cnblogs.com/greatverve/archive/2012/05/15/KeyCode.html)

[C# GDI+双缓冲技术(防止移动时，窗口闪烁)](http://blog.csdn.net/soarandy/article/details/5038644)