//第五章Snake.cs 
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;//添加一个引用，否则无法使用画图相关资源

namespace gdi
{
      
    public class Snake
    {
        #region 成员变量
        private int x;      //蛇头在网格中的x方向位置
        private int y;      //蛇头在网格中的y方向位置
        private int snakeLong;//蛇身（不包含头）的长度
        private Queue<Point> queuePoint;//对列
        #endregion 成员变量

        public Snake()
        {
            queuePoint = new Queue<Point>();
        }
        public void GenerateSnake(int snakeLong)
        {
            x = Control.GridNum/ 2 + 2;
            y = Control.GridNum / 2 + 2;
            this.queuePoint.Clear();
            SolidBrush b2 = new SolidBrush(Color.Red);
            SolidBrush b3 = new SolidBrush(Color.Gray);
            this.snakeLong = snakeLong;
            Control.G.FillRectangle(b2, 11 + x * Control.Interval, 11 + y * Control.Interval, 18, 18);//head
            Control.Grids[x, y] = Control.GridType.SnakeHead;
            this.queuePoint.Enqueue(new Point(x, y));

            for (int i = 1; i <= snakeLong; i++)
            {
                Control.G.FillRectangle(b3, 11 + x * Control.Interval - i * 20, 11 + y * Control.Interval, 18, 18);
                this.queuePoint.Enqueue(new Point(x - i, y));//坐标入队列
                Control.Grids[x - i, y] = Control.GridType.SnakeBody;//将该格子设置为蛇身属性
            }
        }
        public void SnakeMove(Form1 form)
        {
            int i;
            SolidBrush b2 = new SolidBrush(Color.Red);     //蛇头
            SolidBrush b1 = new SolidBrush(Color.Green);   //食物
            SolidBrush b3 = new SolidBrush(Color.Gray);    //蛇身
            SolidBrush b4 = new SolidBrush(form.BackColor);//用于填充的背景色
            if (Control.CurrentDirection == Control.Direction.Up)
            { y--; }
            if (Control.CurrentDirection == Control.Direction.Down)
            { y++; }
            if (Control.CurrentDirection == Control.Direction.Left)
            { x--; }
            if (Control.CurrentDirection == Control.Direction.Right)
            { x++; }
            if (x == 18 || x == -1 || y == -1 || y == 18)//遇到墙壁,这个坐标要放到前面判断，否则会出现下标溢出
            {
                form.timer1.Stop();
                Control.GameOver(form);
                return;//结束就返回，不然程序仍会往下执行，蛇仍会继续前进一格

            }
            if (Control.Grids[x,y]==Control.GridType.Food)            //吃到食物,但不知是吃到哪一种
            {
                snakeLong++;
                int j = (int)queuePoint.LongCount() - 1;      //蛇尾的坐标入队
                queuePoint.Enqueue(new Point(queuePoint.ElementAt(j).X, queuePoint.ElementAt(j).Y));
                foreach (IFood tmp in Control.FoodType)//如果还要添加其它各类的食物，这段代码无须修改
                {
                    
                    if (tmp.FoodX == x && tmp.FoodY == y)
                    {
                        tmp.Eaten = true;
                        //Control.AddScore(form, tmp.Score);//引入事件后,不用AddScore方法了
                        Control.Scores += tmp.Score*100;//直接用接口类型的Score累加到Control.Scores,事件自动刷新显示
                    }
                }
            }
            //遇到障碍物或蛇身
            if (Control.Grids[x, y] == Control.GridType.Barrier)
            {
                form.barrier.ResistSnake(form);
                return;//结束就返回，不然程序仍会往下执行，蛇仍会继续前进一格
            }
           

            Control.G.FillRectangle(b2, 11 + x * Control.Interval, 11 + y * Control.Interval, 18, 18);
            queuePoint.Enqueue(new Point(x, y));
            Control.Grids[x , y] = Control.GridType.SnakeBody;//将该格子设置为蛇身属性

            int x1, y1;
            for (i = 1; i <= snakeLong; i++)
            {
                x1 = queuePoint.First().X;
                y1 = queuePoint.First().Y;
                queuePoint.Dequeue();
                Control.G.FillRectangle(b3, 11 + x1 * Control.Interval, 11 + y1 * Control.Interval, 18, 18);
                queuePoint.Enqueue(new Point(x1, y1));
            }
            x1 = queuePoint.First().X;
            y1 = queuePoint.First().Y;

            queuePoint.Dequeue();//最后一格用背景色填掉
            Control.Grids[x, y] = Control.GridType.Empty;//将该格子设置为空属性
            Control.G.FillRectangle(b4, 11 + x1 * Control.Interval, 11 + y1 * Control.Interval, 18, 18);

            foreach (IFood tmp in Control.FoodType)//如果某一种食物被吃了，则重新产生
            {
                if (tmp.Eaten)
                    ((Solid)tmp).GenerateSolid();//如果再添加新的食物类型，代码无须修改
            }
        }
    }
}
