using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Maple._04._Manager._05._Object;
using TextRPG_Maple._05._Usable.Skill;

namespace TextRPG_Maple._05._Usable.Item
{
    enum ItemType
    {
        Weapon,
        Armor,
        Potion,
        End
    }

    internal class Item : Usable
    {
        public ItemType ItemType;

        public Item(string name, ItemType itemType, float value, string descrip, int cost) : base(name, value, descrip, cost)
        {
            Name = name;
            ItemType = itemType;
            IsOwned = false;
        }

        public override string GetTypeString()
        {
            return ItemType.ToString();
        }

        public string GetPriceString()
        {
            string str = IsOwned ? "구매완료" : $"{Cost}";
            return str;
        }
    }
}
