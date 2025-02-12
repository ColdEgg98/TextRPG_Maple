using TextRPG_Maple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Collections;
using System.Threading;
using TextRPG_Maple._04._Manager;

namespace TextRPG_Maple._01._GameObject.Monster
{
    internal class Monster : GameObject
    {
        public Monster() : base("") { }
        public Monster(string name, int hp, int attack, int defense, int exp, int gold) : base(name)
        {
            Stat.Hp = hp;
            Stat.MaxHp = Stat.Hp;
            Stat.Atk = attack;
            Stat.Def = defense;
            Stat.Exp = exp;
            Stat.Gold = gold;
        }
        public Monster(Monster other) : base(other.Name)
        {
            Stat = other.Stat.Clone();
        }

        public void AddAbility(int value)
        {
            Stat.Hp += value * 5;
            Stat.MaxHp = Stat.Hp;

            Stat.Atk += value * 2;
            Stat.Def += value;

            Stat.Exp += value * 20;
            Stat.Gold += value * 100;
        }

        public override void Attack(GameObject obj)
        {
            int damage = Math.Max(0, Stat.Atk - obj.Stat.Def);

            Random rand = new Random();
            double value = rand.NextDouble();
            if (value < 0.1f) // 10% 확률로 회피
            {
                SoundManager.Instance.PlaySound(SoundType.Attack, "Miss");
                InputManager.Instance.WriteColor("하지만 빗나갔다!\n", ConsoleColor.DarkBlue);
            }
            else if (value < 0.85f)
            {
                obj.TakeDamage(damage);
            }
            //else // 10% 확률로 크리티컬
            //{
            //    SoundManager.Instance.PlaySound(SoundType.Attack, "Critical");
            //    InputManager.Instance.WriteColor("크리티컬! (Critical)", ConsoleColor.Red);
            //    // 방어 무시 2배
            //    obj.TakeDamage(damage * 2);
            //}
        }

        public override GameObject Clone()
        {
            return new Monster(this);
        }

        public override void TakeDamage(int damage)
        {
            Stat.Hp -= damage;
            if (Stat.Hp < 0)
                Stat.Hp = 0;

            Console.WriteLine($"{Name}이(가) {damage}만큼 피해를 입었습니다! (남은 HP: {Stat.Hp})");
        }

        public void displayTakeDamage()
        {
            // ANSI Escape Code로 대미지 받는 몬스터 이름을 깜빡거리게

        }
    }
}
