using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Field
    {
        public int Scores = 0;
        int Combo = 0;
        public Point [,] Fullfield = new Point[15, 26];
        Point point = new Point();
        public Field ()
        {
        }

        public void WriteAfterPause() // отрисовает поле после паузы
        {
            for (int i = 4; i < 9; i++)
                if (Fullfield[i, 10] != null)
                    Fullfield[i, 10].Draw();
        }

        public bool Checkfloor(Elements P) // проверка упала ли фигура на что-нибудь
        {
                bool findfloor = true;
                for (int i = 0; i < 4; i++)
                {
                    Point p1 = new Point(P.mas[i]);
                    int x1 = p1.x;
                    int y1 = p1.y + 1;
                    if (Fullfield[x1,y1] != null || y1 == 21)
                        findfloor = false;
                }
                return findfloor;
        }

        public void SavePoints(Elements P) // сохраняет положение точек в поле
        {
            for (int i = 0; i < 4; i++)
            {
                Point p1 = new Point(P.mas[i]);
                int x = P.mas[i].x;
                int y = P.mas[i].y;
                Fullfield[x, y] = p1;
            }
        }
        
        public void CheckLine() // проверка заполнености линии
        {
            bool checking;
            int Line = 20;
            for (int j = 1; j < 20; j++)
            {
                checking = LineIsFull(j);
                while (checking == false)
                {
                    Combo++;
                    Line = 21 - j;
                    for (int i = 1; i < 13; i++)
                    {
                        Fullfield[i, Line].Clear();
                        Fullfield[i, Line] = null;
                    }
                    for (int k = 21 - Line; k < 20; k++)
                        for (int i = 1; i < 13; i++)
                            if (Fullfield[i, 20 - k] != null)
                            {
                                Point p = new Point(Fullfield[i, 20 - k]);
                                p.colors = Fullfield[i, 20 - k].colors;
                                Fullfield[i, 20 - k] = null;
                                p.Clear();
                                p.y++;
                                p.Draw();
                                Fullfield[i, 20 - k + 1] = p;
                            }
                    checking = LineIsFull(j);
                }
                if (Combo != 0)
                {
                    ScoresWrite();
                    Combo = 0;
                }
                checking = false;
            }
        }

        public bool LineIsFull(int j) // проверка заполенности определённой линии
        {
            bool checking = false;
            for (int i = 1; i < 13; i++)
                if (checking == false)
                {
                    if (Fullfield[i, 21 - j] == null)
                        checking = true;
                }
            return checking;
        }

        public void ScoresWrite() // суммирование очков
        {
            switch (Combo)
            {
                case 1:
                    Scores += 100;
                    break;
                case 2:
                    Scores += 300;
                    break;
                case 3:
                    Scores += 700;
                    break;
                case 4:
                    Scores += 1500;
                    break;
            }
            Console.SetCursorPosition(17, 5);
            Console.Write(Scores);
        }

        public void FieldIsEmpty() // очищение поля от данных
        {
            for (int j = 1; j < 21; j++)
                for (int i = 1; i < 13; i++)
                    Fullfield[i, j] = null;
        }
        
        public bool CheckLeft(Elements P) // условие проверяет есть ли препятствие слева
        {
            bool LeftIsfree = true;
            int MinX = 21;
            for (int i=0; i<4; i++)
                if (P.mas[i].x < MinX)
                    MinX = P.mas[i].x;
            for (int i = 0; i < 4; i++)
                if (P.mas[i].x == MinX)
                {
                    Point p1 = new Point(P.mas[i]);
                    int y1 = p1.y;
                    int x1 = p1.x - 1;
                    if (Fullfield[x1, y1] != null || x1 == 0)
                       LeftIsfree = false;
                    
                }
            return LeftIsfree;
        }

        public bool CheckRight(Elements P) // условие проверяет есть ли препятствие справа
        {
            bool RightIsfree = true;
            int MaxX = 0;
            for (int i = 0; i < 4; i++)
                if (P.mas[i].x > MaxX)
                    MaxX = P.mas[i].x;
            for (int i = 0; i < 4; i++)
                if (P.mas[i].x == MaxX)
                {
                    Point p1 = new Point(P.mas[i]);
                    int y1 = p1.y;
                    int x1 = p1.x + 1;
                    if (Fullfield[x1, y1] != null || x1 == 13)
                        RightIsfree = false;
                }
            return RightIsfree;
        }

        public void CleanAndDraw()  // очищение и отрисовка поля
        {
            for (int j = 1; j < 21; j++)
                for (int i = 1; i < 13; i++)
                    if (Fullfield[i, j] != null)
                    {
                        Point p1 = new Point(Fullfield[i, j]);
                        p1.colors = Fullfield[i, j].colors;
                        p1.Draw();
                    }
        }

        public bool CheckSwap(Elements[] P, int Form) // условие проверяет можно ли повернуть фигуру
        {
            bool answer = true;
            int TrialForm = P[0].timesFigureSwap(Form);
            if (point.CheckWalls(P[TrialForm]) == false)
                answer = false;
            else
                for (int i = 0; i < 4; i++)
                {
                    Point p1 = P[TrialForm].mas[i];
                    if (Fullfield[p1.x, p1.y] != null)
                        answer = false;
                }
            return answer;
        }

        public bool GameOver() // условие проверяет можно ли появится новая фигура
        {
            bool StartPlaceFulled = false;
            for (int j = 1; j < 4; j++)
                for (int i = 5; i < 8; i++)
                    if (Fullfield[i, j] != null)
                        StartPlaceFulled = true;
            return StartPlaceFulled;
        }
    }
}

