using TextRPG_Maple._01._GameObject.Monster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Maple
{
    internal abstract class GameObject
    {
        public string Name;
        public Status Stat { get; set; }
        
        public GameObject(string name)
        {
            Name = name;
        }

        public abstract void Attack(GameObject obj);
        //{
        //    Console.WriteLine($"{Name}가 {obj.Name}을 공격했습니다!");
        //    obj.TakeDamage(Stat.Atk);
        //}
        public abstract void TakeDamage(int damage);
        //{
        //    Health -= Math.Max(0, damage - Defense);
        //    if (Health < 0) Health = 0;

        //    Console.WriteLine($"{Name}이(가) {Math.Max(0, damage - Defense)}만큼 피해를 입었습니다! (남은 HP: {Health})");
        //}

        //public void Attack(Player player)
        //{
        //    player.

        //    player.Health -= Atk;

        //    if (player.Health < 0)
        //        player.Health = 0;
        //}
    }

    internal class Status
    {
        //프로퍼티는 {get; set;}은 읽기+쓰기, {get;}은 읽기
        public int Level { get; }
        public string Job { get; } = "";

        public int Atk { get; set; }
        public int Def { get; set; }

        public int Gold { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int Mp { get; set; }
        public int MaxMp { get; set; }
    }
}
