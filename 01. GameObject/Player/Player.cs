using System.Xml.Linq;
using System.Diagnostics;
using TextRPG_Maple._04._Manager;
using TextRPG_Maple._04._Manager._05._Object;
using TextRPG_Maple._05._Usable.Item;
using TextRPG_Maple._05._Usable.Skill;

namespace TextRPG_Maple
{
    internal class Player : GameObject
    {
        static Random rand = new Random();

        public int Level { get; set; }
        public List<Item> Inventory { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Skill> classSkill { get; set; }
        public string Class { get; set; }
        public bool IsAlive => Stat.Hp > 0;
        public int requiredExp { get; set; }
        public int EquipAtk { get; set; }
        public int EquipDef { get; set; }
        public int eventLv
        {
            get => Level;
            set
            {
                Level = value;
                LevelUPEvent?.Invoke();
            }
        }

        public event Action LevelUPEvent;

        public int DungeonFloor { get; set; } = 1;

        public Player() : base("")
        {
            LevelUPEvent -= LearnSkillEvent;
            LevelUPEvent += LearnSkillEvent;
        }

        public Player(string name) : base(name)
        {
            Stat = new Status();

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

        public Player(Player other) : base(other.Name)
        {
            Stat = other.Stat.Clone();
        }

        /// ����
        public override void Attack(GameObject monster)
        {
            int damage = Math.Max(0, Stat.Atk + EquipAtk - monster.Stat.Def);
            double value = rand.NextDouble();

            if (value < 0.1f) // 10% Ȯ���� ȸ��
            {
                SoundManager.Instance.PlaySound(SoundType.Attack, "Miss");
                InputManager.Instance.WriteColor("������ ��������!\n", ConsoleColor.DarkBlue);
            }
            else if (value < 0.85f)
            {
                SoundManager.Instance.PlaySound(SoundType.Attack, "Player_Attack");
                monster.TakeDamage(damage);
            }
            else // 10% Ȯ���� ũ��Ƽ��
            {
                SoundManager.Instance.PlaySound(SoundType.Attack, "Critical");
                InputManager.Instance.WriteColor("ũ��Ƽ��! (Critical)", ConsoleColor.Red);
                // ��� ���� 2��
                monster.TakeDamage((Stat.Atk + EquipAtk) * 2);
            }
        }

        public override void TakeDamage(int damage)
        {
            Stat.Hp -= damage;
            if (Stat.Hp < 0)
                Stat.Hp = 0;

            Console.WriteLine($"{Name}��(��) {damage}��ŭ ���ظ� �Ծ����ϴ�! (���� HP: {Stat.Hp})");
        }

        public void SkillAttack(GameObject moster, Skill skill)
        {
            if (Stat.Mp >= skill.Cost && skill.IsEquip)
            {
                Stat.Mp -= skill.Cost;

                int damage = Math.Max(0, (int)((Stat.Atk + EquipAtk) * skill.Value - moster.Stat.Def));

                Console.Write($"{Name}��(��)");
                InputManager.Instance.WriteColor($"{skill.Name}", ConsoleColor.DarkYellow);
                Console.WriteLine($"��ų��(��) ����߽��ϴ�! (���� MP: {Stat.Mp}/{Stat.MaxMp}");

                moster.TakeDamage(damage);
            }
            else
            {
                Console.WriteLine("������ �����ϰų� ��� ���� �ƴմϴ�..");
            }
        }

        public void Takeloot(GameObject monster)
        {
            Stat.Gold += monster.Stat.Gold;
            Stat.Exp += monster.Stat.Exp;
            Console.WriteLine($"{monster.Stat.Gold}G �� ȹ���߽��ϴ�.");
            Console.WriteLine($"{monster.Stat.Exp} ����ġ�� ȹ���߽��ϴ�.");
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

            Console.WriteLine("������!");
            Console.Write("���� ���� : ");
            InputManager.Instance.WriteLineColor($"{Level}", ConsoleColor.Yellow);

            if (Stat.Exp >= requiredExp)
                LevelUp();
        }

        /// ���� & ��ų
        public void SetClass(int input)
        {
            Skill skill = new Skill();

            switch (input)
            {
                case 1:
                    Class = "����";
                    break;
                case 2:
                    Class = "����";
                    break;
                case 3:
                    Class = "������";
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
                    Console.WriteLine("���������� ��ų�� ȹ���߽��ϴ�!");
                    Console.WriteLine($"��ų ȹ��! : {skill.Name}");
                    SoundManager.Instance.PlaySound(SoundType.learnSkill, "LearnSkill");
                    Thread.Sleep(500);

                    break;
                }
            }
        }

        public void ShowSkill()
        {
            if (this.Skills.Count == 0)
                InputManager.Instance.WriteLineColor("��� ��ų�� �����ϴ�...", ConsoleColor.DarkGray);
            for (int i = 0; i < this.Skills.Count; i++)
            {
                Console.WriteLine($"{i + 1}. " + this.Skills[i].UsableDisplay());
            }
        }

        private void AddSkill(Skill Skill)
        {
            Player? player = GameObjectManager.Instance.GetGameObject(ObjectType.PLAYER, "MainPlayer") as Player;
            player.Skills.Add(Skill);
        }


        /// ������
        public void EquipItem(Item item)
        {
            if (item.IsEquip)
            {
                // ��� ����
                UnEquip(item);
            }
            else
            {
                // ������ ���� & ���
                Equip(item);
            }
        }

        private void Equip(Item item)
        {
            if (item.ItemType == ItemType.Weapon)
            {
                for (int i = 0; i < Inventory.Count; i++) // �ߺ� ������ ��� ����
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