using ConsoleApp1;
using ConsoleApp2._04._Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Maple._05._Usable.Skill;

namespace TextRPG_Maple._03._Scene.SkillScene
{
    internal class SkillScene
    {
        Player player;

        public void Enter()
        {
            player = GameManager.player;
        }

        public void Exit()
        {
            player = null;
            System.GC.Collect();
        }

        public void Render()
        {
            Console.Clear();

            InputManager.Instance.WriteLineColor("스킬", ConsoleColor.Yellow);

            Console.WriteLine("스킬을 관리하는 장소입니다.");
            Console.WriteLine("이곳에서 스킬을 장비할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[스킬 목록]");

            for (int i = 0; i < player.Skills.Count; i++)
            {
                Console.WriteLine(player.Skills[i].UsableDisplay());
            }

            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("0. 나가기");
        }

        public void Update()
        {
            // 조건에 맞는 올바른 키를 입력할때 까지 반복
            int input = InputManager.Instance.GetInput(0, 2);

            //입력에 따른 실행
            switch (input)
            {
                case 1:
                    SceneManager.Instance.EnterScene(SceneType.EquipSkillScene);
                    break;
                case 2:
                    SceneManager.Instance.ExitScene();
                    break;
                case 0:
                    SceneManager.Instance.ExitScene();
                    SceneManager.Instance.ExitScene();
                    break;
            }
        }
    }
}
