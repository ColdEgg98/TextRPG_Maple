using TextRPG_Maple._04._Manager;
using System.ComponentModel;
using System.Threading.Channels;
using System.Runtime.Serialization;
using TextRPG_Maple._04._Manager._05._Object;
using TextRPG_Maple._03._Scene.SkillScene;
using TextRPG_Maple._04._Manager._06._DB;
using TextRPG_Maple._03._Scene.Inventory;
using TextRPG_Maple._04._Manager._04._Log;
using TextRPG_Maple._04._Manager._07._FileIO;
using TextRPG_Maple._05._Usable.Skill;
using TextRPG_Maple._05._Usable.Item;
using TextRPG_Maple._03._Scene.StatusScene;
using TextRPG_Maple._03._Scene.Ending;

namespace TextRPG_Maple
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //==============================================================================================================================
            // 게임 관련 초기화 작업
            //==============================================================================================================================
            LoadGameInfo();

            GameManager.Instance.Run();
        }



        static void LoadGameInfo()
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);


            // 사운드 매니저 사용 예제
            SoundManager.Instance.LoadSounds();
            SoundManager.Instance.PlaySound(SoundType.BGM, "MapleBGM", true);
            SoundManager.Instance.SetVolume(SoundType.BGM, 0.1f);

            // 플레이어 생성 예제
            //GameObjectManager.Instance.AddGameObject(ObjectType.PLAYER, "MainPlayer", new Player(""));
            //Player? player = GameObjectManager.Instance.GetGameObject(ObjectType.PLAYER, "MainPlayer") as Player;

            // DB 사용 예제 - Monster
            List<GameObject> monsters = DBManager.LoadFromCSV("MonsterDB.csv");
            foreach (GameObject monster in monsters)
            {
                GameObjectManager.Instance.AddPrototypeObject(ObjectType.MONSTER, monster.Name, monster);
            }

            // DB 사용 예제 - Skill
            //List<Skill> skillList = new List<Skill>();
            //skillList.Add(new Skill("슬래시 블러스트", 3, SkillType.전사, 2, "Mp를 소비하여 주위의 적 다수를 동시에 공격한다.", 10, false));
            //skillList.Add(new Skill("브랜디쉬", 5, SkillType.전사, 2.8f, "눈 앞의 적 여러명을 두 번 연속 공격한다.", 16, false));
            //skillList.Add(new Skill("인사이징", 8, SkillType.전사, 4f, "정신을 집중하여 전방으로 검을 크게 내리긋는다. ", 36, false));
            //skillList.Add(new Skill("럭키 세븐", 3, SkillType.도적, 1.1f, "행운을 담은 7개의 표창을 던진다.", 14, false));
            //skillList.Add(new Skill("트리플 스로우", 6, SkillType.도적, 2.9f, "3개의 표창을 동시에 던진다.", 20, false));
            //skillList.Add(new Skill("쇼다운 챌린지", 9, SkillType.도적, 7.8f, "살살 때립니다.", 50, false));
            //DBManager.SaveToCSV<Skill>("SkillDB.csv", skillList);

            List<Skill> skills = DBManager.LoadFromCSV<Skill>("SkillDB.csv");
            Skill.SetSkillList(skills);


            // DB 사용 예제 - Item
            //List<Item> itemList = new List<Item>();
            //itemList.Add(new Item("무한의 대검", ItemType.Weapon, 70, "설명문-대검", 100, false));
            //itemList.Add(new Item("무한의 직검", ItemType.Weapon, 50, "설명-직검", 200, false));
            //itemList.Add(new Item("무한의 단검", ItemType.Weapon, 30, "설명-단검", 200, false));
            //itemList.Add(new Item("무한의 반지", ItemType.Armor, 40, "설명문-반지", 500, false));
            //itemList.Add(new Item("무한의 목걸이", ItemType.Armor, 25, "설명-목걸이", 300, false));
            //itemList.Add(new Item("무한의 신발", ItemType.Armor, 20, "설명-신발", 100, false));
            //DBManager.SaveToCSV<Item>("itemDB.csv", itemList);

            List<Item> items = DBManager.LoadFromCSV<Item>("ItemDB.csv");
            Item.SetItemLists(items);


            // ANSI Escape Code
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                // 모든 씬을 미리 생성하여 Dictionary에 저장
                var scenes = new Dictionary<SceneType, IScene>
                {
                { SceneType.Town, new TownScene() },
                { SceneType.Store, new StoreScene() },
                { SceneType.Start, new StartScene() },
                { SceneType.Title, new TitleScene() },
                { SceneType.StatusScene, new StatusScene() },
                { SceneType.Dungeon, new DungeonScene()},
                {SceneType.EndingCreditsScene, new EndingCreditsScene()},
                { SceneType.SkillScene, new SkillScene() },
                { SceneType.EquipSkillScene, new EquipSkillScene() },
                { SceneType.Inventory, new InventoryScene() },
                { SceneType.EquipInventory, new EquipInventory() },
            };


            // 플레이어 정보 생성
            GameObjectManager.Instance.AddGameObject(ObjectType.PLAYER, "MainPlayer", new Player(""));

            SceneManager.Instance.SetSceneInfo(scenes);
            SceneManager.Instance.EnterScene(SceneType.Title);
        }

        static void OnProcessExit(object sender, EventArgs e)
        {
            try
            {
                Console.Clear();

                // 데이터를 저장하는 로직
                LogManager.Instance.Log(LogLevel.DEBUG, "프로세스 종료 전에 데이터 저장...\n");

                // 플레이어 정보 저장 (인벤토리, 스탯, 착용 정보, 스킬 정보 등등)
                Player? p = GameObjectManager.Instance.GetGameObject(ObjectType.PLAYER, "MainPlayer") as Player;

                // 사용 예제
                if (p != null)
                {
                    // 플레이어 인벤토리, 스킬, class 스킬 정보 저장
                    FileIOManager.SaveJson(p, "Player");
                    FileIOManager.SaveJson(p.Inventory, "Inventory");
                    FileIOManager.SaveJson(p.Skills, "Skill");
                    FileIOManager.SaveJson(p.classSkill, "ClassSkill");
                    FileIOManager.SaveJson(p.Stat, "Status");
                }

                LogManager.Instance.Log(LogLevel.DEBUG, "\n프로세스 종료 전에 데이터 완료!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                LogManager.Instance.Log(LogLevel.ERROR, "데이터 저장 중 예외 발생: " + ex.Message);
                Console.ReadKey();
            }
        }
    }
}
