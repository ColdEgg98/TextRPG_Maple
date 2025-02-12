using TextRPG_Maple._04._Manager;
using System.ComponentModel;
using System.Threading.Channels;
using System.Runtime.Serialization;
using TextRPG_Maple._04._Manager._05._Object;
using TextRPG_Maple._03._Scene.SkillScene;
using TextRPG_Maple._04._Manager._06._DB;

namespace TextRPG_Maple
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ���� �Ŵ��� ��� ����
            SoundManager.Instance.LoadSounds();
            //SoundManager.Instance.PlaySound(SoundType.BGM, "aLIEz_Piano", true);
            //SoundManager.Instance.SetVolume(SoundType.BGM, 0.3f);

            // DB�� ���� ������ ������
            List<GameObject> monsters = DBManager.LoadFromCSV("MonsterDB.csv");
            foreach (GameObject monster in monsters)
            {
                GameObjectManager.Instance.AddPrototypeObject(ObjectType.MONSTER, monster.Name, monster);
            }

            GameObject? goblin = GameObjectManager.Instance.ClonePrototypeObject(ObjectType.MONSTER, "���");


            // ���� ������Ʈ �Ŵ��� ��� ���� - �÷��̾� ���� �ʱ�ȭ
            //GameObjectManager.Instance.AddGameObject(ObjectType.PLAYER, "MainPlayer", new Player(""));
            //Player? player = GameObjectManager.Instance.GetGameObject(ObjectType.PLAYER, "MainPlayer") as Player;

            // ��� ���� �̸� �����Ͽ� Dictionary�� ����
            var scenes = new Dictionary<SceneType, IScene>
                {
                { SceneType.Town, new TownScene() },
                { SceneType.Store, new StoreScene() },
                { SceneType.Start, new StartScene() },
                { SceneType.Title, new TitleScene() },
                { SceneType.Dungeon, new DungeonScene()},
                { SceneType.SkillScene, new SkillScene() }
            };

            SceneManager.Instance.SetSceneInfo(scenes);
            SceneManager.Instance.EnterScene(SceneType.Title);

            GameManager.Instance.Run();
        }
    }
}
