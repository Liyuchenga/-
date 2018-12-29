using System.Drawing;
using System.Collections.Generic;
using System;
using System.IO;
namespace gdi
{
    public static class Control
    {
        #region 成员变量
        private static Graphics g;  //画板
        private static int interval;//格子连长
        private static int gridNum;//水平或垂直方向格子个数
        private static Direction currentDirection =Direction.Up;//当前蛇运动方向,初始方向设置为向上
        private static Direction pressingDirection;//当前按键所表示的方向
        private static int scores; //得分
        private static int margin; //边距
        private static GridType[,] grids;//格子（位置，类型）
        private static Stack<IFood> foodType;
        public delegate void SnakeGetScoresHandler();//定义委托
        public static event SnakeGetScoresHandler ScoresChanged;//定义事件

        public static int worldRecord = 0;//记录
        public static string dataFilePath = Directory.GetCurrentDirectory() + "\\data.txt";//数据文件路径
        #endregion 成员变量

        #region 属性
        public static Stack<IFood> FoodType
        {
            get { return Control.foodType; }
            set { Control.foodType = value; }
        }
        public static GridType[,] Grids
        {
            get { return grids; }
            set { grids = value; }
        }
        public static Graphics G
        {
            get { return Control.g; }
            set { Control.g = value; }
        }
        internal static Direction PressingDirection
        {
            get { return Control.pressingDirection; }
            set { Control.pressingDirection = value; }
        }
        internal static Direction CurrentDirection
        {
            get { return Control.currentDirection; }
            set { Control.currentDirection = value; }
        }
        public static int Scores
        {
            get { return Control.scores; }
            set { Control.scores = value;//当scores改变时
            if (ScoresChanged != null)  //触发事件ScoreChanged
                ScoresChanged();
            }
        }
        public static int Margin
        {
            get { return Control.margin; }
            set { Control.margin = value; }
        }

        public static int GridNum
        {
            get { return Control.gridNum; }
            set { Control.gridNum = value; }
        }
        public static int Interval
        {
            get { return Control.interval; }
            set { Control.interval = value; }
        }
        #endregion 属性 //
        public enum GridType
        {
            Empty,      //空
            SnakeHead,  //蛇头
            SnakeBody,  //蛇身
            Food,      //食物
            Barrier,   //障碍物
        }
        public enum Direction //定义表示方向键值的枚举类型
        {
            Left = 37,//左键键值为37
            Up,      //上键键值为38   
            Right,   //右键键值为39      
            Down,    //下键键值为40，垂直两键键值的差的绝对值为1或3
        }
 
        //public static void AddScore(Form1 form,int score)//引入事件后，这个方法不要了
        //{
        //    Scores+=score;
        //    form.label5.Text= (Scores * 100).ToString();//每吃一个食物得100分
        //}
        public static void DrawGrid(Form1 form)
        {
            grids = new GridType[gridNum, gridNum];//定义表示格子属性的二维数组
            G.FillRectangle(new SolidBrush(form.BackColor), 0, 0, Margin + GridNum * 20, Margin + GridNum * 20);
            Pen p = new Pen(Color.Blue, 1);
            for (int i = 0; i <= GridNum; i++)//格子
            {
                G.DrawLine(p, Margin + i * Interval, Margin, Margin + i *Interval, Margin + GridNum * 20);
                G.DrawLine(p, Margin, Margin + i * Interval, Margin + GridNum * 20, Margin + i *Interval);
            }
            //给每个格子添加GridType.empty属性
            for (int i = 0; i < gridNum; i++)//初始化格子属性为Empty
                for (int j = 0; j <gridNum; j++)
                    grids[i, j] = GridType.Empty;
        }
        public static void GameOver(Form1 form)
        {
            form.label14.Text = "Game Over!";
            form.numericUpDown1.Enabled = true;
            form.numericUpDown2.Enabled = true;
            form.button1.Enabled = true;
            form.numericUpDown3.Enabled = true;

            if (scores > worldRecord)//当前玩家分值大于记录时存档
                using (StreamWriter sw = File.CreateText(dataFilePath))
                {
                    sw.WriteLine(form.textBox1.Text.Trim());

                    sw.WriteLine(scores);
                    form.label10.Text = scores.ToString();
                    form.label12.Text = form.textBox1.Text.Trim();
                    worldRecord = scores;
                }

            return;
        }
    }
}
