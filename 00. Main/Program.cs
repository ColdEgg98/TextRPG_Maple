using TextRPG_Maple._04._Manager;
using System.ComponentModel;
using System.Threading.Channels;

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

            // 모든 씬을 미리 생성하여 Dictionary에 저장
            var scenes = new Dictionary<SceneType, IScene>
                {
                { SceneType.Town, new TownScene() },
                { SceneType.Store, new StoreScene() },
                { SceneType.Start, new StartScene() },
                { SceneType.Title, new TitleScene() },
                { SceneType.Dungeon, new DungeonScene()},
            };

            SceneManager.Instance.SetSceneInfo(scenes);
            SceneManager.Instance.EnterScene(SceneType.Title);

            GameManager.Instance.Run();
        }
    }
}
