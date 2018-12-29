using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace gdi
{
    public class Barrier:Solid
    {
        private static int barrierNum;//障碍物个数

        public static int BarrierNum
        {
            get { return Barrier.barrierNum; }
            set { Barrier.barrierNum = value; }
        }
        public void ResistSnake(Form1 form)
        {
            form.timer1.Stop();
            Control.GameOver(form);
            return;
        }
        public override void Initialize()
        {
            Color = Color.Black;
            SolidType = Control.GridType.Barrier;
        }
    }
}
