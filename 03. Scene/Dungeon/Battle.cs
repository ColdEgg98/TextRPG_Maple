using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Maple._01._GameObject.Monster;
using TextRPG_Maple._01._GameObject.Monster.Boss;
using TextRPG_Maple._04._Manager;
using TextRPG_Maple._04._Manager._04._Log;
using TextRPG_Maple._04._Manager._05._Object;

namespace TextRPG_Maple._03._Scene.Dungeon
{
    internal class BattleView
    {
        public void ShowBattleStatus(Player player, List<Monster> monsters)
        {
            var monster = monsters[0];
            if (monster is Boss boss) // boss로 변환 성공하면 실행
            {
                string status = boss.Stat.Hp > 0 ? "(선택 가능)" : "(죽음)";
                // 1번부터
                if (boss.Stat.Hp > 0)
                {
                    // 아트 출력
                    boss.DisplayBoss();
                    Console.WriteLine("==== 전투 시작 ====");
                    Console.WriteLine($"플레이어: {player.Name} (HP: {player.Stat.Hp} / MP : {player.Stat.Mp})\n");
                    Console.WriteLine("몬스터 목록:");
                    Console.WriteLine($"[1] {boss.Name} (HP: {boss.Stat.Hp}) {status}");
                    boss.RenderBossHP();
                }
                else
                    InputManager.Instance.WriteLineColor($"[1] {boss.Name} (HP: {boss.Stat.Hp}) {status}", ConsoleColor.DarkGray);
            }
            // 일반 전투
            else
            {
                Console.WriteLine("==== 전투 시작 ====");
                Console.WriteLine($"플레이어: {player.Name} (HP: {player.Stat.Hp} / MP : {player.Stat.Mp})\n");
                Console.WriteLine("몬스터 목록: \n");
                for (int i = 0; i < monsters.Count; i++)
                {
                    string status = monster.Stat.Hp > 0 ? "(선택 가능)" : "(죽음)";
                    // 1번부터
                    if (monster.Stat.Hp > 0)
                    {
                        Console.WriteLine($"[{i + 1}] {monster.Name} (HP: {monster.Stat.Hp}) {status}");
                        Console.WriteLine();
                    }
                    else
                    {
                        InputManager.Instance.WriteLineColor($"[{i + 1}] {monster.Name} (HP: {monster.Stat.Hp}) {status}", ConsoleColor.DarkGray);
                        Console.WriteLine();
                    }
                }
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

        public int StartBattle()
        {
            if (player == null)
            {
                LogManager.Instance.Log(LogLevel.ERROR, "DungeonScene::Fight(), 플레이어가 존재하지 않음");
                return -1;
            }
            var monster = monsters[0];
            if (monster is Boss boss) // boss로 변환 성공하면 실행
            {
                boss.BossSetUp();
                boss.SoundBoss();// 음악 실행
            }
            while (player.IsAlive && monsters.Count > 0)
            {
                Console.Clear();

                view.ShowBattleStatus(player, monsters);
                PlayerTurn();
                MonsterTurn();
                int monsterAlives = 0;
                for (int i = 0; i < monsters.Count; i++)
                {
                    if (monsters[i].Stat.Hp > 0) monsterAlives++;
                }
                if (monsterAlives == 0)
                    break;
                //대기
                Console.ReadKey();
            }
            // Result
            Console.WriteLine();
            InputManager.Instance.WriteLineColor("전투 결과\n", ConsoleColor.DarkYellow);
            Console.WriteLine(player.IsAlive ? "승리!!" : "패배...");
            Console.WriteLine(player.IsAlive ? "전투에서 승리했습니다!" : "플레이어가 패배했습니다...");
            if (player.IsAlive)
            {
                SoundManager.Instance.PlaySound(SoundType.BGM, "Final_Fantasy_Victory");
                Thread.Sleep(2000);
                Console.WriteLine("\n1. 계속 진행하기");
                Console.WriteLine("0. 입구로 돌아가기");
                int input = GameManager.Instance.GetInput(0, 1);
                if (input == 0)
                    return 1;
                else
                {
                    SoundManager.Instance.StopSound(0);
                    SoundManager.Instance.PlaySound(SoundType.BGM, "Maplestory BGM - Missing You", true);
                    return 2;
                }
            }
            else
            {
                Console.WriteLine("0. 입구로 돌아가기");
                int input = GameManager.Instance.GetInput(0, 0);
                return 0;
            }
        }

        private void PlayerTurn()
        {
            // 선택할 수 있는 몬스터를 필터링 (HP가 0 이하인 몬스터는 제외)

            int monsterAlives = 0;
            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].Stat.Hp > 0) monsterAlives++;
            }

            Console.WriteLine("1 ) 일반 공격");
            Console.WriteLine("2 ) 스킬");
            int act = GameManager.Instance.GetInput(1, 2);
            int input;
            int skillInput = 0;

            // 

            while (true)
            {
                if (act == 2)
                {
                    Console.WriteLine("");
                    player.ShowSkill();
                    if(player.Skills.Count == 0)
                    {
                        act = 1;
                        continue;
                    }

                    skillInput = GameManager.Instance.GetInput(1, player.Skills.Count, " ");
                    --skillInput;
                    if (!player.Skills[skillInput].IsEquip)
                    {
                        InputManager.Instance.WriteLineColor("해당 스킬은 미장착 되어있습니다..", ConsoleColor.DarkGray);
                        bool isAllSkillflase = true;
                        for (int i = 0; player.Skills.Count > i; i++)
                        {
                            if (player.Skills[i].IsEquip)
                                isAllSkillflase = false;
                        }
                        if (isAllSkillflase)
                            act = 1;
                        continue;
                    }
                }

                Console.WriteLine("");
                input = GameManager.Instance.GetInput(1, monsters.Count, " >>> 공격 대상을 선택해주세요: ");
                input--;
                if (monsters[input].Stat.Hp <= 0)
                {
                    Console.WriteLine("\n잘못 된 대상입니다.");
                    Thread.Sleep(500);
                    // 원래 위치로
                    int top = Console.CursorTop;
                    for (int i = 0; i < 6; i++)
                    {
                        Console.SetCursorPosition(0, top--);
                        Console.Write(new string(' ', 60));
                    }
                }
                else
                { 
                    // 다음 진행
                    break;
                }
            }
            Console.Clear();
            Console.WriteLine("======================");
            if (monsters.Count > input)
            {
                // 몬스터 공격
                Console.WriteLine("플레이어의 공격!");
                if (act == 1)
                    player.Attack(monsters[input]);
                else if (act == 2)
                    player.SkillAttack(monsters[input], player.Skills[skillInput]);
                Thread.Sleep(300);

                // 몬스터가 죽으면 처리
                if (monsters[input].Stat.Hp <= 0)
                {
                    Console.WriteLine($"{monsters[input].Name}을 처치했습니다!");
                    player.Takeloot(monsters[input]);
                    Thread.Sleep(500);
                }
            }
        }

        private void MonsterTurn()
        {
            foreach (var monster in monsters)
            {
                if (monster.Stat.Hp > 0)
                {
                    Console.WriteLine($"\n{monster.Name}의 공격!!");
                    monster.Attack(player);
                    Thread.Sleep(300);
                    if (!player.IsAlive)
                    {
                        Console.WriteLine("플레이어가 사망했습니다...");
                        return;
                    }
                }
            }
            Console.WriteLine("======================");
        }
    }
}