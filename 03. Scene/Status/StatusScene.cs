using System.Diagnostics;
using System.Net.Http.Headers;
using TextRPG_Maple._04._Manager;
using TextRPG_Maple._04._Manager._05._Object;

namespace TextRPG_Maple._03._Scene.StatusScene
{
    internal class StatusScene : IScene
    {
        private Player? player;

        public void Enter()
        {
            player = GameObjectManager.Instance.GetGameObject(ObjectType.PLAYER, "MainPlayer") as Player;
        }

        public void Exit()
        {

        }

        // ���� ���
        public void Render()
        {
            Console.Clear();

            Console.WriteLine(
            "���� ����\n" +
            "ĳ������ ������ ǥ�õ˴ϴ�.\n\n"
            );

            Console.WriteLine("LV. {0:D2}", player.Level);
            Console.WriteLine("{0} ( {1} )", player.Name, player.Class);
            Console.WriteLine($"�� �� �� : {player.Stat.Atk} + {player.EquipAtk}");
            Console.WriteLine($"�� �� �� : {player.Stat.Def} + {player.EquipDef}");
            Console.WriteLine($"ü��(HP) : {player.Stat.Hp} / {player.Stat.MaxHp}"); 
            Console.WriteLine($"����(MP) : {player.Stat.Mp} / {player.Stat.MaxMp}"); 
            Console.WriteLine("");
            Console.Write("������ : ");
            InputManager.Instance.WriteLineColor($"{player.Stat.Gold}", ConsoleColor.DarkYellow);
            Console.WriteLine("");

            Console.WriteLine("0. ������");
        }

        public void Update()
        {

            int input = GameManager.Instance.GetInput(0, 0);
            
            switch (input)
            {
                case 0:
                    SceneManager.Instance.ExitScene();
                    break;
            }
        }
    }
}
 