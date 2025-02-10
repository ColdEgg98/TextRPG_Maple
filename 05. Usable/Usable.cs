using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Maple._05._Usable
{
    enum UsableType
    {
        Weapon,
        Armor,
        Skill
    }

    internal abstract class Usable ()
    {
        public string Name { get; set; } = "";
        public UsableType UsableType { get; }
        public string Descrip { get; set; } = "";
        public int Value { get; set; } // 장비 능력치 혹은 계수
        public int Cost { get; set; } // 가격 혹은 소비 마나
        public bool IsEquip { get; set; }
        public bool IsOwned { get; set; }

        public Usable(string name, UsableType type, int value, string descrip, int cost) : this()
        {
            Name = name;
            UsableType = type;
            Value = value;
            Descrip = descrip;
            Cost = cost;
            IsEquip = false;
            IsOwned = false;
        }

        public string UsableDisplay()
        {
            string str = IsEquip ? "[E]" : "";
            str += $"{Name} | {GetTypeString()} | {Descrip}";
            return str;
        }

        public string GetTypeString()
        {
            return "";
        }

        public string GetPriceString()
        {
            string str = IsOwned ? "구매완료" : $"{Cost}";
            return str;
        }
    }
}
