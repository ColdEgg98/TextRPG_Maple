using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp1
{
    internal class Player : GameObject
    {
        public bool IsAlive => Stat.Hp > 0;
        public int EquipAtk { get; set; }
        public int EquipDef { get; set; }





        public Player(string name) : base(name)
        {
            Stat.Level = 1;
            Stat.Job = "전사";
            Stat.Atk = 10;
            Stat.Def = 5;
            Stat.Hp = 100;
            Stat.MaxHp = Stat.Hp;
            Stat.Mp = 50;
            Stat.MaxMp = Stat.Mp;
            Stat.Gold = 1000;

            EquipAtk = 0;
            EquipDef = 0;
        }

        public override void Attack(GameObject monster)
        {
            int damage = Math.Max(0, Stat.Atk + EquipAtk - monster.Stat.Def);

            monster.TakeDamage(damage);
            Console.WriteLine($"{Name}이(가) {monster.Name}에게 {damage}만큼 피해를 입혔습니다! (남은 HP: {monster.Stat.Hp})");
        }

        public override void TakeDamage(int damage)
        {
            Stat.Hp -= damage;
            if (Stat.Hp < 0)
                Stat.Hp = 0;

            //Console.WriteLine($"{Name}이(가) {damage}만큼 피해를 입었습니다! (남은 HP: {Stat.Hp})");
        }

        public void TakeGold(GameObject monster)
        {
            Stat.Gold += monster.Stat.Gold;
        }
    }
}
