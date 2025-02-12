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

            InputManager.Instance.WriteLineColor("���� ����", ConsoleColor.Yellow);
            Console.WriteLine(
            "\nĳ������ ������ ǥ�õ˴ϴ�.\n\n"
            );

            Console.WriteLine("LV. {0:D2}", player.Level);
            Console.WriteLine("{0} ( {1} )", player.Name, player.Class);
            Console.WriteLine("�� �� �� : {0}", player.Stat.Atk);
            Console.WriteLine("�� �� �� : {0}", player.Stat.Def);
            Console.WriteLine("ü��(HP) : {0}", player.Stat.Hp); 
            Console.WriteLine("����(MP) : {0}", player.Stat.Mp);
            Console.WriteLine("");
            Console.WriteLine("�� �� �� : {0} G\n", player.Stat.Gold);

            Console.WriteLine("0. ������");
        }

        public void Update()
        {

            int input = GameManager.Instance.GetInput(0, 2);
            
            switch (input)
            {
                case 0:
                    SceneManager.Instance.ExitScene();
                    break;
                case 2:
                    SceneManager.Instance.ChangeScene(SceneType.EndingCreditsScene);
                    break;
            }
        }
    }
}
 