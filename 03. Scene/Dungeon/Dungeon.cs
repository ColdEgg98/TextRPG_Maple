using TextRPG_Maple._04._Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using TextRPG_Maple._01._GameObject.Monster;
using TextRPG_Maple._04._Manager._05._Object;
using TextRPG_Maple._04._Manager._04._Log;
using TextRPG_Maple._03._Scene.Dungeon;
using TextRPG_Maple._01._GameObject.Monster.Boss;
using TextRPG_Maple._04._Manager._06._DB;
using System.Threading;

namespace TextRPG_Maple
{
    internal class DungeonScene : IScene
    {
        public static int Floor { get; set; } = 1;   // 층수
        List<Monster> monsters = new List<Monster>();

        public void Enter()
        {
            SoundManager.Instance.StopSound(0);
            SoundManager.Instance.PlaySound(SoundType.BGM, "Maplestory BGM - Missing You");
            SoundManager.Instance.SetVolume(SoundType.BGM, 0.1f);
        }

        public void Exit()
        {
           
        }

        public void Render()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 전투에 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine($"3. 전투 시작 : {Floor}층");  // "전투 시작 : n층" 처럼 층수가 출력되어야 함. 기존 층수에 대한 정보가 존재. 
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
        }

        public void Update()
        {
            // 조건에 맞는 올바른 키를 입력할때 까지 반복
            int input = GameManager.Instance.GetInput(0, 3);

            //입력에 따른 실행
            switch (input)
            {
                case 1:
                    SceneManager.Instance.EnterScene(SceneType.StatusScene);
                    break;
                case 2:
                    //InventoryScreen();
                    SceneManager.Instance.EnterScene(SceneType.Inventory);
                    break;
                case 3:
                    Player? player = GameObjectManager.Instance.GetGameObject(ObjectType.PLAYER, "MainPlayer") as Player;
                    while (true)
                    {
                        BattleView battleView = new BattleView();
                        BattleController battleController = new BattleController(player, GetMonsters(), battleView);
                        int keepGoing = battleController.StartBattle();

                        // 오보젝트 삭제
                        monsters.Clear();

                        // 다음 단계 진행
                        if (keepGoing > 0) // 1(clear and stop) or 2(clear and go)
                        {
                            Floor++;
                            player.DungeonFloor = Floor;
                        }

                        if (keepGoing != 2)
                        {
                            break;
                        }
                    }

                    // 만약 Floor가 30층을 깨서 31이 된 경우.
                    if(Floor == 31)
                    {
                        // 엔딩 씬으로 이동
                        SceneManager.Instance.ChangeScene(SceneType.EndingCreditsScene);
                    }

                    break;
                case 0:
                    SceneManager.Instance.ExitScene();
                    break;
            }
        }


        // 던전에서 등장할 몬스터를 설정
        List<Monster> GetMonsters()
        {
            monsters.Clear();
            if ((Floor % 5) != 0)
            {
                // 랜덤한 숫자의 몬스터를 소환
                Random rnd = new Random();
                int count = rnd.Next(1, 5);

                for (int i = 0; i < count; ++i)
                {
                    // 게임 오브젝트 매니저로부터 랜덤함 몬스터의 프로토타입 정보를 가져옴.
                    Monster? monster = GameObjectManager.Instance.GetRandomPrototype(ObjectType.MONSTER) as Monster;
                    if (monster != null)
                    {
                        // 몬스터를 원본으로부터 복제
                        monster = monster.Clone() as Monster;

                        // 층수에 따라 몬스터의 능력치와 보상을 증가.
                        monster.AddAbility(Floor);

                        // 몬스터 정보 추가
                        monsters.Add(monster);
                    }
                }
            }
            // 10층 마다 보스 몬스터일 경우
            else
            {
                if (Floor == 5)
                {
                    Boss? boss = GameObjectManager.Instance.ClonePrototypeObject(ObjectType.BOSS, "혼테일") as Boss;
                    Boss b = boss.Clone() as Boss;
                    monsters.Add(b);
                }
                else if (Floor == 10)
                {
                    Boss? boss = GameObjectManager.Instance.ClonePrototypeObject(ObjectType.BOSS, "가디언 엔젤 슬라임") as Boss;
                    boss = boss.Clone() as Boss;
                    monsters.Add(boss);
                }
            }

            return monsters;
        }
    }
}
