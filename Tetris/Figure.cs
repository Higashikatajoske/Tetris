using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Tetris
{
    class Figure
    {
        private int x, y;
        private char sym;
        public Point Centr, Down, Up , Left, Right, DownRight, DownLeft, UpRight, UpLeft, DownDown, DownDownRight, DownRightRight, RightRight;
        public void SetLocation() // задание начальных координат
        {
            x = 6;
            y = 2;
            sym = '\u2588';
            Centr = new Point(x, y, sym);
            Down = new Point(x, y + 1, sym);
            Up = new Point(x, y - 1, sym);
            Left = new Point(x - 1, y, sym);
            Right = new Point(x + 1, y, sym);
            DownRight = new Point(x + 1, y + 1, sym);
            DownLeft = new Point(x - 1, y + 1, sym);
            UpRight = new Point(x + 1, y - 1, sym);
            UpLeft = new Point(x - 1, y - 1, sym);
            DownDown = new Point(x, y + 2, sym);
            DownDownRight = new Point(x + 1, y + 2, sym);
            DownRightRight = new Point(x + 2, y + 1, sym);
            RightRight = new Point(x + 2, y, sym);
        }

        protected List<Point> pList;
        public Point[] mas = new Point[4];

        public void Drow() // отрисовка точек
        {
            foreach (Point p in pList)
            {
                p.Draw();
            }
        }



    }
}
