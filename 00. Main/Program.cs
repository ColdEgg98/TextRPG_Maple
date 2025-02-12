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
            // ���� ���� �ʱ�ȭ �۾�
            //==============================================================================================================================
            LoadGameInfo();

            GameManager.Instance.Run();
        }



        static void LoadGameInfo()
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);


            // ���� �Ŵ��� ��� ����
            SoundManager.Instance.LoadSounds();
            SoundManager.Instance.PlaySound(SoundType.BGM, "MapleBGM", true);
            SoundManager.Instance.SetVolume(SoundType.BGM, 0.1f);

            // �÷��̾� ���� ����
            //GameObjectManager.Instance.AddGameObject(ObjectType.PLAYER, "MainPlayer", new Player(""));
            //Player? player = GameObjectManager.Instance.GetGameObject(ObjectType.PLAYER, "MainPlayer") as Player;

            // DB ��� ���� - Monster
            List<GameObject> monsters = DBManager.LoadFromCSV("MonsterDB.csv");
            foreach (GameObject monster in monsters)
            {
                GameObjectManager.Instance.AddPrototypeObject(ObjectType.MONSTER, monster.Name, monster);
            }

            // DB ��� ���� - Skill
            //List<Skill> skillList = new List<Skill>();
            //skillList.Add(new Skill("������ ����Ʈ", 3, SkillType.����, 2, "Mp�� �Һ��Ͽ� ������ �� �ټ��� ���ÿ� �����Ѵ�.", 10, false));
            //skillList.Add(new Skill("�귣��", 5, SkillType.����, 2.8f, "�� ���� �� �������� �� �� ���� �����Ѵ�.", 16, false));
            //skillList.Add(new Skill("�λ���¡", 8, SkillType.����, 4f, "������ �����Ͽ� �������� ���� ũ�� �����ߴ´�. ", 36, false));
            //skillList.Add(new Skill("��Ű ����", 3, SkillType.����, 1.1f, "����� ���� 7���� ǥâ�� ������.", 14, false));
            //skillList.Add(new Skill("Ʈ���� ���ο�", 6, SkillType.����, 2.9f, "3���� ǥâ�� ���ÿ� ������.", 20, false));
            //skillList.Add(new Skill("��ٿ� ç����", 9, SkillType.����, 7.8f, "��� �����ϴ�.", 50, false));
            //DBManager.SaveToCSV<Skill>("SkillDB.csv", skillList);

            List<Skill> skills = DBManager.LoadFromCSV<Skill>("SkillDB.csv");
            Skill.SetSkillList(skills);


            // DB ��� ���� - Item
            //List<Item> itemList = new List<Item>();
            //itemList.Add(new Item("������ ���", ItemType.Weapon, 70, "����-���", 100, false));
            //itemList.Add(new Item("������ ����", ItemType.Weapon, 50, "����-����", 200, false));
            //itemList.Add(new Item("������ �ܰ�", ItemType.Weapon, 30, "����-�ܰ�", 200, false));
            //itemList.Add(new Item("������ ����", ItemType.Armor, 40, "����-����", 500, false));
            //itemList.Add(new Item("������ �����", ItemType.Armor, 25, "����-�����", 300, false));
            //itemList.Add(new Item("������ �Ź�", ItemType.Armor, 20, "����-�Ź�", 100, false));
            //DBManager.SaveToCSV<Item>("itemDB.csv", itemList);

            List<Item> items = DBManager.LoadFromCSV<Item>("ItemDB.csv");
            Item.SetItemLists(items);


            // ANSI Escape Code
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                // ��� ���� �̸� �����Ͽ� Dictionary�� ����
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


            // �÷��̾� ���� ����
            GameObjectManager.Instance.AddGameObject(ObjectType.PLAYER, "MainPlayer", new Player(""));

            SceneManager.Instance.SetSceneInfo(scenes);
            SceneManager.Instance.EnterScene(SceneType.Title);
        }

        static void OnProcessExit(object sender, EventArgs e)
        {
            try
            {
                Console.Clear();

                // �����͸� �����ϴ� ����
                LogManager.Instance.Log(LogLevel.DEBUG, "���μ��� ���� ���� ������ ����...\n");

                // �÷��̾� ���� ���� (�κ��丮, ����, ���� ����, ��ų ���� ���)
                Player? p = GameObjectManager.Instance.GetGameObject(ObjectType.PLAYER, "MainPlayer") as Player;

                // ��� ����
                if (p != null)
                {
                    // �÷��̾� �κ��丮, ��ų, class ��ų ���� ����
                    FileIOManager.SaveJson(p, "Player");
                    FileIOManager.SaveJson(p.Inventory, "Inventory");
                    FileIOManager.SaveJson(p.Skills, "Skill");
                    FileIOManager.SaveJson(p.classSkill, "ClassSkill");
                    FileIOManager.SaveJson(p.Stat, "Status");
                }

                LogManager.Instance.Log(LogLevel.DEBUG, "\n���μ��� ���� ���� ������ �Ϸ�!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                LogManager.Instance.Log(LogLevel.ERROR, "������ ���� �� ���� �߻�: " + ex.Message);
                Console.ReadKey();
            }
        }
    }
}
