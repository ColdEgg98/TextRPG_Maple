using ConsoleApp2._04._Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Maple._03._Scene.SkillScene
{
    internal class SkillScene
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

            InputManager.Instance.WriteColor("스킬", ConsoleColor.DarkYellow);

            InputManager.Instance.Write(
                "스킬을 관리하는 장소입니다.\n" +
                "이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n\n" +
                "1. 스킬 장비\n" +
                "0. 나가기\n"
                );
        }

        public void Update()
        {
            // 조건에 맞는 올바른 키를 입력할때 까지 반복
            int input = InputManager.Instance.GetInput(0, 1);

            //입력에 따른 실행
            switch (input)
            {
                case 1:
                    // SkillEquip();
                    break;
                case 0:
                    SceneManager.Instance.ChangeScene(SceneType.Town);
                    break;
            }
        }
    }
}
