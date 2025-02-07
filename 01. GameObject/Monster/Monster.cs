using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp2._01._GameObject.Monster
{
    internal class Monster : GameObject
    {
        public Monster(string name, int hp, int attack, int defense) : base(name)
        {
            Stat.Hp = hp;
            Stat.MaxHp = Stat.Hp;

            Stat.Atk = attack;
            Stat.Def = defense;
        }

        public override void Attack(GameObject obj)
        {
            int damage = Math.Max(0, Stat.Atk - obj.Stat.Def);

            obj.TakeDamage(damage);
        }

        public override void TakeDamage(int damage)
        {
            Stat.Hp -= damage;
            if (Stat.Hp < 0)
                Stat.Hp = 0;

            Console.WriteLine($"{Name}이(가) {damage}만큼 피해를 입었습니다! (남은 HP: {Stat.Hp})");
        }
    }
}
