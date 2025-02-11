using TextRPG_Maple._04._Manager;
using System.ComponentModel;
using System.Threading.Channels;
using System.Runtime.Serialization;
using TextRPG_Maple._04._Manager._05._Object;
using TextRPG_Maple._03._Scene.SkillScene;
using System.Numerics;
using TextRPG_Maple._05._Usable.Skill;
using TextRPG_Maple._03._Scene.Inventory;

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

            // 게임 오브젝트 매니저 사용 예제 - 플레이어 정보 초기화
            GameObjectManager.Instance.AddGameObject(ObjectType.PLAYER, "MainPlayer", new Player(""));
            Player? player = GameObjectManager.Instance.GetGameObject(ObjectType.PLAYER, "MainPlayer") as Player;

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

            //테스트용
            player.AddSkill(Skill.Warrior_skillSet[0]);
            player.AddSkill(Skill.Warrior_skillSet[1]);
            player.AddSkill(Skill.Warrior_skillSet[2]);

            GameManager.Instance.Run();
        }
    }
}
