//第8章Food.cs 改了Food 的 x,y和GenerateSolid()
using System.Drawing;

namespace gdi
{
    public class Solid
    {
        #region 成员变量
        private int x; //X坐标
        private int y; //Y坐标
        private Color color;//颜色
        private Control.GridType solidType;//添加类型变量
        #endregion 成员变量

        #region 属性
        public Control.GridType SolidType
        {
            get { return solidType; }
            set { solidType = value; }
        }
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        public int X
        {
            get { return x; }
            set { x = value; }
        } 
        #endregion 属性
        public virtual void Initialize()
        { }
        public virtual void FinallyHandle()
        { }
        public void GenerateSolid()
        {
            Initialize();
            System.Random rand = new System.Random();
            int x1, y1;
            x1 = rand.Next(Control.GridNum);
            y1 = rand.Next(Control.GridNum);
            while (Control.Grids[x1, y1] != Control.GridType.Empty)//不再固定食物和障碍物位置，让系统随机产生
            {
                x1 = rand.Next(Control.GridNum);
                y1 = rand.Next(Control.GridNum);
            }
            x = x1; y = y1;//将新坐标赋值给成员变量
            Control.Grids[x, y] = solidType;
            Control.G.FillRectangle(new SolidBrush(color), 11 + x * Control.Interval, 11 + y * Control.Interval, 18, 18);
            FinallyHandle();
        }
       
    }
}
