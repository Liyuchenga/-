using System.Drawing;
namespace gdi
{
    public interface IFood
    {
        bool Eaten      //判断食物是否被蛇吃掉了
        { get; set; }
        int FoodX       //用于记录接口类型食物坐标
        {get;set;}
        int FoodY
        {get; set;}
        int Score       //得分
        { get; set;}

    }
    public class Food1:Solid,IFood
    {
        private int foodX;
        private int foodY;
        private int score;
        private bool eaten;

        public bool Eaten
        {
            get { return eaten; }
            set { eaten = value; }
        }
        public int FoodX
        {
            get { return foodX; }
            set { foodX = value; }
        }
        public int FoodY
        {
            get { return foodY; }
            set { foodY = value; }
        }
        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public override void Initialize()
        {
            Color = Color.Green;
            SolidType = Control.GridType.Food;
            Score = 1;
            Eaten = false;
        }
        public override void FinallyHandle()
        {
            foodX = X;
            foodY = Y;
        }
    }

    public class Food2 : Solid, IFood
    {
        private int foodX;
        private int foodY;
        private int score;
        private bool eaten;

        public bool Eaten
        {
            get { return eaten; }
            set { eaten = value; }
        }
        public int FoodX
        {
            get { return foodX; }
            set { foodX = value; }
        }
        public int FoodY
        {
            get { return foodY; }
            set { foodY = value; }
        }
        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public override void Initialize()
        {
            Color = Color.Purple;
            SolidType = Control.GridType.Food;
            Score = 2;
            Eaten = false;
        }
        public override void FinallyHandle()
        {
            foodX = X;
            foodY = Y;
        }
    }
}
