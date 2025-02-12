using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using NAudio.CoreAudioApi;
using TextRPG_Maple._04._Manager;
using TextRPG_Maple._04._Manager._05._Object;

namespace TextRPG_Maple._03._Scene.Rest
{
    internal class RestScene : IScene
    {
        private Player? player;
        int restgold = 200;

        public void Enter()
        {
            player = GameObjectManager.Instance.GetGameObject(ObjectType.PLAYER, "MainPlayer") as Player;

            SoundManager.Instance.StopSound(0);
            SoundManager.Instance.PlaySound(SoundType.BGM, "restscene", true);
            SoundManager.Instance.SetVolume(SoundType.BGM, 0.1f);
        }
        public void Exit()
        {
        
        }
        public void Render()
        {
            Console.Clear();

            InputManager.Instance.WriteLineColor("휴식하기", ConsoleColor.Yellow);
            Console.WriteLine("{0} G를 지불하면 체력과 마력을 회복할 수 있습니다.\n", restgold);

            Console.WriteLine("현재 체력(HP) : {0}", player.Stat.Hp);
            Console.WriteLine("현재 마력(MP) : {0}", player.Stat.Mp);
            Console.WriteLine("현재 소 지 금 : {0} G\n", player.Stat.Gold);

            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");

        }

        public void Update()
        {
            int input = GameManager.Instance.GetInput(0, 1);

            switch (input)
            {
                case 0:
                    SceneManager.Instance.ExitScene();
                    break;
                case 1:
                    if (player.Stat.Gold >= restgold)
                    {
                        player.Stat.Hp = player.Stat.MaxHp;
                        player.Stat.Mp = player.Stat.MaxMp;
                        player.Stat.Gold -= restgold;
                        restgold += 50;
                        Console.WriteLine("\n휴식을 완료했습니다.");
                        Thread.Sleep(600);
                    }
                    else
                    {
                        Console.WriteLine("\n골드가 부족합니다.");
                        Thread.Sleep(600);
                    }
                    break;
            }

        }
    }
    
}
