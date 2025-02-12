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
        public string Name { get; set; } = "";
        public Status Stat { get; set; }
        
        public GameObject(string name)
        {
            Name = name;
        }

        public abstract void Attack(GameObject obj);
        public abstract void TakeDamage(int damage);
        public abstract GameObject Clone();
    }

    internal class Status
    {
        //프로퍼티는 {get; set;}은 읽기+쓰기, {get;}은 읽기
        public int Level { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Gold { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int Mp { get; set; }
        public int MaxMp { get; set; }
        public int Exp { get; set; }
        public int Dex { get; set; }
        public int Luc { get; set; }
        public Status Clone()
        {
            return (Status)this.MemberwiseClone();
        }
    }
}
