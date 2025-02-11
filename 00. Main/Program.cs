using TextRPG_Maple._04._Manager;
using System.ComponentModel;
using System.Threading.Channels;
using System.Runtime.Serialization;
using TextRPG_Maple._04._Manager._05._Object;
using TextRPG_Maple._03._Scene.SkillScene;

namespace TextRPG_Maple
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 사운드 매니저 사용 예제
            SoundManager.Instance.LoadSounds();
            SoundManager.Instance.PlaySound(SoundType.BGM, "aLIEz_Piano", true);
            SoundManager.Instance.SetVolume(SoundType.BGM, 0.3f);

            // 플레이어 정보 초기화
            GameObjectManager.Instance.AddGameObject(ObjectType.PLAYER, "MainPlayer", new Player(""));

            GameObject player = GameObjectManager.Instance.GetGameObject(ObjectType.PLAYER, "MainPlayer");

            // 모든 씬을 미리 생성하여 Dictionary에 저장
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
