using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Maple._04._Manager;
using TextRPG_Maple._04._Manager._05._Object;
using TextRPG_Maple._05._Usable.Skill;

namespace TextRPG_Maple._03._Scene.SkillScene
{
    internal class EquipSkillScene : IScene
    {
        private Player? player;

        int left;
        int top;

        public void Enter()
        {
            player = GameObjectManager.Instance.GetGameObject(ObjectType.PLAYER, "MainPlayer") as Player;
        }

        public void Exit()
        {

        }

        public void Render()
        {
            InputManager.Instance.WriteLineColor("스킬 - 장비 관리", ConsoleColor.Yellow);

            Console.WriteLine("장비할 스킬의 번호를 입력해주세요.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[스킬 목록]");

            left = Console.CursorLeft;
            top = 5;

            Console.SetCursorPosition(left, top);

            for (int i = 0; i < player.Skills.Count; i++)
            {
                InputManager.Instance.WriteColor($"{i + 1}. ", ConsoleColor.DarkGreen);
                Console.WriteLine(player.Skills[i].UsableDisplay());
            }

            Console.WriteLine();
            Console.WriteLine("0. 스킬 확인으로 돌아가기");
            Console.WriteLine();
        }

        public void Update()
        {
            // 조건에 맞는 올바른 키를 입력할때 까지 반복
            int input = InputManager.Instance.GetInput(0, player.Skills.Count);

            //입력에 따른 실행
            switch (input)
            {
                case 0:
                    SceneManager.Instance.ExitScene();
                    break;
                default:
                    player.Skills[input - 1].IsEquip = !(player.Skills[input - 1].IsEquip);
                    break;
            }
        }
    }
}
