using TextRPG_Maple._04._Manager;
using TextRPG_Maple._04._Manager._05._Object;
using TextRPG_Maple._05._Usable.Item;
namespace TextRPG_Maple
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
        private List<Item> itemList = new List<Item>();
        private List<Item> inventoryList = new List<Item>();
        int nowMoney = 999;
        
        public void Enter()
        {
            // player = 
            //itemList = GetItemList();
            inventoryList = GetInvetoryList();
        }

        public void Exit()
        {
            
        }
        // 기본 정보 출력
        public void Render()
        {
            Player? player = GameObjectManager.Instance.GetGameObject(ObjectType.PLAYER, "MainPlayer") as Player;
            List<Item> inventoryList = player.Inventory;


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
                    Console.Write($"- {(pase != Pase.Intro ? $"{i + 1} " : "")} {itemList[i].Name}  |  {itemList[i].Cost}G  |  {itemList[i].Descrip}");
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
                    Console.WriteLine($"- {(pase != Pase.Intro ? $"{i + 1} " : "")} {inventoryList[i].Name}  |  {inventoryList[i].Cost}G  |  {inventoryList[i].Descrip}  ");
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
                    if (nowMoney < itemList[input].Cost)
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
                        nowMoney -= itemList[input].Cost;
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
                    nowMoney += itemList[input].Cost * 85 / 100;
                    inventoryList.Remove(inventoryList[input]);
                    Console.WriteLine("\n판매에 성공했습니다.");
                    Thread.Sleep(1000);
                }
            }
        }
        // 아이템 목록을 가져오는 함수
        static List<Item> GetItemList()
        {
            //itemList.Clear();  // 기존 리스트를 초기화
            //itemList.AddRange( // 새로운 아이템 추가
            //[
            //    new Item("무한의 대검", ItemType.Weapon, 70, "설명문-대검", 100),
            //    new Item("무한의 직검", ItemType.Weapon, 50, "설명-직검", 200),
            //    new Item("무한의 단검", ItemType.Weapon, 30, "설명-단검", 200),
            //    new Item("무한의 반지", ItemType.Armor, 40, "설명문-반지", 500),
            //    new Item("무한의 목걸이", ItemType.Armor, 25, "설명-목걸이", 300),
            //    new Item("무한의 신발", ItemType.Armor, 20, "설명-신발", 100),
            //]);
            return new List<Item>();
        }
        // 아이템 목록을 가져오는 함수
        static List<Item> GetInvetoryList()
        {
            List<Item> items = new List<Item>
            {
                new Item("무한의 대검", ItemType.Weapon, 20, "설명 : 무한의 대검", 100),
                new Item("무한의 갑옷", ItemType.Armor, 20, "설명 : 무한의 갑옷", 100)
            };
            return items;
        }
    }
}
