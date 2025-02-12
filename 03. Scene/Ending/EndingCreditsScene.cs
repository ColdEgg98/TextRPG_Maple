using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Maple._04._Manager;


namespace TextRPG_Maple._03._Scene.Ending
{
    internal class EndingCreditsScene : IScene
    {
        public List<string> endingcreadits;
        public int ConsoleWriteLineIndex;

        public EndingCreditsScene()
        {
            endingcreadits = new List<string>()
                    {
                        " ",
                        "3 조",
                        "Maple Text RPG",
                        "------------------------",
                        "",
                        "팀장 : 신 찬영",
                        "",
                        "팀원 : 김 효중",
                        "     : 김 효준",
                        "     : 오 우택",
                        "     : 정 성욱",
                        "------------------------",
                        "플레이 해 주셔서 감사드립니다!",
                        "",
                        "0. 타이틀로",
                        "",
                    };
        }

        public void Enter()
        {
        
        }

        public void Exit()
        {

        }

        public void Render()
        {
            foreach (string ConsoleWriteLineIndex in endingcreadits)
            {
                Console.WriteLine(ConsoleWriteLineIndex);   // 한 줄 씩 순차적으로 출력
                Thread.Sleep(500);
            }

        }

        public void Update()
        {
            int input = GameManager.Instance.GetInput(0, 0);

            switch (input)
            {
                case 0:
                    SceneManager.Instance.ChangeScene(SceneType.Title);
                    break;
            }
        }
    }
}
