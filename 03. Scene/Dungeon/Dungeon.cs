﻿using TextRPG_Maple._04._Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using TextRPG_Maple._01._GameObject.Monster;
using TextRPG_Maple._04._Manager._05._Object;
using TextRPG_Maple._04._Manager._04._Log;
using TextRPG_Maple._03._Scene.Dungeon;
using TextRPG_Maple._04._Manager._06._DB;

namespace TextRPG_Maple
{
    internal class DungeonScene : IScene
    {
        public int Floor { get; set; } = 1;   // 층수
        List<string> nameList = new List<string>();
        public void Enter()
        {
            List<GameObject> monsters = DBManager.LoadFromCSV("MonsterDB.csv");
            foreach (GameObject monster in monsters)
            {
                GameObjectManager.Instance.AddPrototypeObject(ObjectType.MONSTER, monster.Name, monster);
            }

            // 프로토타입으로 부터 객체를 복사
            GameObject? goblin = GameObjectManager.Instance.ClonePrototypeObject(ObjectType.MONSTER, "고블린");
        }
        public void Exit()
        {
           
        }

        public void Render()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 전투에 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine($"3. 전투 시작 : {Floor}층");  // "전투 시작 : n층" 처럼 층수가 출력되어야 함. 기존 층수에 대한 정보가 존재. 
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
        }

        public void Update()
        {
            // 조건에 맞는 올바른 키를 입력할때 까지 반복
            int input = GameManager.Instance.GetInput(0, 3);

            //입력에 따른 실행
            switch (input)
            {
                case 1:
                    //StatusScreen();
                    break;
                case 2:
                    //InventoryScreen();
                    SceneManager.Instance.EnterScene(SceneType.Inventory);
                    break;
                case 3:
                    // 레벨에 따른 몬스터 정보를 여기서 삽입


                    Player? player = GameObjectManager.Instance.GetGameObject(ObjectType.PLAYER, "MainPlayer") as Player;
                    while (true)
                    {
                        BattleView battleView = new BattleView();
                        BattleController battleController = new BattleController(player, GetMonsters(), battleView);
                        int keepGoing = battleController.StartBattle();
                        // 오보젝트 삭제
                        DeleteMonster();
                        // 다음 단계 진행
                        if (keepGoing > 0) // 1(clear and stop) or 2(clear and go)
                        {
                            Floor++;
                        }
                        if (keepGoing == 1)
                        {
                            break;
                        }
                    }
                    break;
                case 0:
                    SceneManager.Instance.ExitScene();
                    break;
            }
        }


        List<Monster> GetMonsters()
        {
            // 던전에서 등장할 몬스터를 설정
            // 난이도 관련되선 나중에 추가
            List<Monster> monsters = new List<Monster>();
            Monster? goblin = GameObjectManager.Instance.ClonePrototypeObject(ObjectType.MONSTER, "고블린") as Monster;
            Monster? orc = GameObjectManager.Instance.ClonePrototypeObject(ObjectType.MONSTER, "오크") as Monster;
            Monster? wolf = GameObjectManager.Instance.ClonePrototypeObject(ObjectType.MONSTER, "늑대") as Monster;

            if (Floor == 1)
            {
                Monster? wolf1 = GameObjectManager.Instance.GetGameObject(ObjectType.MONSTER, "늑대") as Monster;
                monsters.Add(wolf1);
                nameList.Add("늑대");
            }
            else if (Floor == 2)
            { 
                Monster? wolf1 = GameObjectManager.Instance.GetGameObject(ObjectType.MONSTER, "늑대 A") as Monster;
                Monster? wolf2 = GameObjectManager.Instance.GetGameObject(ObjectType.MONSTER, "늑대 B") as Monster;
                monsters.Add(wolf1);
                monsters.Add(wolf2);
                nameList.Add("늑대 A");
                nameList.Add("늑대 B");
            }

            //return monster
            return monsters;
        }
        void DeleteMonster()
        {
            //public bool RemoveGameObject(ObjectType type, string name)
            for (int i = 0; i < nameList.Count; i++)
            {
                GameObjectManager.Instance.RemoveGameObject(ObjectType.MONSTER, nameList[i]);
            }
            nameList.Clear();
        }
    }
}
