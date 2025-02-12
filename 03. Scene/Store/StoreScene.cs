using System.Numerics;
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
        private Player player;
        private List<Item> itemList = new List<Item>();
        private List<Item> inventoryList = new List<Item>();
        int nowMoney = 999;

        public void Enter()
        {
            // player = 
            GetItemList();
            GetInvetoryList();

            SoundManager.Instance.StopSound(0);
            SoundManager.Instance.PlaySound(SoundType.BGM, "townscene", true);
            SoundManager.Instance.SetVolume(SoundType.BGM, 0.1f);
        
        }

        public void Exit()
        {

        }
        // 기본 정보 출력
        public void Render()
        {
            Console.Clear();

            switch (pase)
            {
                case Pase.Intro:
                    InputManager.Instance.WriteLineColor("==== 상점 ====", ConsoleColor.Green);
                    Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
                    break;
                case Pase.Buy:
                    InputManager.Instance.WriteLineColor("==== 상점 - 아이템 구매 ====", ConsoleColor.Green);
                    Console.WriteLine("원하는 아이템의 번호를 눌러 구매 할 수 있습니다.\n");
                    break;
                case Pase.Sell:
                    InputManager.Instance.WriteLineColor("==== 상점 - 아이템 판매 ====", ConsoleColor.Green);
                    Console.WriteLine("아이템을 판매하여 골드를 얻을 수 있습니다.\n");
                    break;
            }
            InputManager.Instance.WriteLineColor("[보유 골드]\n", ConsoleColor.Yellow);
            Console.WriteLine($"{nowMoney} G\n");

            Console.WriteLine("[아이템 목록]\n");

            // 아이템 정보 출력
            // 판매목록
            if (pase != Pase.Sell)
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    if (inventoryList != null && inventoryList.Any(item => item.Name == itemList[i].Name))
                    {
                        // 이미 소지중
                        InputManager.Instance.WriteLineColor($"- {(pase != Pase.Intro ? $"{i + 1} " : "")} {itemList[i].Name} |  {itemList[i].GetTypeString()}  |  {GetTypoName(itemList[i])} : {itemList[i].Value}  |    {itemList[i].Descrip}  |  구매 완료", ConsoleColor.DarkGray);
                    }
                    else
                        Console.WriteLine($"- {(pase != Pase.Intro ? $"{i + 1} " : "")} {itemList[i].UsableDisplay()}|  {GetTypoName(itemList[i])} : {itemList[i].Value}  |  {itemList[i].GetPriceString()}G");
                }
            }
            // 판매 : 인벤토리
            else
            {
                for (int i = 0; i < inventoryList.Count; i++)
                {
                    if (inventoryList[i].IsEquip)
                        InputManager.Instance.WriteLineColor($"- {(pase != Pase.Intro ? $"{i + 1} " : "")} {inventoryList[i].UsableDisplay()}|  {GetTypoName(itemList[i])} : {itemList[i].Value}  |  {inventoryList[i].GetPriceString()}G", ConsoleColor.Green);
                    else
                        Console.WriteLine($"- {(pase != Pase.Intro ? $"{i + 1} " : "")} {inventoryList[i].UsableDisplay()}|  {GetTypoName(itemList[i])} : {itemList[i].Value}  |  {inventoryList[i].GetPriceString()}G");
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
                        SceneManager.Instance.ExitScene();
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
                if (input == 0)
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
                        //player.Buy
                        Console.WriteLine("\n구입에 성공했습니다.");
                        int left = Console.CursorLeft;
                        int heigth = Console.CursorTop;
                        // Sound
                        SoundManager.Instance.PlaySound(SoundType.Click, "GetGold");
                        // PLAYER BUY
                        player.Stat.Gold -= itemList[input].Cost * 85 / 100;
                        nowMoney -= itemList[input].Cost;
                        Thread.Sleep(500);
                        // 이후 수정
                        player.Inventory.Add(itemList[input]);
                        Console.SetCursorPosition(left, heigth);
                        Console.WriteLine("\n바로 장착하시겠습니까? \n1) 예 \n0) 아니오");
                        int select = GameManager.Instance.GetInput(0, 1);
                        if (select == 1)
                        {
                            player.EquipItem(itemList[input]);
                            Console.WriteLine("\n 아이템을 장착하였습니다.");
                            Thread.Sleep(500);
                        }
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
                    // TODO: PLAYER SELL
                    player.Stat.Gold += itemList[input].Cost * 85 / 100;
                    // Sound
                    SoundManager.Instance.PlaySound(SoundType.Click, "GetGold");
                    // return 85%
                    nowMoney += itemList[input].Cost * 85 / 100;
                    // 장비 해제
                    if (inventoryList[input].IsEquip)
                        player.EquipItem(inventoryList[input]);
                    // 목록에서 삭제
                    inventoryList.Remove(inventoryList[input]);
                    Console.WriteLine("\n판매에 성공했습니다.");
                    Thread.Sleep(1000);
                }
            }
        }
        // 아이템 목록을 가져오는 함수
        void GetItemList()
        {
            itemList.Clear();  // 기존 리스트를 초기화
            itemList.AddRange( // 새로운 아이템 추가
            [
                new Item("무한의 대검", ItemType.Weapon, 70, "설명문-대검", 100),
                new Item("무한의 직검", ItemType.Weapon, 50, "설명-직검", 200),
                new Item("무한의 단검", ItemType.Weapon, 30, "설명-단검", 200),
                new Item("무한의 반지", ItemType.Armor, 40, "설명문-반지", 500),
                new Item("무한의 목걸이", ItemType.Armor, 25, "설명-목걸이", 300),
                new Item("무한의 신발", ItemType.Armor, 20, "설명-신발", 100),

            ]);

        }
        // 아이템 목록을 가져오는 함수
        void GetInvetoryList()
        {
            player = GameObjectManager.Instance.GetGameObject(ObjectType.PLAYER, "MainPlayer") as Player;
            nowMoney = player.Stat.Gold;
            inventoryList = player.Inventory;
        }
        string GetTypoName(Item item)
        {
            if (item.ItemType == ItemType.Armor)
                return "방어력";
            else if (item.ItemType == ItemType.Weapon)
                return "공격력";

            return "";
        }
    }
}
