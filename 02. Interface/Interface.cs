using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal interface IScene
    {
        // 씬에 들어갈 때 호출
        void Enter();

        // 씬에 대한 정보 소개
        void Render();

        // 행동 선택 기능
        void Update();

        // 씬에서 나갈 때 호출
        void Exit();
    }
}
