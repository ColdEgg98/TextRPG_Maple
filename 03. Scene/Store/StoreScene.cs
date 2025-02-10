using ConsoleApp1;
using ConsoleApp2._04._Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class StoreScene : IScene
    {
        // 상점흐름
        enum Pase
        {
            Intro,
            Buy,
            Sell
        }
        private Pase pase = Pase.Intro;
        Player player;
        private List<Item> itemList = new List<Item>();
        private List<Item> inventoryList = new List<Item>();
        int nowMoney = 999;
        
        public void Enter()
        {
            // player = 
            itemList = GetItemList();
            inventoryList = GetInvetoryList();
        }

        public void Exit()
        {
            
        }
        // 기본 정보 출력
        public void Render()
        {
            Console.Clear();
            Console.WriteLine("==== 상점 ====");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]\n");
            Console.WriteLine($"{nowMoney} G\n");

            Console.WriteLine("[아이템 목록]\n");
            // 아이템 정보 출력
            var targetList = pase == Pase.Sell ? inventoryList : itemList;
            if (pase != Pase.Sell)
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    Console.Write($"- {(pase != Pase.Intro ? $"{i + 1} " : "")} {itemList[i].Name}  |  {itemList[i].Price}G  |  {itemList[i].Descript}");
                    if (inventoryList != null && inventoryList.Any(item => item.Name == itemList[i].Name))
                    {
                        Console.WriteLine("  |  구매 완료");
                    }
                    else
                        Console.WriteLine("");
                }
            }
            else
            {
                for (int i = 0; i < inventoryList.Count; i++)
                {
                    Console.WriteLine($"- {(pase != Pase.Intro ? $"{i + 1} " : "")} {inventoryList[i].Name}  |  {inventoryList[i].Price}G  |  {inventoryList[i].Descript}  ");
                }
            }
            // 선택지 정보 출력
            Console.WriteLine("\n");
            switch (pase)
            {
                case Pase.Intro:
                    Console.WriteLine("1. 아이템 구매");
                    Console.WriteLine("2. 아이템 판매");
                    break;
                case Pase.Buy:
                    if (itemList.Count > 0)
                        Console.WriteLine($"1~{itemList.Count}. 아이템 구매");
                    break;
                case Pase.Sell:
                    if (itemList.Count > 0)
                        Console.WriteLine($"1~{inventoryList.Count}. 아이템 판매");
                    break;
            }
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
        }
        // 입력 실행
        public void Update()
        {
            //입력에 따른 실행
            // 기본 화면
            if (pase == Pase.Intro)
            {
                int input = GameManager.Instance.GetInput(0, 2);
                switch (input)
                {
                    case 0:
                        SceneManager.Instance.ChangeScene(SceneType.Town);
                        break;
                    case 1:
                        pase = Pase.Buy;
                        break;
                    case 2:
                        pase = Pase.Sell;
                        break;
                }
            }
            // 구매 화면
            else if (pase == Pase.Buy)
            {
                int input = GameManager.Instance.GetInput(0, itemList.Count);
                if(input == 0)
                {
                    pase = Pase.Intro;
                    return;
                }
                else
                {
                    input--;
                    if (nowMoney < itemList[input].Price)
                    {
                        Console.WriteLine("\n골드가 부족합니다.");
                        Thread.Sleep(1000);
                    }
                    else if (inventoryList != null && inventoryList.Any(item => item.Name == itemList[input].Name))
                    {
                        Console.WriteLine("\n이미 가지고 있는 아이템입니다.");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        //player.BuyItem(itemList[input]);
                        Console.WriteLine("\n구입에 성공했습니다.");
                        nowMoney -= itemList[input].Price;
                        inventoryList.Add(itemList[input]);
                        Thread.Sleep(1000);
                    }
                }
            }
            // 판매 화면
            else if (pase == Pase.Sell)
            {
                // 아이템 판매
                int input = GameManager.Instance.GetInput(0, inventoryList.Count);
                if (input == 0)
                {
                    pase = Pase.Intro;
                    return;
                }
                else
                {
                    input--;
                    
                    // 85%
                    nowMoney += itemList[input].Price * 85 / 100;
                    inventoryList.Remove(inventoryList[input]);
                    Console.WriteLine("\n판매에 성공했습니다.");
                    Thread.Sleep(1000);
                }
            }
        }
        // 아이템 목록을 가져오는 함수
        List<Item> GetItemList()
        {
            List<Item> items = new List<Item>
            {
                new Item { Name = "무한의 대검", Price = 1000, isSell = false, Descript =  "설명하는글 : 무한의 대검" },
                new Item { Name = "무한의 갑옷", Price = 800, isSell = false, Descript =  "설명하는글 : 무한의 갑옷" },
                new Item { Name = "무한의 반지", Price = 500, isSell = false, Descript =  "설명하는글 : 무한의 반지" },
                new Item { Name = "무한의 목걸이", Price = 300, isSell = false, Descript =  "설명하는글 : 무한의 목걸이" },
                new Item { Name = "무한의 장갑", Price = 200, isSell = false, Descript =  "설명하는글 : 무한의 장갑" },
                new Item { Name = "무한의 신발", Price = 100, isSell = false, Descript =  "설명하는글 : 무한의 신발" },
            };
            List<Item> items2 = items;
            return items2;
        }
        // 아이템 목록을 가져오는 함수
        List<Item> GetInvetoryList()
        {
            List<Item> items = new List<Item>
            {
                new Item { Name = "무한의 대검", Price = 1000, isSell = false, Descript =  "설명하는글 : 무한의 대검" },
                new Item { Name = "무한의 갑옷", Price = 800, isSell = false, Descript =  "설명하는글 : 무한의 갑옷" },
            };
            return items;
        }
    }
    // 임시 클래스
    class Item
    {
        public string Name { get; set; }
        public string Descript { get; set; }
        public int Price { get; set; }
        public Status Status { get; set; }
        public int SellPrice { get; set; }
        public bool isSell { get; set; }
        public int Count { get; set; }
    }
}
