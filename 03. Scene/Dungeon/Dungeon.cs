using TextRPG_Maple._04._Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Maple
{
    internal class DungeonScene : IScene
    {
        public int Floor { get; }   // 층수
        
        

        public void Enter()
        {
        }
        public void Exit()
        {
        }

        public void Render()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 전투에 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 전투 시작");  // "전투 시작 : n층" 처럼 층수가 출력되어야 함. 기존 층수에 대한 정보가 존재. 
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
        }

        public void Update()
        {
            // 조건에 맞는 올바른 키를 입력할때 까지 반복
            int input = GameManager.Instance.GetInput(0, 3);

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
                case 0:
                    SceneManager.Instance.ExitScene();
                    break;
            }
        }
    }
}
