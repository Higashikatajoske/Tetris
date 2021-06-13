using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Tetris
{
    class Elements : Figure
    {
        public Direction direction;
        Direction _direction;
        public Colors Elemcolors;
        Random ElementsRandom = new Random();
        Field field = new Field();
        public Elements[] FormsOfElem = new Elements[4];
        Point point = new Point();
        public int Form = 0;

        public Elements() // создания фигуры
        {
            SetLocation(); // задаётся поле где будет создана фигура
            int NewElement = ElementsRandom.Next(0, 7); // выбирается номер фигуры
            switch (NewElement) // создание фигуры и его всех форм по координатам точек
            {
                case 0:
                    FormsOfElem[0] = new Elements(Up, Centr, Down, DownRight, Colors.Red);
                    FormsOfElem[1] = new Elements(Left, Centr, Right, DownLeft, Colors.Red);
                    FormsOfElem[2] = new Elements(UpLeft, Up, Centr, Down, Colors.Red);
                    FormsOfElem[3] = new Elements(UpRight, Left, Centr, Right, Colors.Red);
                    break;
                case 1:
                    FormsOfElem[0] = new Elements(UpRight, Up, Centr, Right, Colors.Green);
                    FormsOfElem[1] = new Elements(UpRight, Up, Centr, Right, Colors.Green);
                    FormsOfElem[2] = new Elements(UpRight, Up, Centr, Right, Colors.Green);
                    FormsOfElem[3] = new Elements(UpRight, Up, Centr, Right, Colors.Green);
                    break;
                case 2:
                    FormsOfElem[0] = new Elements(Up, Centr, Down, DownLeft, Colors.Blue);
                    FormsOfElem[1] = new Elements(UpLeft, Left, Centr, Right, Colors.Blue);
                    FormsOfElem[2] = new Elements(UpRight, Up, Centr, Down, Colors.Blue);
                    FormsOfElem[3] = new Elements(Right, Centr, Left, DownRight, Colors.Blue);
                    break;
                case 3:
                    FormsOfElem[0] = new Elements(Up, Right, Centr, Down, Colors.Yellow);
                    FormsOfElem[1] = new Elements(Centr, Left, Right, Down, Colors.Yellow);
                    FormsOfElem[2] = new Elements(Up, Centr, Left, Down, Colors.Yellow);
                    FormsOfElem[3] = new Elements(Up, Left, Right, Centr, Colors.Yellow);
                    break;
                case 4:
                    FormsOfElem[0] = new Elements(UpRight, Centr, Right, Down, Colors.Cyan);
                    FormsOfElem[1] = new Elements(Centr, Left, DownRight, Down, Colors.Cyan);
                    FormsOfElem[2] = new Elements(Up, Left, Centr, DownLeft, Colors.Cyan);
                    FormsOfElem[3] = new Elements(Up, UpLeft, Centr, Right, Colors.Cyan);
                    break;
                case 5:
                    FormsOfElem[0] = new Elements(Up, Centr, Right, DownRight, Colors.DarkRed);
                    FormsOfElem[1] = new Elements(Centr, Right, DownLeft, Down, Colors.DarkRed);
                    FormsOfElem[2] = new Elements(UpLeft, Left, Centr, Down, Colors.DarkRed);
                    FormsOfElem[3] = new Elements(Up, UpRight, Left, Centr, Colors.DarkRed);
                    break;
                case 6:
                    FormsOfElem[0] = new Elements(Left, Centr, Right, RightRight, Colors.DarkMagenta);
                    FormsOfElem[1] = new Elements(UpRight, Right, DownRight, DownDownRight, Colors.DarkMagenta);
                    FormsOfElem[2] = new Elements(DownLeft, Down, DownRight, DownRightRight, Colors.DarkMagenta);
                    FormsOfElem[3] = new Elements(Up, Centr, Down, DownDown, Colors.DarkMagenta);
                    break;
            }
        }


        public Elements(Point p1, Point p2, Point p3, Point p4, Colors _colors) // создание одной формы фигуры
        {
            mas[0] = p1;
            mas[1] = p2;
            mas[2] = p3;
            mas[3] = p4;
            Elemcolors = _colors;
        }

        public void Create(int Sleep) // отрисовка фигуры
        {
            for (int i = 0; i < 4; i++)
            {
                FormsOfElem[Form].mas[i].colors = FormsOfElem[Form].Elemcolors;
                Point p1 = new Point(FormsOfElem[Form].mas[i]);
                p1.Draw();
            }
            Thread.Sleep(Sleep);
        }

        public void MoveFig(int Sleep) // отрисовка падения фигуры
        {
            for (int i = 0; i < 4; i++)
                FormsOfElem[Form].mas[i].Clear();

            for (int i = 0; i < 4; i++)
                for (int j=0; j < 4; j++)
                {
                    Point p1 = new Point(FormsOfElem[i].mas[j]);
                    p1.y++;
                    FormsOfElem[i].mas[j] = p1;
                }
            Create(Sleep);
        }

        public void KeyboardMove(ConsoleKey key) // отрисовка движений фигуры, взависимости от нажатой кнопки 
        {
            for (int i = 0; i < 4; i++)
                FormsOfElem[Form].mas[i].Clear();
             _direction = Keyboardbuttons(key);
            for (int i = 0; i < 4; i++)
            point.ButtonMove(_direction, FormsOfElem[i]);
        }

        public void CheckKeyboardConditions(ConsoleKey key, Field Tetrisfield) // проверка условий перемещения фигуры
        {
                if (ConsoleKey.LeftArrow == key)
                    if (Tetrisfield.CheckLeft(FormsOfElem[Form]))
                        KeyboardMove(key);
                if (ConsoleKey.RightArrow == key)
                    if (Tetrisfield.CheckRight(FormsOfElem[Form]))
                        KeyboardMove(key);
                if (ConsoleKey.DownArrow == key)
                    if (Tetrisfield.Checkfloor(FormsOfElem[Form]))
                        KeyboardMove(key);
        }

        public Direction Keyboardbuttons(ConsoleKey key) // задания направления, взависимости от нажатой клавиши
        {
            if (key == ConsoleKey.LeftArrow)
                direction = Direction.LEFT;
            else if (key == ConsoleKey.RightArrow)
                direction = Direction.RIGHT;
            else if (key == ConsoleKey.DownArrow)
                direction = Direction.DOWN;
            return direction;
        }

        public int FigureSwap() // отрисовка поворота фигуры
        {
            for (int i = 0; i < 4; i++)
            {
                Point p1 = new Point(FormsOfElem[Form].mas[i]);
                p1.Clear();
            }
            Form = timesFigureSwap(Form);
            for (int i = 0; i < 4; i++)
            {
                FormsOfElem[Form].mas[i].colors = FormsOfElem[Form].Elemcolors;
                Point p1 = new Point(FormsOfElem[Form].mas[i]);
                p1.Draw();
            }
            return Form;
        }

        public int timesFigureSwap(int TrialForm) // возращает значение номера положения фигуры
        {
            if (TrialForm >= 0 && TrialForm < 3)
                TrialForm++;
            else
                TrialForm = 0;
            return TrialForm;
        }

        public void ShowNextElement() // выводит следующую фигуру которая будет падать в интерфейсе
        {
            for (int i = 18; i < 22; i++)
                for (int j = 14; j < 17; j++)
                {
                    Point p1 = new Point(i, j, ' ');
                    p1.Clear();
                }
            Elements ShowedElem = FormsOfElem[0];
            for (int i = 0; i < 4; i++)
            {
                FormsOfElem[0].mas[i].x = FormsOfElem[0].mas[i].x + 13;
                FormsOfElem[0].mas[i].y = FormsOfElem[0].mas[i].y + 13;
                FormsOfElem[Form].mas[i].colors = FormsOfElem[Form].Elemcolors;
                Point p1 = new Point(ShowedElem.mas[i]);
                p1.Draw();
                FormsOfElem[0].mas[i].x = FormsOfElem[0].mas[i].x - 13;
                FormsOfElem[0].mas[i].y = FormsOfElem[0].mas[i].y - 13;
            }
        }
    }
}