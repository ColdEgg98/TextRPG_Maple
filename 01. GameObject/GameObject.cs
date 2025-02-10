using ConsoleApp2._01._GameObject.Monster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
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

        public abstract void TakeDamage(int damage);
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
    }
}
