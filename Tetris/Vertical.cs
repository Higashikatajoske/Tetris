using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Vertical : Figure
    {
        public Vertical(int x, int yUp, int yDown, char sym) // задание вертикальной линии по параметрам
        {
            {
                pList = new List<Point>();
                for (int y = yUp; y <= yDown; y++)
                {
                    Point p = new Point(x, y, sym);
                    pList.Add(p);
                }
            }

        }
    }
}