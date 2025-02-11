using TextRPG_Maple._04._Manager;
using TextRPG_Maple._04._Manager._05._Object;
using TextRPG_Maple._05._Usable.Skill;

namespace TextRPG_Maple._03._Scene.SkillScene
{
    internal class SkillScene : IScene
    {
        Player? player = GameObjectManager.Instance.GetGameObject(ObjectType.PLAYER, "MainPlayer") as Player;

        public void Enter()
        {

        }

        public void Exit()
        {

        }

        public void Render()
        {
            Console.Clear();

            InputManager.Instance.WriteLineColor("스킬", ConsoleColor.Yellow);

            Console.WriteLine("스킬을 관리하는 장소입니다.");
            Console.WriteLine("이곳에서 스킬을 장비할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[스킬 목록]");

            if (player.Skills == null|| player.Skills.Count == 0)
                InputManager.Instance.WriteLineColor("배운 스킬이 없습니다...", ConsoleColor.DarkGray);
            else
            {
                for (int i = 0; i < player.Skills.Count; i++) // 스택 오버플로우?
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
                    if (player.Skills.Count != 0)
                        SceneManager.Instance.EnterScene(SceneType.EquipSkillScene);
                    else
                    {
                        InputManager.Instance.WriteLineColor("\n보유한 스킬이 없습니다...", ConsoleColor.DarkGray);
                        Thread.Sleep(600);
                    }
                    break;
                case 2:
                    SceneManager.Instance.EnterScene(SceneType.Inventory);
                    break;
                case 0:
                    SceneManager.Instance.ExitScene();
                    break;
            }
        }
    }
}
