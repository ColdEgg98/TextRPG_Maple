using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Maple._04._Manager;
using TextRPG_Maple._04._Manager._05._Object;

namespace TextRPG_Maple._03._Scene.Inventory
{
    internal class EquipInventory : IScene
    {
        private Player? player;

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
            InputManager.Instance.WriteLineColor("인벤토리 - 아이템 장착", ConsoleColor.Yellow);
            Console.WriteLine("아이템 번호를 눌러 탈착합니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            // inventory에 있는 item들에 대한 출력
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                InputManager.Instance.WriteColor($"{i + 1}. ", ConsoleColor.DarkGreen);
                Console.WriteLine(player.Inventory[i].UsableDisplay());
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
        }
        public void Update()
        {
            // 조건에 맞는 올바른 키를 입력할때 까지 반복
            int input = InputManager.Instance.GetInput(0, player.Inventory.Count);
            //입력에 따른 실행
            switch (input)
            {
                case 0:
                    SceneManager.Instance.ExitScene();
                    break;
                default:
                        player.EquipItem(player.Inventory[input - 1]);
                    break;
            }
        }
    }
}

