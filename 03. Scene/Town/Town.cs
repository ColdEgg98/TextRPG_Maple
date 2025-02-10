using TextRPG_Maple._04._Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Maple
{
    internal class TownScene : IScene
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

            InputManager.Instance.Write(
                "스파르타 마을에 오신 여러분 환영합니다.\n" +
                "이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n\n" +
                "1.상태 보기\n" +
                "2.인벤토리\n" +
                "3.상점\n" +
                "4.휴식\n" +
                "5.던전\n\n"
                );
        }

        public void Update()
        {
            // 조건에 맞는 올바른 키를 입력할때 까지 반복
            int input = InputManager.Instance.GetInput(1, 5);

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
                    SceneManager.Instance.ChangeScene(SceneType.Store);
                    break;
                case 4:
                    //ShopScreen();
                    break;
                case 5:
                    SceneManager.Instance.EnterScene(SceneType.Dungeon);
                    break;
            }
        }
    }
}
