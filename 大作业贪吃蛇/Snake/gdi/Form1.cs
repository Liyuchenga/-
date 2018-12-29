//第五章Snake.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
namespace gdi
{
    
    public partial class Form1 : Form
    {
        #region 成员变量
        public Snake snake;
        public Food1 food1;
        public Food2 food2;
        public Barrier barrier;
        public bool Refreshed;
        #endregion 成员变量

        #region 方法
        public Form1()
        {
            InitializeComponent();
            Control.ScoresChanged += //将委托分配给事件,建立事件关联
                delegate()
                {
                    label5.Text = Control.Scores.ToString();
                };
        }
     
        private void Form1_Load_1(object sender, EventArgs e)
        {
            //DoubleBuffered = true;
            Refreshed = false;
            this.TopMost = true;
            snake = new Snake();
            food1 = new Food1();
            food2= new Food2();
            barrier = new Barrier();
           // control = new Control();
            Control.Interval = 20;
            Control.Margin = 10;
            Control.GridNum = 18;
            Control.G = this.CreateGraphics();
            Control.FoodType = new Stack<IFood>();

            using (StreamReader sr = File.OpenText(Control.dataFilePath))//读取文件
            {
                this.label12.Text = sr.ReadLine().Trim();
                Control.worldRecord = int.Parse(sr.ReadLine());
                this.label10.Text = Control.worldRecord.ToString();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            this.label1.Text = "蛇长：" + numericUpDown1.Value;
            this.label2.Text = "速度：" + numericUpDown2.Value;
            Barrier.BarrierNum =  (int)numericUpDown3.Value;
            this.label14.Text = "";
            this.numericUpDown1.Enabled = false;
            this.numericUpDown2.Enabled = false;
            this.numericUpDown3.Enabled = false;
            this.textBox1.Enabled = false;
            this.button1.Enabled = false;
            int speed = (int)numericUpDown2.Value;
            Control.CurrentDirection = Control.Direction.Up;//为了防止蛇不反走
            
            timer1.Interval = 1000 - (speed - 1) * 100;//speed越大，Interval越小
            // 游戏结束后，再按游戏开始，需要重新画格子、蛇和食物
            // 画格子
            Control.DrawGrid(this);
            // 画蛇
            snake.GenerateSnake((int)numericUpDown1.Value);
            // 画食物
            Control.FoodType.Clear();//清空栈中数据
            food1.GenerateSolid();//无须参数了
            Control.FoodType.Push((IFood)food1);
            food2.GenerateSolid();
            Control.FoodType.Push((IFood)food2);
            //画障碍物
            for(int i=0;i<Barrier.BarrierNum;i++)
                barrier.GenerateSolid();//无须参数了。为了将来扩展，这里可以用数组
            timer1.Start();

        }
       
        protected override void OnPaint(PaintEventArgs e)
        {
            if (!Refreshed)
            {
                Barrier.BarrierNum = (int)numericUpDown3.Value;
                base.OnPaint(e);//引发Paint事件处理（处理该事件时候调用Form1_Paint方法）
                Barrier.BarrierNum = (int)numericUpDown3.Value;
                //画格子
                Control.DrawGrid(this);
                //画蛇
                snake.GenerateSnake((int)numericUpDown1.Value);
                //画食物
                food1.GenerateSolid();//无须固定食物位置了
                food2.GenerateSolid();
                //画障碍物
                for (int i = 0; i < Barrier.BarrierNum; i++)
                    barrier.GenerateSolid();//无须固定坐标了
                //g.Dispose();
                Refreshed = true;
            }
  
        }
        protected override bool ProcessDialogKey(Keys keyData)//重写键盘处理方法，第八章多态内容
        {
            if (keyData == Keys.Enter)//回车，继续
                timer1.Start();
            else if (keyData == Keys.Space)//空格，暂停
                timer1.Stop();
            else
                Control.PressingDirection = (Control.Direction)keyData;//

            // return base.ProcessDialogKey(keyData);
            return false;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //垂直两键键值的差的绝对值为1或3
            int diff = Math.Abs((int)Control.CurrentDirection - (int)Control.PressingDirection);

            if (diff == 1 || diff == 3)//垂直，就改变方向
            {
                Control.CurrentDirection = (Control.Direction)Control.PressingDirection;
            }
            snake.SnakeMove(this);
        }
        #endregion 方法
    }
}
