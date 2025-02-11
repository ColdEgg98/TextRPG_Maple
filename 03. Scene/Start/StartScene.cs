using TextRPG_Maple._04._Manager;
using TextRPG_Maple._04._Manager._05._Object;
namespace TextRPG_Maple
{
    internal class StartScene : IScene
    {
        private Player player;
        private bool endNameSetting = false;
        private bool endJobSetting = false;
        private string userName;
        private int userJob;
        public void Enter()
        {
            // TODO : 데이터가 있는지 확인
        }

        public void Exit()
        {

        }

        public void Render()
        {
            //Console.Clear();

            #region NameSetting
            if (!endNameSetting)
            {
                Console.WriteLine("메이플 월드에 오신 대적자님 환영합니다.");
                Console.Write("당신의 이름을 알려주세요 : ");
            }
            #endregion

            // Job Setting
            #region JobSetting
            if (endNameSetting)
            {
                Console.WriteLine("안녕하세요. " + userName + "님\n");
                Console.WriteLine("직업을 선택해주세요\n");
                Console.WriteLine("1) 전사");
                Console.WriteLine("2) 도적\n");
                //player = new Player(userName, userJob);
            }
            #endregion
            if (endJobSetting)
            {
                Console.WriteLine("환영합니다." + player.Name + "님");
                Console.WriteLine("직업 : " + player.Class + "로 시작합니다.");
            }
        }

        public void Update()
        {
            // name setting
            if (!endNameSetting)
            {
                userName = Console.ReadLine();
                endNameSetting = true;
            }
            // job setting
            else if (!endJobSetting)
            {
                userJob = GameManager.Instance.GetInput(1, 2);
                player = new Player(userName);
                player.SetClass(userJob);
                GameObjectManager.Instance.AddGameObject(ObjectType.PLAYER, "MainPlayer", player);
                endJobSetting = true;
            }
            else
            {
                Thread.Sleep(1000);
                SceneManager.Instance.ChangeScene(SceneType.Town);
            }
        }
    }
}
