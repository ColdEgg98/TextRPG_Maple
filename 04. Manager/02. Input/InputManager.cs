using System;
using System.Runtime.InteropServices;
using System.Threading;

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
                SoundManager.Instance.PlaySound(SoundType.Click, "Error");
                Console.SetCursorPosition(left, top);
                Thread.Sleep(500);
                Console.Write(new string(' ', 37));
                Console.SetCursorPosition(left, top);
            }
        }

        //========================================================================= 
        // ANSI Escape Code
        //=========================================================================
        public InputManager()
        {
            EnableAnsiEscapeCodes();
        }

        // Windows API 함수 선언
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        // 상수 정의
        private const int STD_OUTPUT_HANDLE = -11;
        private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

        private static void EnableAnsiEscapeCodes()
        {
            var handle = GetStdHandle(STD_OUTPUT_HANDLE);
            GetConsoleMode(handle, out uint mode);
            SetConsoleMode(handle, mode | ENABLE_VIRTUAL_TERMINAL_PROCESSING);
        }
    }
}
