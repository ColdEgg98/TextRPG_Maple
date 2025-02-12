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
            // 사운드 매니저 사용 예제
            SoundManager.Instance.LoadSounds();
            SoundManager.Instance.PlaySound(SoundType.BGM, "aLIEz_Piano", true);
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

            // 프로토타입으로 부터 객체를 복사
            GameObject? goblin = GameObjectManager.Instance.ClonePrototypeObject(ObjectType.MONSTER, "고블린");

 
            // 모든 씬을 미리 생성하여 Dictionary에 저장
            var scenes = new Dictionary<SceneType, IScene>
                {
                { SceneType.Town, new TownScene() },
                { SceneType.Store, new StoreScene() },
                { SceneType.Start, new StartScene() },
                { SceneType.Title, new TitleScene() },
                { SceneType.Dungeon, new DungeonScene()},
                { SceneType.SkillScene, new SkillScene() },
                { SceneType.EquipSkillScene, new EquipSkillScene() },
                { SceneType.Inventory, new InventoryScene() },
                { SceneType.EquipInventory, new EquipInventory() },
            };

            SceneManager.Instance.SetSceneInfo(scenes);
            SceneManager.Instance.EnterScene(SceneType.Title);

            GameManager.Instance.Run();
        }
    }
}
