using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Maple._01._GameObject.Monster;
using TextRPG_Maple._04._Manager._06._DB;

namespace TextRPG_Maple._06._DB
{
    internal class MonsterData
    {
        public string Name { get; }
        public int HP { get; }
        public int Attack { get; }
        public int Defense { get; }
        public int Exp { get; }
        public int Gold { get; }

        public MonsterData(string name, int hp, int attack, int defense, int exp, int gold)
        {
            Name = name;
            HP = hp;
            Attack = attack;
            Defense = defense;
            Exp = exp;
            Gold = gold;
        }
    }

    internal class MonsterFactory
    {
        public static Monster? CreateMonster(string name)
        {
            MonsterData? data = DBManager.Instance.GetMonsterData(name);
            if (data == null)
            {
                Console.WriteLine($"[ERROR] {name} 몬스터 데이터를 찾을 수 없습니다!");
                return null;
            }

            return new Monster(data.Name, data.HP, data.Attack, data.Defense, data.Exp, data.Gold);
        }
    }
}