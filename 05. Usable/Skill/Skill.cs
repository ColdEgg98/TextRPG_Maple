using TextRPG_Maple._04._Manager;
using TextRPG_Maple._04._Manager._05._Object;

namespace TextRPG_Maple._05._Usable.Skill
{
    enum SkillType
    {
        전사,
        도적,
        버프
    }

    internal class Skill : Usable
    {
        static List<Skill> skillData = new List<Skill>();
        static public void SetSkillList(List<Skill> skills) { skillData = skills; }

        public SkillType SkillType { get; set; }
        public int NeedLv { get; set; }
        public bool isAeraSkill { get; set; }

        public Skill() : base("", 0, "", 0)
        {

        }

        public Skill(string name, int needLv, SkillType skillType, float value, string descrip, int cost, bool isAreaSkill) : base(name, value, descrip, cost)
        {
            isAeraSkill = isAreaSkill;
            SkillType = skillType;
            NeedLv = needLv;
        }

        public List<Skill> SetSkillType(string strClass)
        {
            switch (strClass)
            {
                case "전사":
                    return SetWarriorSkill();
                case "도적":
                    return SetThief_skillSet();
                case "마법사":
                    return SetWarriorSkill();
                default:
                    Console.WriteLine("예외가 발생했습니다.");
                    Thread.Sleep(1000);
                    return null;
            }
        }

        public override string GetTypeString()
        {
            return SkillType.ToString();
        }

        public List<Skill> SetWarriorSkill()
        {
            List<Skill> Warrior_skillSet = skillData.Where(skill => skill.SkillType == SkillType.전사).ToList();

            return Warrior_skillSet;
        }

        public List<Skill> SetThief_skillSet()
        {
            List<Skill> Thief_skillSet = skillData.Where(skill => skill.SkillType == SkillType.도적).ToList();

            return Thief_skillSet;
        }
    }
}
