using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            bool escape = false;
            bool tryagain =  false;
            int FigureForm = 0;
            bool exit = true;
            bool newrecord = false;
            int Sleep = 0;
            Menu menu = new Menu();
            while (exit) // пока игрок не вышел из игры
            {
                if (tryagain == false) 
                menu.WriteMenu(); // отрисовка главного меню
                if (menu.Line == 0) // условие если пользовать нажал "Играть"
                {
                    escape = false;
                    Sleep = menu.LevelChoice(); // выбор сложности
                    menu.WriteTetris(); // отрисовка полей Тетриса
                    Elements TakenElement = new Elements(); // создание фигуры
                    Elements NextElement = new Elements(); // создание следующей фигуры
                    Field Tetrisfield = new Field(); // создание поля Тетриса
                    Tetrisfield.FieldIsEmpty(); // в поле ничего
                    TakenElement.Create(Sleep); // отрисовка фигуры
                    NextElement.ShowNextElement(); // отрисовка следующей фигуры
                    while (true)
                    {
                        if (Tetrisfield.Checkfloor(TakenElement.FormsOfElem[FigureForm]) == false) // если фигуры упала на пол
                        {
                            Tetrisfield.SavePoints(TakenElement.FormsOfElem[FigureForm]); // сохраняется положение фигуры в поле
                            Tetrisfield.CleanAndDraw(); // отчистка поля и отрисовка
                            if (Tetrisfield.GameOver()) // условние конца игры
                            {
                                menu.ShowGameOver(); // отрисовка что игра закончена
                                if (menu.CheckRecords(Tetrisfield.Scores)) // условие если игрок сделал новый рекорд
                                {
                                    menu.WriteNewRecord(Tetrisfield.Scores); // отрисовка рамки где можно ввести своё имя
                                    newrecord = true; // был записал новый рекорд
                                }
                                tryagain = menu.Continue(); // хочет ли игрок переиграть
                                break;
                            }
                            Tetrisfield.CheckLine(); // есть ли заполненная линия
                            FigureForm = 0; // начальное положение фигуры
                            TakenElement = NextElement; // следующий элемент становится текущим
                            TakenElement.Create(Sleep); // отрисовка фигуры
                            NextElement = new Elements(); // создание следующей фигуры
                            NextElement.ShowNextElement(); // отрисовка следующей фигуры
                        }
                        while (Tetrisfield.Checkfloor(TakenElement.FormsOfElem[FigureForm])) // пока фигура падает
                        {
                            TakenElement.MoveFig(Sleep); // перемещение фигуры вниз
                            for (int i = 0; i < 2; i++) 
                                if (Console.KeyAvailable) // если нажата клавиша
                                {
                                    ConsoleKeyInfo key;
                                    key = Console.ReadKey(); 
                                    if (key.Key == ConsoleKey.UpArrow) // если нажата клавижа "Вверх"
                                    {
                                        if (Tetrisfield.CheckSwap(TakenElement.FormsOfElem, FigureForm)) // можно ли повернуть фигуру
                                            FigureForm = TakenElement.FigureSwap(); // поворот фигуры
                                    }
                                    else if (key.Key == ConsoleKey.Spacebar) // если нажат "Пробел"
                                    {
                                        menu.Pause(); // игра на паузе
                                        Tetrisfield.WriteAfterPause(); // возрат к игре
                                    }
                                    else if (key.Key == ConsoleKey.Escape) // если нажат "ESC"
                                    {
                                        tryagain = menu.Continue(); // отриска поля и предложением переиграть
                                        escape = true; // игра выйдёт или в меню или начнёт игру снова
                                    }
                                    else
                                        TakenElement.CheckKeyboardConditions(key.Key, Tetrisfield); // если нажата кнопка "Вправо или влево"
                                    Thread.Sleep(50);
                                   
                                }
                             if (escape) // если в был нажат "ESC"
                             break; // прерывание игры

                        }
                        if (escape)
                            break;
                    }
                }
                else if (menu.Line == 1) // если игрок решил зайти в Рекорды
                {
                    menu.WindowRecords(); // отрисовка окна рекордов
                }
                else if (menu.Line == 2) // если игрок решил выйти из игрв
                {
                    if (newrecord) // если в течение сеанса игры игрок поставил новый рекорд
                    menu.records.WriteInFile(); // запись новых рекордов в файл
                    exit = false; // выход из игры
                }

            }
        }
    }
}
