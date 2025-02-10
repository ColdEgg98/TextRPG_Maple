using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Maple._04._Manager
{
    internal class InputManager
    {
        //========================================================================= 
        // 매니저 초기화
        //========================================================================= 
        private static InputManager? instance;
        public static InputManager Instance => instance ??= new InputManager();

        public int ErrorLevel { get; set; }

        public void Write(string text)
        {
            Console.Write(text);
        }

        public void WriteColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public void WriteLineColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public int GetInput(int min, int max)
        {
            int top = Console.CursorTop;
            int left = Console.CursorLeft;
            while (true) //return이 되기 전까지 반복
            {
                Console.Write("원하시는 행동을 입력해주세요.");

                //int.TryParse는 int로 변환이 가능한지 bool값을 반환, 가능(true)할 경우 out int input으로 숫자도 반환
                char key = Console.ReadKey(true).KeyChar;
                if (int.TryParse(key.ToString(), out int input) && (input >= min) && (input <= max))
                    return input;
                Console.SetCursorPosition(left, top);
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
                Console.SetCursorPosition(left, top);
                Thread.Sleep(500);
                Console.Write(new string(' ', 37));
                Console.SetCursorPosition(left, top);
            }
        }
    }
}
