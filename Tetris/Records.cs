using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tetris
{
    class Records
    {
        char[] fg;
        char test;
        bool space;
        bool nameaccess;
        int count;
        int CountOfLines = 0;
        public int[] RecordScores = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] NumbersCount = new int[10];
        string[] NickName = new string[10];
        public string trialName = "";
        public int stringCount;

        public Records()
        {

        }

        public void CleanRecords() // функция очищения рекордов
        {
            RecordScores = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            NickName = new string[10];
            WriteInFile();
            StreamWriter writer = new StreamWriter("File.txt");
            writer.WriteLine("");
            writer.Close();
        }

        public void ReadFormFile() // запись данных из файла во временную память
        {
            StreamReader reader;
            reader = new StreamReader("File.txt", Encoding.Default);
            if (reader.ReadLine() != "")
            {
                reader.Close();
                reader = new StreamReader("File.txt", Encoding.Default);
                while (reader.ReadLine() != null)
                    CountOfLines++;
            }
            reader.Close();
            for (int i = 0; i < CountOfLines; i++)
            {
                count = 0;
                NumbersCount[i] = 0;
                space = false;
                fg = null;
                reader = new StreamReader("File.txt", Encoding.Default);
                for (int j = 0; j < i; j++)
                    reader.ReadLine();
                while (space == false)
                {
                    test = Convert.ToChar(reader.Read());
                    if (test == ' ')
                        space = true;
                    else
                        count++;
                }
                reader.Close();
                reader = new StreamReader("File.txt", Encoding.Default);
                for (int j = 0; j < i; j++)
                    reader.ReadLine();
                fg = new char[count];
                for (int j = 0; j < count; j++)
                    fg[j] = Convert.ToChar(reader.Read());
                NickName[i] = new string(fg);
                RecordScores[i] = Convert.ToInt32(reader.ReadLine());
                reader.Close();
            }
            CountNumbersOfNumber();

        }

        public void CreateNewRecord(int Scores) // запись рекорда во временную память, и его сортировка по убыванию
        {
            nameaccess = true;
            while (nameaccess)
            {
                Console.SetCursorPosition(7, 12);
                trialName = Console.ReadLine();
                stringCount = trialName.Count();
                if (stringCount > 10)
                {
                    Console.SetCursorPosition(3, 13);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Имя больше 10 знаков!");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.SetCursorPosition(3, 13);
                    Console.WriteLine("                     ");
                    Console.SetCursorPosition(7, 12);
                    Console.WriteLine("                  ");
                }
                else
                    nameaccess = false;
            }
            int ChangedScores;
            string ChangedName;
            for (int i = 0; i < 10; i++)
            {
                if (RecordScores[i] == 0)
                {
                    RecordScores[i] = Scores;
                    NickName[i] = trialName;
                    break;
                }
                else if (RecordScores[i] < Scores)
               {
                    ChangedScores = RecordScores[i];
                    ChangedName = NickName[i];
                    RecordScores[i] = Scores;
                    NickName[i] = trialName;
                    Scores = ChangedScores;
                    trialName = ChangedName;
               }
            }
        }

        public void WriteRecords() // вывод рекордов в окне рекордов
        {
            Console.SetCursorPosition(2, 2);
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(2, i + 2);
                Console.Write(i + 1 + ") ");
                Console.SetCursorPosition(6, i+2);
                if (NickName[i] == null)
                {
                    Console.Write("X");
                    Console.SetCursorPosition(23, i+2);
                    Console.WriteLine(RecordScores[i]);
                }
                else
                {
                    Console.Write("          ");
                    Console.SetCursorPosition(6, i+2);
                    Console.Write(NickName[i]);
                    Console.SetCursorPosition(24 - NumbersCount[i], i+2);
                    Console.WriteLine(RecordScores[i]);
                }
            }
        }

        public void CountNumbersOfNumber() // записывается положение вывода цифры рекодра
        {
            for (int i = 0; i < 10; i++)
            {
                NumbersCount[i] = 0;
                int ScoreNumbers = RecordScores[i];
                while (ScoreNumbers != 0)
                {
                    ScoreNumbers = ScoreNumbers / 10;
                    NumbersCount[i]++;
                }
            }
        }

        public void WriteInFile() // запись данных временной памяти в файл
        {
            StreamWriter writer = new StreamWriter("File.txt");
            for (int i = 0; i < 10; i++)
            {
                if (NickName[i] != null)
                    writer.WriteLine(NickName[i] + " " + RecordScores[i] + " ");
                else
                    break;
            }
            writer.Close();
        }
    }
}