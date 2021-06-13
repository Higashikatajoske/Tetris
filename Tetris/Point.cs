using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Point
    {
        public int x, y;
        public char sym;
        public Colors colors;
    public Point(int _x, int _y, char _sym) // задание точки
        {
            x = _x;
            y = _y;
            sym = _sym;
        } 

        public Point(Point p) // задание точки с указанием цвета
        {
            x = p.x;
            y = p.y;
            sym = p.sym;
            colors = p.colors;
        }

        public Point()
        {

        }

        public void ButtonMove(Direction direction, Elements P) // перемещается координату точки взависимости от нажатой клавиши
        {
            if (direction == Direction.RIGHT)
                for (int i=0; i<4; i++)
                    P.mas[i].x = P.mas[i].x + 1;
            else if (direction == Direction.LEFT)
                for (int i = 0; i < 4; i++)
                    P.mas[i].x = P.mas[i].x - 1;
            else if (direction == Direction.DOWN)
                for (int i = 0; i < 4; i++)
                    P.mas[i].y = P.mas[i].y + 1;
        }

        public bool CheckWalls(Elements P) // проверяет нет ли стены при перемещении вправо или в влево
        {
            bool answer = true;
            for (int i=0; i<4; i++)
                if (P.mas[i].x == 0 || P.mas[i].x == 13)
                    answer = false;
            return answer;
        }

        public void Clear() // очищает интерфес в указанной координате
        {
            char _sym = sym;
            sym = ' ';
            Draw();
            sym = _sym;
        }

        public void Draw() // отрисовка точки в определённой координате
        {
            Console.SetCursorPosition(x, y);
            if (colors != Colors.White)
                ChangeColor();
            Console.Write(sym);
            Console.ResetColor();
            Console.SetCursorPosition(18, 7);
        }
        
        public void ChangeColor () // изменение цвета точки
        {
            switch (colors)
            {
                case Colors.Green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Colors.Red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Colors.Blue:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case Colors.Yellow:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case Colors.Cyan:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case Colors.DarkRed:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case Colors.DarkMagenta:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;

            }
        }
    }
}
