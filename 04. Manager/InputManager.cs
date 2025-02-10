using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._04._Manager
{
    internal class InputManager
    {
        //========================================================================= 
        // 매니저 초기화
        //========================================================================= 
        private static InputManager instance;
        public static InputManager Instance => instance ??= new InputManager();


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
            while (true) //return이 되기 전까지 반복
            {
                Console.Write("원하시는 행동을 입력해주세요.");

                //int.TryParse는 int로 변환이 가능한지 bool값을 반환, 가능(true)할 경우 out int input으로 숫자도 반환
                if (int.TryParse(Console.ReadLine(), out int input) && (input >= min) && (input <= max))
                    return input;

                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
                Console.ReadKey();
            }
        }
    }
}
