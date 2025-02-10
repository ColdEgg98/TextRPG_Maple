namespace TextRPG_Maple._05._Usable
{
    //enum ItemType
    //{
    //    Weapon,
    //    Armor
    //}

    internal abstract class Usable ()
    {
        public string Name { get; set; } = "";
        public string Descrip { get; set; } = "";
        public float Value { get; set; } // 장비 능력치 혹은 계수
        public int Cost { get; set; } // 가격 혹은 소비 마나
        public bool IsEquip { get; set; }
        public bool IsOwned { get; set; }

        public Usable(string name, float value, string descrip, int cost) : this()
        {
            Name = name;
            Value = value;
            Descrip = descrip;
            Cost = cost;
            IsEquip = false;
            IsOwned = false;
        }

        public virtual string UsableDisplay()
        {
            string str = IsEquip ? "[E]" : "";
            str += $"{Name} | {GetTypeString()} | {Descrip}";
            return str;
        }

        public string GetTypeString()
        {
            return "미구현";
        }
    }
}
