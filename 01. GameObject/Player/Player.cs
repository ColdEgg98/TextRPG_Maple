using TextRPG_Maple._04._Manager;
using TextRPG_Maple._04._Manager._05._Object;
using TextRPG_Maple._05._Usable.Item;
using TextRPG_Maple._05._Usable.Skill;

namespace TextRPG_Maple
{
    internal class Player : GameObject
    {
        public int Level { get; set; }
        public int eventLv
        {
            get => Level;
            set
            {
                Level = value;
                LevelUPEvent?.Invoke();
            }
        }
        public List<Item> Inventory { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Skill> classSkill { get; set; }
        public string Class { get; set; }
        public bool IsAlive => Stat.Hp > 0;
        public int requiredExp { get; set; }
        public int EquipAtk { get; set; }
        public int EquipDef { get; set; }

        public event Action LevelUPEvent;

        public Player(string name) : base(name)
        {
            Level = 1;
            Stat.Atk = 10;
            Stat.Def = 5;
            Stat.Hp = 100;
            Stat.MaxHp = Stat.Hp;
            Stat.Mp = 50;
            Stat.MaxMp = Stat.Mp;
            Stat.Gold = 1000;
            Stat.Exp = 0;

            Inventory = new List<Item>();
            Skills = new List<Skill>();
            classSkill = new List<Skill>();
            Class = "";
            requiredExp = 100;
            EquipAtk = 0;
            EquipDef = 0;
            LevelUPEvent += LearnSkillEvent;
        }
        public Player(Player other) : base(other.Name) { }

        /// 전투
        public override void Attack(GameObject monster)
        {
            int damage = Math.Max(0, Stat.Atk + EquipAtk - monster.Stat.Def);
            SoundManager.Instance.PlaySound(SoundType.Attack, "Player_Attack");

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
            if (Stat.Mp >= skill.Cost && skill.IsEquip)
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
                Console.WriteLine("마나가 부족하거나 장비 중이 아닙니다..");
            }
        }

        public void Takeloot(GameObject monster)
        {
            Stat.Gold += monster.Stat.Gold;
            Stat.Exp += monster.Stat.Exp;
            Console.WriteLine($"{Stat.Gold}G 를 획득했습니다.");
            Console.WriteLine($"{Stat.Exp} 경험치를 획득했습니다.");
            SoundManager.Instance.PlaySound(SoundType.BGM, "Final_Fantasy_Victory");
            if (Stat.Exp >= requiredExp)
                LevelUp();
        }

        public void LevelUp()
        {
            eventLv++;
            Stat.Exp -= requiredExp;
            requiredExp += 100;

            Stat.MaxHp += 10;
            Stat.Hp += 10;
            Stat.MaxMp += 5;
            Stat.Mp += 5;
            Stat.Atk += 5;
            Stat.Def += 2;

            Console.WriteLine("레벨업!");
            Console.Write("현재 레벨 : ");
            InputManager.Instance.WriteLineColor($"{Level}", ConsoleColor.Yellow);

            if (Stat.Exp >= requiredExp)
                LevelUp();
        }

        /// 직업 & 스킬
        public void SetClass(int input)
        {
            Skill skill = new Skill();

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
            classSkill = skill.SetSkillType(this.Class);
        }

        public void LearnSkillEvent()
        {
            foreach (var skill in classSkill)
            {
                if (skill.NeedLv == Level && !skill.IsOwned)
                {
                    skill.IsOwned = true;
                    this.AddSkill(skill);
                    Console.WriteLine("레벨업으로 스킬을 획득했습니다!");
                    Console.WriteLine($"스킬 획득! : {skill.Name}");
                    SoundManager.Instance.PlaySound(SoundType.learnSkill, "LearnSkill");
                    Thread.Sleep(500);

                    break;
                }
            }
        }

        private void AddSkill(Skill Skill)
        {
            Player? player = GameObjectManager.Instance.GetGameObject(ObjectType.PLAYER, "MainPlayer") as Player;
            player.Skills.Add(Skill);
        }


        /// 아이템
        public void EquipItem(Item item)
        {
            if (item.IsEquip)
            {
                // 장비 빼기
                UnEquip(item);
            }
            else
            {
                // 아이템 착용 & 사용
                Equip(item);
            }
        }

        private void Equip(Item item)
        {
            if (item.ItemType == ItemType.Weapon)
            {
                for (int i = 0; i < Inventory.Count; i++) // 중복 부위의 장비 벗기
                {
                    if (Inventory[i].ItemType == ItemType.Weapon)
                    {
                        UnEquip(Inventory[i]);
                    }
                }
                EquipAtk += (int)item.Value;
                item.IsEquip = true;
            }

            else if (item.ItemType == ItemType.Armor)
            {
                for (int i = 0; i < Inventory.Count; i++)
                {
                    if (Inventory[i].ItemType == ItemType.Armor)
                    {
                        UnEquip(Inventory[i]);
                    }
                }
                EquipDef += (int)item.Value;
                item.IsEquip = true;
            }

            else
            {
                Stat.Hp += (int)item.Value;
                Inventory.Remove(item);
            }
        }

        private void UnEquip(Item item)
        {
            item.IsEquip = false;

            if (item.ItemType == ItemType.Weapon)
            {
                EquipAtk -= (int)item.Value;
            }
            else if (item.ItemType == ItemType.Armor)
            {
                EquipDef -= (int)item.Value;
            }
        }

        public override GameObject Clone()
        {
            return new Player(this);
        }
    }
}