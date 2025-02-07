using ConsoleApp2._04._Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class DungeonScene : IScene
    {
        public void Enter()
        {
        }
        public void Exit()
        {
        }

        public void Render()
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine();
        }

        public void Update()
        {
            // 조건에 맞는 올바른 키를 입력할때 까지 반복
            int input = GameManager.Instance.GetInput(1, 3);

            //입력에 따른 실행
            switch (input)
            {
                case 1:
                    //StatusScreen();
                    break;
                case 2:
                    //InventoryScreen();
                    break;
                case 3:
                    //ShopScreen();
                    break;
            }
        }
    }
}
