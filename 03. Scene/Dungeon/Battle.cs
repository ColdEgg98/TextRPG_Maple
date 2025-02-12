using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Maple._01._GameObject.Monster;
using TextRPG_Maple._04._Manager;
using TextRPG_Maple._04._Manager._04._Log;
using TextRPG_Maple._04._Manager._05._Object;

namespace TextRPG_Maple._03._Scene.Dungeon
{
    internal class BattleView
    {
        public void ShowBattleStatus(Player player, List<Monster> monsters)
        {
            Console.WriteLine("==== 전투 시작 ====");
            Console.Write($"플레이어: {player.Name}");
            InputManager.Instance.WriteLineColor(DisplayHP(player.Stat.Hp, player.Stat.MaxHp), ConsoleColor.DarkRed);

            Console.WriteLine("몬스터 목록:");
            for (int i = 0; i < monsters.Count; i++)
            {
                var monster = monsters[i];
                string status = monster.Stat.Hp > 0 ? "(선택 가능)" : "(죽음)";
                Console.WriteLine($"[{i}] {monster.Name} (HP: {monster.Stat.Hp}) {status}");
            }
            Console.WriteLine("================");
        }

        public string DisplayHP(int hp, int maxHp)
        {
            string hpstr;

            if (hp <= maxHp * 0.2)
                return hpstr = $" \x1b[5m{hp}\x1b[0m"; // 체력이 얼마 남지 않으면 깜빡임 (ANSI EC)
            else
                return hpstr = $" (HP: {hp}";
        }
    }

    internal class BattleController
    {
        private Player player;
        private List<Monster> monsters;
        private BattleView view;

        public BattleController(Player player, List<Monster> monsters, BattleView view)
        {
            this.player = player;
            this.monsters = monsters;
            this.view = view;
        }

        public void StartBattle()
        {
            if (player == null)
            {
                LogManager.Instance.Log(LogLevel.ERROR, "DungeonScene::Fight(), 플레이어가 존재하지 않음");
                return;
            }

            while (player.IsAlive && monsters.Count > 0)
            {
                Console.Clear();

                view.ShowBattleStatus(player, monsters);
                PlayerTurn();
                if (monsters.Count == 0) break;
                MonsterTurn();

                Console.ReadKey();
            }
            Console.WriteLine(player.IsAlive ? "전투에서 승리했습니다!" : "플레이어가 패배했습니다...");
        }

        private void PlayerTurn()
        {
            // 선택할 수 있는 몬스터를 필터링 (HP가 0 이하인 몬스터는 제외)

            int monsterAlives = 0;
            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].Stat.Hp > 0) monsterAlives++;
            }

            if (monsterAlives == 0)
            {
                Console.WriteLine("모든 몬스터가 죽었습니다. 전투 종료!");
                return;
            }

            Console.Write("공격할 몬스터의 번호를 선택하세요: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && monsters.Count > choice && choice >= 0)
            {
                // 몬스터 공격
                player.Attack(monsters[choice]);

                // 몬스터가 죽으면 처리
                if (monsters[choice].Stat.Hp <= 0)
                {
                    Console.WriteLine($"{monsters[choice].Name}을 처치했습니다!");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다!");
            }
        }

        private void MonsterTurn()
        {
            foreach (var monster in monsters)
            {
                if (monster.Stat.Hp > 0)
                {
                    monster.Attack(player);

                    Console.WriteLine($"{monster.Name}가 {player.Name}을 공격했습니다!");
                    Console.WriteLine($"{player.Name}의 남은 체력: {player.Stat.Hp}");

                    if (!player.IsAlive)
                    {
                        Console.WriteLine("플레이어가 사망했습니다...");
                        return;
                    }
                }
            }
        }
       
    }

}
