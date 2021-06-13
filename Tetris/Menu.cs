using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tetris
{
    class Menu
    {
        public int Line = 0;
        bool FileNotRead = true;
        public Records records = new Records();

        public Menu()
        {

        }

        public void Pause()
        {
            Console.SetCursorPosition(4, 10);
            Console.Write("PAUSE");
            Console.ReadKey();
            Console.SetCursorPosition(4, 10);
            Console.Write("     ");
        }

        public void SwitchChangeColor(int number, string name, string name1, string name2, int Xname, int Xname1, int Xname2
            ,int Y) // изменяет цвет кпонки из обычного на выделенный
        {
            switch (number)
            {
                case 0:
                    ChangeLineColor(Xname, Y, name);
                    break;
                case 1:
                    ChangeLineColor(Xname1, Y+1, name1);
                    break;
                case 2:
                    ChangeLineColor(Xname2, Y+2, name2);
                    break;
            }
        }

        public void SwitchDefaultColor(int number, string name, string name1, string name2, int Xname, int Xname1, int Xname2
            , int Y) // изменяет цвет кпонки из выделенного на обычный
        {
            switch (number)
            {
                case 0:
                    Console.SetCursorPosition(Xname, Y);
                    Console.Write(name);
                    break;
                case 1:
                    Console.SetCursorPosition(Xname1, Y+1);
                    Console.Write(name1);
                    break;
                case 2:
                    Console.SetCursorPosition(Xname2, Y+2);
                    Console.Write(name2);
                    break;
            }
        }

        public int SwitchButton(int number, ConsoleKey Key) // измение положение кнопки и нажатии
        {
            if (Key == ConsoleKey.UpArrow)
            {
                number--;
                if (number < 0)
                    number = 2;
            }
            else if (Key == ConsoleKey.DownArrow)
            {
                number++;
                if (number == 3)
                    number = 0;
            }
            return number;
        }

        public void WriteTetris() // отрисовка интерфейса тетриса
        {
            Console.SetBufferSize(27, 22);
            Console.SetWindowSize(27, 22);
            Vertical LeftLine = new Vertical(0, 0, 21, '|');
            Vertical RightLine = new Vertical(13, 0, 21, '|');
            Vertical RightLine1 = new Vertical(25, 0, 21, '|');
            Horizontal UpLine = new Horizontal(0, 25, 0, '-');
            Horizontal DownLine = new Horizontal(0, 25, 21, '-');
            Console.SetCursorPosition(17, 4);
            Console.WriteLine("Очки:");
            Console.SetCursorPosition(17, 5);
            Console.Write(0);
            Console.SetCursorPosition(15, 11);
            Console.WriteLine("Следующий");
            Console.SetCursorPosition(16, 12);
            Console.WriteLine("элемент");
            LeftLine.Drow();
            RightLine.Drow();
            RightLine1.Drow();
            UpLine.Drow();
            DownLine.Drow();
            Console.SetCursorPosition(14, 1);
        }

        public int LevelChoice() // отрисовка меню выбора уровня сложности
        {
            int Sleep = 0;
            int Leveline = 0;
            bool EnterPress = false;
            Console.SetWindowSize(22, 16);
            Vertical LeftLine = new Vertical(0, 0, 15, '|');
            Vertical RightLine = new Vertical(20, 0, 15, '|');
            Horizontal UpLine = new Horizontal(0, 20, 0, '-');
            Horizontal DownLine = new Horizontal(0, 20, 15, '-');
            Console.SetCursorPosition(3, 4);
            Console.Write("Выберите уровень");
            Console.SetCursorPosition(6, 5);
            Console.Write("сложности");
            Console.SetCursorPosition(8, 7);
            Console.Write("Лёгкий");
            Console.SetCursorPosition(7, 8);
            Console.Write("Средний");
            Console.SetCursorPosition(7, 9);
            Console.Write("Сложный");
            SwitchChangeColor(Leveline, "Лёгкий", "Средний", "Сложный", 8, 7, 7, 7);
            LeftLine.Drow();
            RightLine.Drow();
            UpLine.Drow();
            DownLine.Drow();
            Console.SetCursorPosition(14, 1);
            while (EnterPress == false)
            {
                if (Console.KeyAvailable)
                {
                    SwitchDefaultColor(Leveline, "Лёгкий", "Средний", "Сложный", 8, 7, 7, 7);
                    ConsoleKeyInfo key;
                    key = Console.ReadKey();
                    Leveline = SwitchButton(Leveline, key.Key);
            
                    if (key.Key == ConsoleKey.Enter)
                    {
                        EnterPress = true;
                        break;
                    }
                    SwitchChangeColor(Leveline, "Лёгкий", "Средний", "Сложный", 8, 7, 7, 7);
                }
            }
            CleanWindow(20, 15);
            switch (Leveline)
            {
                case 0:
                    Sleep = 400;
                    break;
                case 1:
                    Sleep = 300;
                    break;
                case 2:
                    Sleep = 200;
                    break;
            }
            return Sleep;
        }

        public void TetrisLogo() // отрисовка лого ТЕТРИСа
        {
            Console.SetCursorPosition(8, 4);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("T");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("E");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("T");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Я");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("I");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("S");
        }

        public void WriteMenu() // отрисовка глаавного меню
        {
            if (FileNotRead)
            {
                records.ReadFormFile();
                FileNotRead = false;
            }
            bool EnterPress = false;
            Console.SetWindowSize(1, 1);
            Console.SetBufferSize(22, 16);
            Console.SetWindowSize(22, 16);
            Vertical LeftLine = new Vertical(0, 0, 15, '|');
            Vertical RightLine = new Vertical(20, 0, 15, '|');
            Horizontal UpLine = new Horizontal(0, 20, 0, '-');
            Horizontal DownLine = new Horizontal(0, 20, 15, '-');
            TetrisLogo();
            Console.SetCursorPosition(8, 7);
            Console.Write("Играть");
            Console.SetCursorPosition(7, 8);
            Console.Write("Рекорды");
            Console.SetCursorPosition(8, 9);
            Console.Write("Выход");
            SwitchChangeColor(Line, "Играть", "Рекорды", "Выход", 8, 7, 8, 7);
            LeftLine.Drow();
            RightLine.Drow();
            UpLine.Drow();
            DownLine.Drow();
            Console.SetCursorPosition(14, 1);
            while (EnterPress == false)
            {
                if (Console.KeyAvailable)
                {
                    SwitchDefaultColor(Line, "Играть", "Рекорды", "Выход", 8, 7, 8, 7);

                    ConsoleKeyInfo key;
                    key = Console.ReadKey();
                    Line = SwitchButton(Line, key.Key);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        EnterPress = true;
                        break;
                    }
                    SwitchChangeColor(Line, "Играть", "Рекорды", "Выход", 8, 7, 8, 7);
                }
            }
            CleanWindow(20, 15);
        }

        private void ChangeLineColor(int x, int y, string word) // изменяет цвет определённой кпонки с обычного на выделенный
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(x, y);
            Console.Write(word);
            Console.ResetColor();
        }

        public void WriteNewRecord(int Scores) // отрисовка поля где можно вписать своё имя
        {
            CleanWindow(24, 14, 1, 6);
            Horizontal UpLine = new Horizontal(0, 25, 6, '-');
            Horizontal DownLine = new Horizontal(0, 25, 14, '-');
            UpLine.Drow();
            DownLine.Drow();
            Console.SetCursorPosition(7, 8);
            Console.WriteLine("Новый рекорд!");
            Console.SetCursorPosition(5, 9);
            Console.WriteLine("Введите наше имя");
            Console.SetCursorPosition(3, 10);
            Console.WriteLine("(  Оно должно быть )");
            Console.SetCursorPosition(3, 11);
            Console.WriteLine("(  меньше 10 букв  )");
            records.CreateNewRecord(Scores);
            records.CountNumbersOfNumber();
        }

        public bool CheckRecords(int Scores) // проверяет поставил ли игрок новый рекорд
        {
            bool NewRecord = false;
            int[] ScoresMas = records.RecordScores;
            for (int i = 0; i < 10; i++)
                if (Scores > ScoresMas[i])
                    NewRecord = true;
            return NewRecord;
        }

        public void ShowGameOver() // выводит экран что игрок проиграл
        {
            Console.SetCursorPosition(3, 9);
            Console.WriteLine("         ");
            Console.SetCursorPosition(3, 10);
            Console.WriteLine("GAME OVER");
            Console.SetCursorPosition(3, 11);
            Console.WriteLine("         ");
            Console.ReadKey();
        }

        public bool Continue() // выводит окно с предложением переиграть уровень
        {
            bool tryagain = false;
            bool EnterPress = true;
            int ButtonNumber = 0;
            CleanWindow(23, 12, 2, 7);
            Vertical LeftLine = new Vertical(2, 7, 12, '|');
            Vertical RightLine = new Vertical(23, 7, 12, '|');
            Horizontal UpLine = new Horizontal(2, 23, 7, '-');
            Horizontal DownLine = new Horizontal(2, 23, 12, '-');
            LeftLine.Drow();
            RightLine.Drow();
            UpLine.Drow();
            DownLine.Drow();
            Console.SetCursorPosition(4, 9);
            Console.WriteLine("Начать новую игру?");
            ChangeLineColor(8, 10, "Да");
            Console.SetCursorPosition(15, 10);
            Console.WriteLine("Нет");
            while (EnterPress)
            {
                if (Console.KeyAvailable)
                {
                    switch (ButtonNumber)
                    {
                        case 0:
                            Console.SetCursorPosition(8, 10);
                            Console.WriteLine("Да");
                            break;
                        case 1:
                            Console.SetCursorPosition(15, 10);
                            Console.WriteLine("Нет");
                            break;
                    }

                    ConsoleKeyInfo key;
                    key = Console.ReadKey();

                    if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.LeftArrow)
                    {
                        ButtonNumber--;
                        if (ButtonNumber == -1)
                            ButtonNumber = 1;
                    }
                    else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.RightArrow)
                    {
                        ButtonNumber++;
                        if (ButtonNumber == 2)
                            ButtonNumber = 0;
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        EnterPress = false;
                        break;
                    }

                    switch (ButtonNumber)
                    {
                        case 0:
                            ChangeLineColor(8, 10, "Да");
                            break;
                        case 1:
                            ChangeLineColor(15, 10, "Нет");
                            break;
                    }
                }
            }
            if (ButtonNumber == 0)
                tryagain = true;
            CleanWindow(25, 21);
            return tryagain;
            
        }

        public void WindowRecords() // отрисовка окна с рекордами
        {
            int RecordLine = 1;
            bool EnterPress = true;
            Console.SetBufferSize(27, 17);
            Console.SetWindowSize(27, 17);
            Vertical LeftLine = new Vertical(0, 0, 16, '|');
            Vertical RightLine = new Vertical(25, 0, 16, '|');
            Horizontal UpLine = new Horizontal(0, 25, 0, '-');
            Horizontal DownLine = new Horizontal(0, 25, 16, '-');
            LeftLine.Drow();
            RightLine.Drow();
            UpLine.Drow();
            DownLine.Drow();
            Console.SetCursorPosition(5, 13);
            Console.WriteLine("Очистить рекорды");
            records.WriteRecords();
            while (true)
            {
                ChangeLineColor(11, 14, "Назад");
                while (EnterPress)
                {
                    if (Console.KeyAvailable)
                    {
                        switch (RecordLine)
                        {
                            case 0:
                                Console.SetCursorPosition(5, 13);
                                Console.Write("Очистить рекорды");
                                break;
                            case 1:
                                
                                Console.SetCursorPosition(11, 14);
                                Console.Write("Назад");
                                break;
                        }

                        ConsoleKeyInfo key;
                        key = Console.ReadKey();

                        if (key.Key == ConsoleKey.UpArrow)
                        {
                            RecordLine--;
                            if (RecordLine == -1)
                                RecordLine = 1;
                        }
                        else if (key.Key == ConsoleKey.DownArrow)
                        {
                            RecordLine++;
                            if (RecordLine == 2)
                                RecordLine = 0;
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                            EnterPress = false;
                            break;
                        }

                        switch (RecordLine)
                        {
                            case 0:
                                ChangeLineColor(5, 13, "Очистить рекорды");
                                break;
                            case 1:
                                ChangeLineColor(11, 14, "Назад");
                                break;
                        }
                    }

                }
                if (RecordLine == 1)
                {
                    CleanWindow(25, 16);
                    break;
                }
                else
                {
                    records.CleanRecords();
                    CleanWindow(24, 12, 2, 2);
                    records.WriteRecords();
                    EnterPress = true;
                }
            }

                
        }

        public void CleanWindow(int Width, int Height, int WidthStart = 0, int HeightStart = 0) // очищение интерефейса
        {
            for (int i = WidthStart; i <= Width; i++)
                for (int j = HeightStart; j <= Height; j++)
                {
                    Point CleanPoint = new Point(i, j, ' ' );
                    CleanPoint.Clear();
                }
        }
    }
}
