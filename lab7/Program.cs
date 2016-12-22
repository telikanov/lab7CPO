using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System;

namespace _7_SPO
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllText(@"C:\Users\telik\OneDrive\Документы\Visual Studio 2015\Projects\lab7\Program.txt")
                .Replace("\r", "").Replace("\n", "").Split(' ');
            var dot_comma = input.Where((t) => t.Contains(";") || t.Contains("if"));
            var brackets = input.Where((t) => t.Contains(")") || t.Contains("("));
            int count = dot_comma.Count();
            int if_index = -1;
            int then_index = -1;
            int if_then_count = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if ((i > 0) && (input[i].Contains("if") && input[i - 1].Contains("else")))
                {
                    count--;
                }
                if (input[i].Contains("if"))
                {
                    if_index = i;
                    if_then_count++;
                }
                if (input[i].Contains("then"))
                {
                    then_index = i;
                    if_then_count++;
                }
                string pattern = @"[a-z]";
                string pattern1 = @"[0-9]";
                string pattern2 = @"[><=-]";
                Regex regex = new Regex(pattern);
                Regex regex1 = new Regex(pattern1);
                Regex regex2 = new Regex(pattern2);
                if ((i > 0) && ((input[i] == ">") || (input[i] == "<") || (input[i] == "==") || (regex2.IsMatch(input[i]))) && ((regex.IsMatch(input[i - 1]) || regex1.IsMatch(input[i - 1])) && (regex.IsMatch(input[i + 1]) || regex1.IsMatch(input[i + 1])) && ((input[i] != ":="))))
                {
                    if ((input[i] == ">") || (input[i] == "<") || (input[i] == "=="))
                    {
                        try
                        {
                            bool flag = true;
                            while (flag == true)
                            {
                                if (Convert.ToInt32(input[i - 1].Replace("(", "")) > Convert.ToInt32(input[i + 1].Replace(")", "")))
                                {
                                    flag = false;

                                }
                                if (Convert.ToInt32(input[i - 1].Replace("(", "")) > Convert.ToInt32(input[i + 1].Replace(")", "")))
                                {
                                    flag = false;
                                }
                                if (Convert.ToInt32(input[i - 1].Replace("(", "")) == Convert.ToInt32(input[i + 1].Replace(")", "")))
                                {
                                    flag = false;
                                }

                            }
                        }
                        catch
                        {
                            if (!regex.IsMatch(input[i - 1].Replace("(", "")))
                            {
                                Console.WriteLine("Неверная левая часть сравнения");
                            }
                            if (!regex1.IsMatch(input[i + 1].Replace(")", "")))
                            {
                                Console.WriteLine("Неверная правая часть сравнения");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неопознанный оператор");
                    }
                }

              
            }
            if (count % 2 != 0)
            {
                Console.WriteLine("Пропущен закрывающий символ ';'");
            }
            if ((then_index < if_index))
            {
                Console.WriteLine("Нарушена последовательность 'if-then'");
            }
            if (if_then_count % 2 != 0)
            {
                Console.WriteLine("Не закончена связка 'if-then'");
            }
            if (brackets.Count() % 2 != 0)
            {
                Console.WriteLine("Нет парных закрывающих скобок");
            }
            Console.ReadKey();
        }
    }
}