using TextRPG_Maple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Maple._04._Manager
{
    public enum SceneType
    {
        Town,
        Start,
        Title,
        Store,
        SkillScene,
        StatusScene,
        EquipSkillScene,
        Dungeon,
        Inventory,
        EquipInventory,
        End
    }

    internal class SceneManager
    {
        private static SceneManager? instance;
        public static SceneManager Instance => instance ??= new SceneManager();

        private Stack<IScene> sceneStack = new Stack<IScene>();
        private Dictionary<SceneType, IScene> scenes = new Dictionary<SceneType, IScene>();


        // 생성자를 통해 씬 정보를 외부에서 주입
        public void SetSceneInfo(Dictionary<SceneType, IScene> initialScenes)
        {
            scenes = initialScenes;
        }

        public void ChangeScene(SceneType sceneType)
        {
            ExitScene();
            EnterScene(sceneType);
        }

        public void ExitScene()
        {
            if (sceneStack.Count > 0)
            {
                IScene currentScene = sceneStack.Peek();
                currentScene.Exit();

                sceneStack.Pop();
            }
        }
        public void EnterScene(SceneType sceneType)
        {
            if (scenes.TryGetValue(sceneType, out IScene scene))
            {
                sceneStack.Push(scene);
                scene.Enter();
            }
        }

        public void RunCurrentScene()
        {
            if (sceneStack.Count == 0)
                return;

            // 씬이 종료되지 않도록 Run을 호출하도록 수정
            while (true)
            {
                Console.Clear();

                IScene currentScene = sceneStack.Peek();
                currentScene.Render();
                currentScene.Update();
            }
        }
    }
}
