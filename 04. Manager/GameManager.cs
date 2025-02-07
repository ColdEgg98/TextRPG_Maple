﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._04._Manager
{
    internal class GameManager
    {
        //========================================================================= 
        // 매니저 초기화
        //========================================================================= 
        private static GameManager instance;
        public static GameManager Instance => instance ??= new GameManager();

        //========================================================================= 
        // 필요한 멤버들
        //========================================================================= 

        //....

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
