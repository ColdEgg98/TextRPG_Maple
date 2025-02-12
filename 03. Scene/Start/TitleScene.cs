using System.Numerics;
using TextRPG_Maple._04._Manager;
using TextRPG_Maple._04._Manager._04._Log;
using TextRPG_Maple._04._Manager._05._Object;
using TextRPG_Maple._04._Manager._07._FileIO;
using TextRPG_Maple._05._Usable.Item;
using TextRPG_Maple._05._Usable.Skill;
namespace TextRPG_Maple
{
    internal class TitleScene : IScene
    {
        private bool hasData = false;
        public void Enter()
        {
            // TODO : 데이터가 있는지 확인

            string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
            string SaveDirectory = Path.Combine(projectRoot, "..", "..", "..", "Resources", "Saves");

            // 저장 폴더의 유뮤 확인
            // 폴더가 없다면
            if (!Directory.Exists(SaveDirectory))
            {
                hasData = false;
            }
            // 폴더가 있다면 플레이어 정보 Load
            else
            {
                hasData = true;
            }
        }

        public void Exit()
        {

        }

        public void Render()
        {
            Console.Clear();

            DrawTitle();

            // menu

            Console.SetCursorPosition(15, 17);
            Console.WriteLine("1. 처음부터 시작");
            if (hasData)
            {
                Console.SetCursorPosition(15, 18);
                Console.WriteLine("2. 이어서 시작");
            }
            Console.SetCursorPosition(15, 19);
            Console.WriteLine("0. 게임 종료");

            // meny box
            DrawWall(50, 6, 10, 15);

            // outline
            DrawWall(72, 27, 0, 0);
        }

        public void Update()
        {
            // 조건에 맞는 올바른 키를 입력할때 까지 반복
            Console.SetCursorPosition(18, 23);
            int input = GameManager.Instance.GetInput(0, hasData? 2 : 1);
            //입력에 따른 실행
            switch (input)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    //new game
                    SceneManager.Instance.ChangeScene(SceneType.Start);
                    break;
                case 2:
                    //continue game

                    // 데이터가 Saves 폴더에 무조건 있을 것이라 가정하자. 원래는 여러 안전 장치가 필요한데 일단 기능만 넣음.
                    //Player player = GameObjectManager.Instance.GetGameObject(ObjectType.PLAYER, "MainPlayer") as Player;

                    // 1. 플레이어 정보 불러오기
                    Player player = FileIOManager.LoadJson<Player>("Player");

                    // 플레이어가 가지고 있는 던전 층수에 대한 정보를 던전씬에 대입
                    DungeonScene.Floor = player.DungeonFloor;

                    if (player != null)
                    {
                        GameObjectManager.Instance.RemoveGameObject(ObjectType.PLAYER, "MainPlayer");
                        GameObjectManager.Instance.AddGameObject(ObjectType.PLAYER, "MainPlayer", player);
                        Console.WriteLine("플레이어 불러오기 완료!");
                    }

                    //// 1. 스탯 정보 불러오기
                    //var stat = FileIOManager.LoadJson<Status>("Status");
                    //if (stat != null)
                    //{
                    //    player.Stat = stat;
                    //    Console.WriteLine("스탯정보 불러오기 완료!");
                    //}

                    //// 2. 인벤토리 불러오기
                    //var Inventory = FileIOManager.LoadJson<List<Item>>("Inventory");
                    //if (Inventory != null)
                    //{
                    //    player.Inventory = Inventory;
                    //    Console.WriteLine("인벤토리 불러오기 완료!");
                    //}

                    //// 3. 스킬 불러오기
                    //var Skill = FileIOManager.LoadJson<List<Skill>>("Skill");
                    //if (Skill != null)
                    //{
                    //    player.Skills = Skill;
                    //    Console.WriteLine("스킬 불러오기 완료!");
                    //}
                    //var classSkill = FileIOManager.LoadJson<List<Skill>>("ClassSkill");
                    //if (classSkill != null)
                    //{
                    //    player.classSkill = classSkill;
                    //    Console.WriteLine("class 스킬 불러오기 완료!");
                    //}

                    SceneManager.Instance.ChangeScene(SceneType.Town);
                    break;
            }
        }
        public void DrawWall(int x, int y, int posX, int posY)
        {
            // 상단 가로선
            Console.SetCursorPosition(posX, posY);
            Console.Write(new string('=', x + 1));

            // 세로선
            for (int i = 1; i < y; i++)
            {
                Console.SetCursorPosition(posX, posY + i);
                Console.Write("|"); // 왼쪽 벽

                Console.SetCursorPosition(posX + x, posY + i);
                Console.Write("|"); // 오른쪽 벽
            }

            // 하단 가로선
            Console.SetCursorPosition(posX, posY + y);
            Console.Write(new string('=', x + 1));
        }

        public void DrawTitle()
        {
            Console.SetCursorPosition(0, 3);
            Console.WriteLine("                                                                        \r\n ######    #####   ##  ##   ######            #####    #####     #####  \r\n   ##     ##        ####      ##              ##  ##   ##  ##   ##      \r\n   ##     ####       ##       ##              ##  ##   ##  ##   ##      \r\n   ##     ##        ####      ##              #####    #####    ## ###  \r\n   ##     ##       ##  ##     ##              ## ##    ##       ##  ##  \r\n   ##      #####   ##  ##     ##              ##  ##   ##        ####   \r\n                                                                        \r\n");
        }
    }
}
