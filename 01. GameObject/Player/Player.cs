using TextRPG_Maple._04._Manager;
using TextRPG_Maple._05._Usable.Skill;

namespace TextRPG_Maple
{
    internal class Player : GameObject
    {
        //public List<Item> Items { get; set; }
        public List<Skill> Skills { get; set; }
        public string Class { get; set; }
        public bool IsAlive => Stat.Hp > 0;
        public int requiredExp { get; set; }
        public int EquipAtk { get; set; }
        public int EquipDef { get; set; }

        public Player(string name) : base(name)
        {
            Stat.Level = 1;
            Stat.Atk = 10;
            Stat.Def = 5;
            Stat.Hp = 100;
            Stat.MaxHp = Stat.Hp;
            Stat.Mp = 50;
            Stat.MaxMp = Stat.Mp;
            Stat.Gold = 1000;
            Stat.Exp = 0;

            Skills = new List<Skill>();
            Class = "";
            requiredExp = 100;
            EquipAtk = 0;
            EquipDef = 0;
        }

        public override void Attack(GameObject monster)
        {
            int damage = Math.Max(0, Stat.Atk + EquipAtk - monster.Stat.Def);

            monster.TakeDamage(damage);
        }

        public override void TakeDamage(int damage)
        {
            Stat.Hp -= damage;
            if (Stat.Hp < 0)
                Stat.Hp = 0;

            Console.WriteLine($"{Name}이(가) {damage}만큼 피해를 입었습니다! (남은 HP: {Stat.Hp})");
        }

        public void SkillAttack(GameObject moster, Skill skill)
        {
            if (Stat.Mp >= skill.Cost)
            {
                Stat.Mp -= skill.Cost;

                int damage = Math.Max(0, (int)((Stat.Atk + EquipAtk) * skill.Value - moster.Stat.Def));

                Console.Write($"{Name}이(가)");
                InputManager.Instance.WriteColor($"{skill.Name}", ConsoleColor.DarkYellow);
                Console.WriteLine($"스킬을(를) 사용했습니다! (남은 MP: {Stat.Mp}/{Stat.MaxMp}");

                moster.TakeDamage(damage);
            }
            else
            {
                Console.WriteLine("MP가 부족합니다.");
            }
        }

        public void Takeloot(GameObject monster)
        {
            Stat.Gold += monster.Stat.Gold;
            Stat.Exp += monster.Stat.Exp;
            Console.WriteLine($"{Stat.Gold}G 를 획득했습니다.");
            Console.WriteLine($"{Stat.Exp} 경험치를 획득했습니다.");
            if (Stat.Exp >= requiredExp)
                LevelUp();
        }

        public void LevelUp()
        {
            Stat.Level++;
            Stat.Exp -= requiredExp;
            requiredExp = 0;
            requiredExp += 100;

            Stat.MaxHp += 10;
            Stat.Hp += 10;
            Stat.MaxMp += 5;
            Stat.Mp += 5;
            Stat.Atk += 5;
            Stat.Def += 2;

            Console.WriteLine("레벨업!");
            Console.WriteLine($"현재 레벨 : {Stat.Level}");
        }
        public void SetClass(int input)
        {
            switch (input)
            {
                case 1:
                    Class = "전사";
                    break;
                case 2:
                    Class = "도적";
                    break;
                case 3:
                    Class = "마법사";
                    break;
            }
        }
    }
}