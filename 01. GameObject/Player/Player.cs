using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace TextRPG_Maple
{
    internal class Player : GameObject
    {
        public bool IsAlive => Stat.Hp > 0;
        public int EquipAtk { get; set; }
        public int EquipDef { get; set; }

        public Player(string name) : base(name)
        {


        }

        public override void Attack(GameObject obj)
        {
            int damage = Math.Max(0, Stat.Atk + EquipAtk - obj.Stat.Def);

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
