using ConsoleApp1;
using ConsoleApp2._04._Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class TitleScene : IScene
    {
        private bool hasData = true;
        public void Enter()
        {
            // TODO : 데이터가 있는지 확인
        }

        public void Exit()
        {

        }

        public void Render()
        {
            Console.Clear();

            DrawTitle();

            // menu

            Console.SetCursorPosition(15, 17);
            Console.WriteLine("1. 처음부터 시작");
            if (hasData)
            {
                Console.SetCursorPosition(15, 18);
                Console.WriteLine("2. 이어서 시작");
            }
            Console.SetCursorPosition(15, 19);
            Console.WriteLine("0. 게임 종료");

            // meny box
            DrawWall(50, 6, 10, 15);

            // outline
            DrawWall(72, 27, 0, 0);
        }

        public void Update()
        {
            // 조건에 맞는 올바른 키를 입력할때 까지 반복
            Console.SetCursorPosition(18, 23);
            int input = GameManager.Instance.GetInput(0, hasData? 2 : 1);
            //입력에 따른 실행
            switch (input)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    //new game
                    SceneManager.Instance.ChangeScene(SceneType.Start);
                    break;
                case 2:
                    //continue game
                    SceneManager.Instance.ChangeScene(SceneType.Town);
                    break;
            }
        }
        public void DrawWall(int x, int y, int posX, int posY)
        {
            // 상단 가로선
            Console.SetCursorPosition(posX, posY);
            Console.Write(new string('=', x + 1));

            // 세로선
            for (int i = 1; i < y; i++)
            {
                Console.SetCursorPosition(posX, posY + i);
                Console.Write("|"); // 왼쪽 벽

                Console.SetCursorPosition(posX + x, posY + i);
                Console.Write("|"); // 오른쪽 벽
            }

            // 하단 가로선
            Console.SetCursorPosition(posX, posY + y);
            Console.Write(new string('=', x + 1));
        }

        public void DrawTitle()
        {
            Console.SetCursorPosition(0, 3);
            Console.WriteLine("                                                                        \r\n ######    #####   ##  ##   ######            #####    #####     #####  \r\n   ##     ##        ####      ##              ##  ##   ##  ##   ##      \r\n   ##     ####       ##       ##              ##  ##   ##  ##   ##      \r\n   ##     ##        ####      ##              #####    #####    ## ###  \r\n   ##     ##       ##  ##     ##              ## ##    ##       ##  ##  \r\n   ##      #####   ##  ##     ##              ##  ##   ##        ####   \r\n                                                                        \r\n");
        }
    }
}
