using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Maple._04._Manager;
using TextRPG_Maple._04._Manager._05._Object;

namespace TextRPG_Maple._03._Scene.Inventory
{
    internal class InventoryScene : IScene
    {
        Player player;

        public void Enter()
        {
            player = GameObjectManager.Instance.GetGameObject(ObjectType.PLAYER, "MainPlayer") as Player;
        }
        public void Exit()
        {
        }
        public void Render()
        {
            Console.Clear();
            InputManager.Instance.WriteLineColor("인벤토리", ConsoleColor.DarkYellow);
            Console.WriteLine("보유 중인 아아템과 스킬을 확인할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            // inventory에 있는 item들에 대한 출력
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                Console.WriteLine(player.Inventory[i].UsableDisplay());
            }

            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("2. 스킬 확인");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

        }
        public void Update()
        {
            int input = InputManager.Instance.GetInput(0, 2);

            switch (input)
            {
                case 1:
                    if (player.Skills.Count != 0)
                        SceneManager.Instance.EnterScene(SceneType.EquipInventory);
                    else
                    {
                        InputManager.Instance.WriteLineColor("\n보유한 아이템이 없습니다...", ConsoleColor.DarkGray);
                        Thread.Sleep(600);
                    }
                    break;
                case 2:
                    SceneManager.Instance.EnterScene(SceneType.SkillScene);
                    break;
                case 0:
                    SceneManager.Instance.ExitScene();
                    break;
            }
        }
    }
}
