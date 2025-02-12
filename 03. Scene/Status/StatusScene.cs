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

        // 정보 출력
        public void Render()
        {
            Console.Clear();

            Console.WriteLine(
            "상태 보기\n" +
            "캐릭터의 정보가 표시됩니다.\n\n"
            );

            Console.WriteLine("LV. {0:D2}", player.Level);
            Console.WriteLine("{0} ( {1} )", player.Name, player.Class);
            Console.WriteLine("공  격  력 : {0}", player.Stat.Atk);
            Console.WriteLine("방  어  력 : {0}", player.Stat.Def);
            Console.WriteLine("체 력 (HP) : {0}", player.Stat.Hp);
            Console.WriteLine("마 력 (MP) : {0}", player.Stat.Mp);
            Console.WriteLine("");
            Console.WriteLine("소  지  금 : {0} 메 소\n", player.Stat.Gold);

            Console.WriteLine("0. 나가기");
        }

        public void Update()
        {

            int input = GameManager.Instance.GetInput(0, 0);
            
            switch (input)
            {
                case 0:
                    SceneManager.Instance.ChangeScene(SceneType.Town);
                    break;
            }
        }
    }
}
 