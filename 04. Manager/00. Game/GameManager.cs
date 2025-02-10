namespace TextRPG_Maple._04._Manager
{
    internal class GameManager
    {
        //========================================================================= 
        // 매니저 초기화
        //========================================================================= 
        private static GameManager? instance;
        public static GameManager Instance => instance ??= new GameManager();

        //========================================================================= 
        // 필요한 멤버들
        //========================================================================= 

        //....

        public static Player player;

        public void Run()
        {
            SceneManager.Instance.RunCurrentScene();
        }

        public int GetInput(int min, int max)
        {
            return InputManager.Instance.GetInput(min, max);
        }
    }
}